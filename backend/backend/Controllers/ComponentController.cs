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
    public class ComponentController : ControllerBase
    {
        protected readonly ILogger<ComponentController> logger;
        protected readonly string connectionString;
        
        public ComponentController(ILogger<ComponentController> logger)
        {
            this.logger = logger;
            DotNetEnv.Env.Load();
            connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        }

        protected async Task<IActionResult> AddComment(Comment comment)
        {
            try
            {

                if (string.IsNullOrEmpty(comment.Content))
                {
                    return BadRequest(new { error = "Content cannot be empty" });
                }

                comment.CommentDate = DateTime.Now;

                int insertedId;

                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    var data = new
                    {
                        content = comment.Content,
                        commentDate = comment.CommentDate,
                        componentId = comment.ComponentId,
                        component = comment.Component,
                    };
                    
                    await connection.OpenAsync();

                    logger.LogInformation("Connection started");

                    insertedId = await connection.QueryFirstOrDefaultAsync<int>(
                        $"INSERT INTO public.comment (content, comment_date, component_id, component)" +
                        "VALUES (@content, @commentDate, @commentId, @component) RETURNING id", data);

                    return Ok(new { id = insertedId, data });
                }


            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while adding comment");
                return BadRequest(new { error = "An error occurred while adding comment" });
            }
        }

        protected async Task<IActionResult> UpdateComment(Comment comment)
        {
            try
            {

                if (string.IsNullOrEmpty(comment.Content))
                {
                    return BadRequest(new { error = "Content cannot be empty" });
                }

                comment.CommentDate = DateTime.Now;

                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    var data = new
                    {
                        id = comment.Id,
                        content = comment.Content,
                        commentDate = comment.CommentDate,
                        componentId = comment.ComponentId,
                        component = comment.Component,
                    };

                    await connection.OpenAsync();

                    logger.LogInformation("Connection started");

                    int updatedComment = await connection.ExecuteAsync(
                        $"UPDATE public.comment " +
                        "SET content = @content, comment_date = @commentDate " +
                        "WHERE id = @id AND component_id = @componentId AND component = @component", data);

                    if (updatedComment == 0)
                    {
                        return NotFound(new { error = "Comment not found" });
                    }

                    return Ok(new { id = comment.Id, data });
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while updating comment");
                return BadRequest(new { error = ex.Message });
            }
        }

        protected async Task<IActionResult> DeleteComment(int productId, int commentId, string component)
        {
            try
            {
                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    logger.LogInformation("Connection started");

                    int deletedComment = await connection.ExecuteAsync(
                        $"DELETE FROM public.comment " +
                        "WHERE id = @commentId AND component_id = @productId AND component = @Component",
                        new { productId, commentId, component });

                    if (deletedComment == 0)
                    {
                        return NotFound(new { error = "Comment not found" });
                    }
                }

                return Ok(new { id = productId, comment_id = commentId, Component = component });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while deleting comment");
                return BadRequest(new { error = ex.Message });
            }
        }

        protected async Task<IActionResult> GetAllComments(int limit, int offset, string component, int productId)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var comments = connection.Query<Comment>($"SELECT * FROM public.comment WHERE" +
                        $" component = @Component AND component_id = @ProductId " +
                                                             "LIMIT @Limit OFFSET @Offset",
                        new { Limit = limit, Offset = offset, Component = component, ProductId = productId });

                    logger.LogInformation("Retrieved all ComputerCase data from the database");

                    return Ok(new { comments });
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with get comments");
                return BadRequest(new { error = ex.Message });
            }
        }

        protected async Task<IActionResult> GetComment(int productId, int commentId, string component)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var comment = await connection.QueryFirstOrDefaultAsync<Comment>(
                        $"SELECT * FROM public.comment WHERE id = @commentId AND " +
                        $"component_id = @productId AND component = @Component",
                        new { commentId = commentId, productId = productId, Component = component });

                    if (comment == null)
                    {
                        return NotFound(new { error = "Comment not found" });
                    }


                    return Ok(new { id = commentId, comment });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error with retrieving comment by ID");
                return BadRequest(new { error = ex.Message });
            }
        }

        protected async Task<IActionResult> LikeComponent(int id, User user, string component)
        {
            try
            {

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    int? userId = user.Id;

                    bool isUserExist = await connection.ExecuteScalarAsync<bool>("SELECT EXISTS (SELECT 1 FROM public.users " +
                        "WHERE Id = @UserId)",
                        new { UserId = userId });

                    if (!isUserExist)
                    {
                        return StatusCode(500, new { error = $"User with {userId} not Found" });
                    }

                    bool isUserLiked = await connection.ExecuteScalarAsync<bool>("SELECT EXISTS (SELECT 1 " +
                        "FROM public.like WHERE UserId = @UserId AND ComponentId = @ComponentId AND Component = @Component)",
                        new { UserId = userId, ComponentId = id, Component = component });

                    var currentLikes = await connection.ExecuteScalarAsync<int>($"SELECT likes FROM public.{component} " +
                        $"WHERE Id = @Id",
                        new { Id = id });

                    if (!isUserLiked)
                    {

                        int likeid = connection.QueryFirstOrDefault<int>("INSERT INTO public.like (userid," +
                            " componentid, component) " +
                            "VALUES (@userid, @componentid, component) RETURNING id",
                            new { userid = userId, assemblyid = id, component = component });

                        var updatedLikes = currentLikes + 1;

                        await connection.ExecuteAsync($"UPDATE public.{component} SET likes = @Likes WHERE Id = @Id",
                            new { Likes = updatedLikes, Id = id });

                        logger.LogInformation($"Likes for {component} with Id {id} was plused");

                        // ИНдекс сборки, которой поставили лайк
                        return Ok(new { id = id });
                    }

                    else
                    {
                        connection.Execute("DELETE FROM public.like WHERE UserId = @UserId AND ComponentId = @ComponentId " +
                            "AND Component = @Component",
                            new { UserId = userId, AssemblyId = id });

                        if (currentLikes > 0)
                        {
                            var updatedLikes = currentLikes - 1;
                            await connection.ExecuteAsync($"UPDATE public.{component} SET likes = @Likes WHERE Id = @Id",
                            new { Likes = updatedLikes, Id = id });
                        }
                        else
                        {
                            await connection.ExecuteAsync($"UPDATE public.{component} SET likes = @Likes WHERE Id = @Id",
                            new { Likes = currentLikes, Id = id });
                        }

                        logger.LogInformation($"Likes for {component} with Id {id} was minused");

                        // ИНдекс сборки, которой поставили лайк
                        return Ok(new { id = id });
                    }

                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to like Assembly with Id {id}. Exception: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
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

        protected async Task<IActionResult> GetAllComponents<T>(int limit, int offset, string databaseName) where T: Component<string>
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    var computerCasesBase = connection.Query<Component<string>>($"SELECT * FROM public.products WHERE product_type = '{databaseName}'", 
                        new {Limit = limit, Offset = offset});
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
            catch(Exception ex)
            {
                logger.LogError($"ComputerCase data did not get from database. Exception: {ex}");
                return NotFound(new {error = ex.Message});
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
    }
}
