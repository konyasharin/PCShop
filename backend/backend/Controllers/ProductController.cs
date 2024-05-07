using backend.Entities.CommentEntities;
using backend.Entities.User;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        protected readonly ILogger<ProductController> logger;
        protected readonly string connectionString;
        
        public ProductController(ILogger<ProductController> logger)
        {
            this.logger = logger;
            DotNetEnv.Env.Load();
            connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        }
        
        /// <summary>
        /// Метод для добавления комментария.
        /// </summary>
        /// <param name="comment">Данные комментария.</param>
        /// <returns>Результат операции добавления комментария.</returns>
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
        
        /// <summary>
        /// Метод для обновления комментария.
        /// </summary>
        /// <param name="comment">Данные комментария.</param>
        /// <returns>Результат операции обновления комментария.</returns>
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
        
        /// <summary>
        /// Метод для удаления комментария.
        /// </summary>
        /// <param name="productId">Идентификатор продукта.</param>
        /// <param name="commentId">Идентификатор комментария.</param>
        /// <param name="component">Компонент коментария.</param>
        /// <returns>Результат операции удаления комментария.</returns>
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
        
        
        /// <summary>
        /// Получает все комментарии для определенного продукта и компонента с учетом ограничения и смещения.
        /// </summary>
        /// <param name="limit">Ограничение количества комментариев.</param>
        /// <param name="offset">Смещение комментариев.</param>
        /// <param name="component">Компонент коментария.</param>
        /// <param name="productId">Идентификатор продукта.</param>
        /// <returns>Все комментарии для определенного продукта и компонента.</returns>
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
        
        
        /// <summary>
        /// Получает комментарий по его идентификатору для определенного продукта и компонента.
        /// </summary>
        /// <param name="productId">Идентификатор продукта.</param>
        /// <param name="commentId">Идентификатор комментария.</param>
        /// <param name="component">Компонент коментария.</param>
        /// <returns>Комментарий для определенного продукта и компонента.</returns>
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
                        new { CommentId = commentId, ProductId = productId, Component = component });

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
        
        
        /// <summary>
        /// Лайкает компонент с указанным идентификатором для пользователя и компонента.
        /// </summary>
        /// <param name="id">Идентификатор компонента.</param>
        /// <param name="user">Пользователь, который ставит лайк.</param>
        /// <param name="component">Компонент, для которого ставится лайк.</param>
        /// <returns>Идентификатор компонента, для которого был поставлен лайк.</returns>
        protected async Task<IActionResult> LikeComponent(int id, User user, string component)
        {
            try
            {

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    int? userId = user.Id;

                    bool isUserExist = await connection.ExecuteScalarAsync<bool>("SELECT EXISTS (SELECT 1 FROM public.user " +
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