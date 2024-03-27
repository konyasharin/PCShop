using System.ComponentModel;
using System.Reflection;
using backend.Entities;
using backend.Entities.CommentEntities;
using backend.Entities.ComponentsInfo;
using backend.Entities.User;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ProductController
    {
        protected readonly ILogger<ComponentController> logger;
        protected readonly string connectionString;
        
        public ComponentController(ILogger<ComponentController> logger) : base(logger)
        {
            this.logger = logger;
            DotNetEnv.Env.Load();
            connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        }

        protected async Task<IActionResult> CreateComponent<T>(T component, string[] characteristics, string databaseName) where T: Component<IFormFile>
        {
            try
            {
                string[] characteristicsBase = ["brand", "model", "country", "price", "description", "image", "amount", "power", "likes", "product_type"];
                string imagePath = BackupWriter.Write(component.Image);


                if (component.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }
                if (component.Amount < 0)
                {
                    return BadRequest(new {error = "Amount must not be less than 0"});
                }

                if(component.Power < 0 || component.Power > 10)
                {
                    return BadRequest(new { error = "Power must be between 0 and 10" });
                }

                component.Likes = 0;
                PropertyInfo[] properties = typeof(T).GetProperties();
                var requestData = new Dictionary<string, object>();
                foreach (var property in properties)
                {
                    if (property.Name != "Image")
                    {
                        requestData[property.Name] = property.GetValue(component);
                    }
                    else
                    {
                        requestData["Image"] = imagePath;
                    }
                }

                requestData["product_type"] = databaseName;
                await using var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                requestData["product_id"] = connection.QueryFirstOrDefault<int>($"INSERT INTO public.products ({TransformCharacteristicsToString(characteristicsBase)}) VALUES ({TransformCharacteristicsToString(characteristicsBase, "@")}) RETURNING product_id", requestData);
                connection.QueryFirstOrDefault<int>($"INSERT INTO public.{databaseName} (product_id, {TransformCharacteristicsToString(characteristics)}) VALUES (@product_id, {TransformCharacteristicsToString(characteristics, "@")})", requestData);
                logger.LogInformation($"Component data saved to database with id {requestData["product_id"]}");
                return Ok(new { id = requestData["product_id"], requestData });
            }
            catch (Exception ex)
            {
                logger.LogError($"Component data failed to save in database. Exception: {ex}");
                return BadRequest(new { error = ex.Message});
            }
        }

        protected async Task<IActionResult> GetAllComponents<T>(int limit, int offset, string databaseName, string[] characteristics) where T : Component<string>
        {
            logger.LogInformation(TransformToCamelCase(["brand", "model"], ["product_type"]));
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    var computerCasesBase = connection.Query<Component<string>>($"SELECT {TransformToCamelCase(SeparateCharacteristics(characteristics)["characteristicsWithoutSnakes"],
                        SeparateCharacteristics(characteristics)["characteristicsWithSnakes"])} FROM public.products WHERE product_type = '{databaseName}'",
                        new { Limit = limit, Offset = offset });
                    Dictionary<string, object>[] computerCases = [];
                    foreach (var computerCase in computerCasesBase)
                    {
                        logger.LogInformation(computerCase.ProductType);
                        logger.LogInformation(computerCase.ProductId.Value.ToString());
                        if (computerCase.ProductId != null)
                        {
                            computerCases.Append(await JoinProductInfo(computerCase.ProductId.Value, databaseName, computerCase));
                        }
                    }
                    logger.LogInformation("Retrieved all ComputerCase data from the database");
                    return Ok(new { data = computerCases });
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"ComputerCase data did not get from database. Exception: {ex}");
                return NotFound(new { error = ex.Message });
            }
        }

        private static string TransformCharacteristicsToString(string[] characteristics, string prev = "")
        {
            string stringOfCharacteristics = "";
            for (int i = 0; i < characteristics.Length; i++)
            {
                if (i != 0)
                {
                    stringOfCharacteristics += $", {prev + characteristics[i]}";
                }
                else
                {
                    stringOfCharacteristics += prev + characteristics[i];
                }
            }

            return stringOfCharacteristics;
        }

        private async Task<Dictionary<string, object>> JoinProductInfo(int productId, string databaseName, Component<string> productBase)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    Dictionary<string, object> product = new Dictionary<string, object>();
                    var productInfo = connection.Query<ComputerCaseInfo>($"SELECT * FROM public.{databaseName} WHERE product_id = {productId}");
                    PropertyInfo[] properties = typeof(Component<string>).GetProperties();
                    properties = properties.Concat(typeof(ComputerCaseInfo).GetProperties()).ToArray();
                    foreach (var property in properties)
                    {
                        if (property.GetValue(productBase) != null)
                        {
                            product[property.Name] = property.GetValue(productBase)!;
                        }
                        else
                        {
                            product[property.Name] = property.GetValue(productInfo);
                        }
                    }

                    return product;
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString());
                return new Dictionary<string, object>();
            }
        }

        private string TransformToCamelCase(string[] characteristics, string[] snakeCaseCharacteristics)
        {
            string[] camelStrings = new string[snakeCaseCharacteristics.Length];

            for (int i = 0;  i < snakeCaseCharacteristics.Length; i++)
            {
                List<char> snakeElemsList = new List<char>(snakeCaseCharacteristics[i].ToCharArray());

                for(int j = 0; j < snakeElemsList.Count; j++)
                {
                    if (snakeElemsList[j] == '_')
                    {
                        snakeElemsList[j + 1] = Char.ToUpper(snakeElemsList[j + 1]);
                        snakeElemsList.RemoveAt(j);
                    }
                }

                string camelString = new string(snakeElemsList.ToArray());
                camelStrings[i] = $"{snakeCaseCharacteristics[i]} AS {camelString}";
                
            }

            string characteristicsString = TransformCharacteristicsToString(characteristics);
            
            if(camelStrings.Length > 0)
            {
                string camelString = TransformCharacteristicsToString(camelStrings);
                characteristicsString = characteristicsString + ", " + camelString;
            }

            return characteristicsString;
        }

        private Dictionary<string, string[]> SeparateCharacteristics(string[] characteristics)
        {
            List<string> characteristicsWithoutSnakes = new List<string>();
            List<string> chacteristicsWithSnakes = new List<string>();
            for (int i = 0; i < characteristics.Length;  i++) { 

                if (characteristics[i].Contains("_"))
                {
                    chacteristicsWithSnakes.Add(characteristics[i]);
                }

                else
                {
                    characteristicsWithoutSnakes.Add(characteristics[i]);
                }
            }

            return new Dictionary<string, string[]>() { { "characteristicsWithoutSnakes", characteristicsWithoutSnakes.ToArray()},
                { "characteristicsWithSnakes", chacteristicsWithSnakes.ToArray()} };
        }
    }
}
