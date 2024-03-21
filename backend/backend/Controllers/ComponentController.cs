using backend.Entities.CommentEntities;
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
    }
}
