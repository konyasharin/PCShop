using backend.Entities.CommentEntities;
using backend.Entities.User;
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

        protected async Task<IActionResult> AddComment(Comment comment, string tableName)
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
                        computerCaseId = comment.ProductId,
                    };
                    
                    await connection.OpenAsync();

                    logger.LogInformation("Connection started");

                    insertedId = await connection.QueryFirstOrDefaultAsync<int>(
                        $"INSERT INTO public.{tableName} (content, comment_date, computer_case_id)" +
                        "VALUES (@content, @commentDate, @computerCaseId) RETURNING id", data);

                    return Ok(new { id = insertedId, data });
                }


            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while adding comment");
                return BadRequest(new { error = "An error occurred while adding comment" });
            }
        }

        protected async Task<IActionResult> UpdateComment(Comment comment, string tableName)
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
                        computerCaseId = comment.ProductId,
                    };

                    await connection.OpenAsync();

                    logger.LogInformation("Connection started");

                    int updatedComment = await connection.ExecuteAsync(
                        $"UPDATE public.{tableName} " +
                        "SET content = @content, comment_date = @commentDate " +
                        "WHERE id = @id AND computer_case_id = @computerCaseId", data);

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

        protected async Task<IActionResult> DeleteComment(int productId, int commentId, string tableName)
        {
            try
            {
                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    logger.LogInformation("Connection started");

                    int deletedComment = await connection.ExecuteAsync(
                        $"DELETE FROM public.{tableName} " +
                        "WHERE id = @commentId AND computer_case_id = @productId", new { productId, commentId });

                    if (deletedComment == 0)
                    {
                        return NotFound(new { error = "Comment not found" });
                    }
                }

                return Ok(new { id = productId, comment_id = commentId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while deleting comment");
                return BadRequest(new { error = ex.Message });
            }
        }

        protected async Task<IActionResult> GetAllComments(int limit, int offset, string tableName)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var comments = connection.Query<Comment>($"SELECT * FROM public.{tableName} " +
                                                             "LIMIT @Limit OFFSET @Offset",
                        new { Limit = limit, Offset = offset });

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

        protected async Task<IActionResult> GetComment(int productId, int commentId, string tableName)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var comment = await connection.QueryFirstOrDefaultAsync<Comment>(
                        $"SELECT * FROM public.{tableName} WHERE id = @commentId AND computer_case_id = @productId",
                        new { commentId = commentId, productId = productId });

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
    }
}
