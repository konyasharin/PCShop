using System.Globalization;
using System.Reflection;
using backend.Entities;
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

        protected async Task<IActionResult> GetAllComponents<T>(int limit, int offset, string databaseName,
            string[] componentCharacteristics)
        {
            string[] characteristicsBase = ["product_id AS productId", "brand", "model", "country", "price", "description", "image", "amount", "power", "likes", "product_type AS productType"];
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    var componentsBase = 
                        connection.Query<Component<string>>($"SELECT {TransformCharacteristicsToString(characteristicsBase)} " +
                                                            $"FROM public.products WHERE product_type = '{databaseName}' " +
                                                            $"LIMIT {limit} OFFSET {offset}");
                    List<Dictionary<string, object>> components = new List<Dictionary<string, object>>();
                    foreach (var component in componentsBase)
                    {
                        if (component.ProductId != null)
                        {
                            components.Add(await JoinComponentInfo<T>(component.ProductId.Value, databaseName, component, componentCharacteristics));
                        }
                    }
                    return Ok(new { data = components });
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Component data did not get from database. Exception: {ex}");
                return NotFound(new { error = ex.Message });
            }
        }
        
        protected async Task<IActionResult> getComponent<T>(int componentId, string databaseName, string[] componentCharacteristics)
        {
            string[] characteristicsBase = ["product_id AS productId", "brand", "model", "country", "price", "description", "image", "amount", "power", "likes", "product_type AS productType"];
            
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    
                    var componentBase = connection.QueryFirstOrDefault<Component<string>>($"SELECT {TransformCharacteristicsToString(characteristicsBase)} FROM public.products " +
                        $"WHERE product_id = {componentId}");
                    
                    if (componentBase != null)
                    {
                        var response = await JoinComponentInfo<T>(componentId, databaseName, componentBase, componentCharacteristics);
                        logger.LogInformation($"Retrieved component with Id {componentId} from the database");
                        return Ok(new { response });
                    }
                    else
                    {
                        logger.LogInformation($"Component with Id {componentId} not found in the database");
                        return NotFound(new {error = $"Not found component with {componentId}"});
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve component data from the database. \nException {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        protected async Task<IActionResult> UpdateComponent<T>(T component, bool isUpdated, string databaseName,
            string[] characteristics) where T: Component<IFormFile>
        {
            string[] characteristicsBase = ["brand", "model", "country", "price", "description", "image", "amount", "power", "likes", "product_type"];
            try
            {
                if (component.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }

                if (component.Amount < 0)
                {
                    return BadRequest(new { error = "Amount must not be less than 0" });
                }

                if (component.Power < 0 || component.Power > 10)
                {
                    return BadRequest(new { error = "Power must be between 0 and 10" });
                }

                if(component.Likes < 0)
                {
                    return BadRequest(new { error = "Likes must not be less than 0" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    string imagePath;
                    string oldImagePath = connection.QueryFirstOrDefault<string>($"SELECT image FROM public.products WHERE " +
                        $"product_id = {component.ProductId}");
                    if (isUpdated)
                    {
                        BackupWriter.Delete(oldImagePath);
                        imagePath = BackupWriter.Write(component.Image);
                    }
                    else
                    {
                        imagePath = oldImagePath;
                    }

                    PropertyInfo[] properties = typeof(T).GetProperties();
                    Dictionary<string, object> baseComponentCharacteristics = new Dictionary<string, object>();
                    Dictionary<string, object> componentCharacteristics = new Dictionary<string, object>();
                    foreach (var property in properties)
                    {
                        bool isContain = characteristicsBase.Any(characteristic => SnakeCaseToPascalCase(characteristic) == property.Name);
                        if (isContain)
                        {
                            if (property.Name == "Image")
                            {
                                baseComponentCharacteristics[property.Name] = imagePath;
                            }
                            else
                            {
                                baseComponentCharacteristics[property.Name] = property.GetValue(component);      
                            }
                        }
                        else if(property.Name != "ProductId")
                        {
                            componentCharacteristics[property.Name] = property.GetValue(component);
                        }
                    }
                    Dictionary<string, object> response = new Dictionary<string, object>();
                    foreach (var elem in baseComponentCharacteristics)
                    {
                        response[elem.Key] = elem.Value;
                    }
                    foreach (var elem in componentCharacteristics)
                    {
                        response[elem.Key] = elem.Value;
                    }
                    connection.Execute($"UPDATE public.products SET {TransformCharacteristicsToSetString(characteristicsBase)} WHERE product_id = {component.ProductId}", response);
                    connection.Execute($"UPDATE public.{databaseName} SET {TransformCharacteristicsToSetString(characteristics)} WHERE product_id = {component.ProductId}", response);
                    logger.LogInformation("Component data updated in the database");
                    
                    return Ok(new { id = component.ProductId, data = response });
                
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update component data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        protected async Task<IActionResult> DeleteComponent(int id)
        {
            try
            {
                string filePath;

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    filePath = connection.QueryFirstOrDefault<string>($"SELECT image FROM public.products WHERE product_id = {id}");
                    BackupWriter.Delete(filePath);

                    connection.Execute($"DELETE FROM public.products WHERE product_id = {id}");

                    logger.LogInformation("Component data deleted from the database");

                    return Ok(new {id = id});
                }

            }
            catch(Exception ex)
            {
                logger.LogError($"Failed to delete component data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message });
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

        private static string TransformCharacteristicsToSetString(string[] characteristics)
        {
            string stringOfValues = "";
            for (int i = 0; i < characteristics.Length; i++)
            {
                if (i != 0)
                {
                    stringOfValues += $", {characteristics[i]} = @{SnakeCaseToPascalCase(characteristics[i])}";   
                }
                else
                {
                    stringOfValues += $"{characteristics[i]} = @{SnakeCaseToPascalCase(characteristics[i])}"; 
                }
            }

            return stringOfValues;
        }
        
        private static string SnakeCaseToPascalCase(string snakeCase)
        {
            string[] words = snakeCase.Split('_');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words[i]);
            }
            return string.Concat(words);
        }
        
        private async Task<Dictionary<string, object>> JoinComponentInfo<T>(int componentId, string databaseName, Component<string> componentBase,
            string[] componentCharacteristics)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    Dictionary<string, object> component = new Dictionary<string, object>();
                    var componentInfo =
                        connection.QueryFirstOrDefault<T>($"SELECT {TransformCharacteristicsToString(componentCharacteristics)} " +
                                                           $"FROM public.{databaseName} WHERE product_id = {componentId}");
                    List<PropertyInfo> properties = new List<PropertyInfo>(typeof(Component<string>).GetProperties());
                    for (int i = 0; i < typeof(T).GetProperties().Length; i++)
                    {
                        bool isContains = properties.Any(property =>
                            property.Name == typeof(T).GetProperties()[i].Name);
                        if (!isContains)
                        {
                            properties.Add(typeof(T).GetProperties()[i]);
                        }
                    }
                    foreach (var property in properties)
                    {
                        if (typeof(Component<string>).GetProperties().Any(elem => elem.Name == property.Name))
                        {
                            component[property.Name] = property.GetValue(componentBase)!;
                        }
                        else
                        {
                            component[property.Name] = property.GetValue(componentInfo);
                        }
                    }
                    return component;
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString());
                return new Dictionary<string, object>();
            }
        }

        protected async Task<IActionResult> SearchComponent(string keyword, int limit = 1, int offset = 0)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var component = connection.Query<VideoCard<string>>(@"SELECT * FROM public.products " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT @Limit OFFSET @Offset", new { Keyword = "%" + keyword + "%", Limit = limit, Offset = offset });

                    return Ok(new { component });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with search");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        
    }
}
