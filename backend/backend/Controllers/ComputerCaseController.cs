using backend.Entities;
using backend.Entities.CommentEntities;
using backend.Entities.User;
using backend.UpdatedEntities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerCaseController : ComponentController
    {

        public ComputerCaseController(ILogger<ComputerCaseController> logger):base(logger)
        {
        }

        [HttpPost("createComputerCase")]
        public async Task<IActionResult> CreateCreateCase([FromForm] ComputerCase<IFormFile> computerCase)
        {
                if (computerCase.Width < 10 || computerCase.Width > 100)
                {
                    return BadRequest(new { error = "Width must be between 10 and 100" });
                }
                if (computerCase.Height < 30 || computerCase.Height > 150)
                {
                    return BadRequest(new { error = "Height must be between 30 and 150" });
                }
                if (computerCase.Depth < 20 || computerCase.Depth > 100)
                {
                    return BadRequest(new { error = "Depth must be between 20 and 100" });
                }
                return await CreateComponent<ComputerCase<IFormFile>>(computerCase, ["material", "width", "height", "depth"], "computer_cases");
        }

        [HttpGet("getAllComputerCases")]
        public async Task<IActionResult> GetAllComputerCases(int limit, int offset)
        {
            return await GetAllComponents<ComputerCase<string>>(limit, offset, "computer_cases");
        }

        [HttpGet("getComputerCase/{id}")]
        public async Task<IActionResult> GetComputerCaseById (int id)
        {
            try
            {
                

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    
                    var computerCase = connection.QueryFirstOrDefault<ComputerCase<string>>("SELECT * FROM public.computer_case " +
                        "WHERE Id = @Id",
                        new { Id = id });

                    if (computerCase != null)
                    {
                        logger.LogInformation($"Retrieved ComputerCase with Id {id} from the database");
                        return Ok(new { id = id, computerCase});

                    }
                    else
                    {
                        logger.LogInformation($"ComputerCase with Id {id} not found in the database");
                        return NotFound(new {error = $"Not found ComputerCase with {id}"});
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve ComputerCase data from the database. \nException {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("updateComputerCase/{id}")]
        public async Task<IActionResult> UpdateComputerCase(int id, [FromForm] UpdatedComputerCase updatedComputerCase)
        {
            try
            {

                if (updatedComputerCase.Width < 10 || updatedComputerCase.Width > 100)
                {
                    return BadRequest(new { error = "Width must be between 10 and 100" });
                }

                if (updatedComputerCase.Height < 30 || updatedComputerCase.Height > 150)
                {
                    return BadRequest(new { error = "Height must be between 30 and 150" });
                }

                if (updatedComputerCase.Depth < 20 || updatedComputerCase.Depth > 100)
                {
                    return BadRequest(new { error = "Depth must be between 20 and 100" });
                }

                if (updatedComputerCase.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }

                if (updatedComputerCase.Amount < 0)
                {
                    return BadRequest(new { error = "Amount must not be less than 0" });
                }

                if (updatedComputerCase.Power < 0 || updatedComputerCase.Power > 10)
                {
                    return BadRequest(new { error = "Power must be between 0 and 10" });
                }

                if(updatedComputerCase.Likes < 0)
                {
                    return BadRequest(new { error = "Likes must not be less than 0" });
                }


                string imagePath = string.Empty;

                await using var connection = new NpgsqlConnection(connectionString);
                {

                    string filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.computer_case" +
                        " WHERE Id = @id");

                    if (updatedComputerCase.updated)
                    {
                        
                        BackupWriter.Delete(filePath);
                        imagePath = BackupWriter.Write(updatedComputerCase.Image);
                    }
                    else 
                    {
                        imagePath = filePath;
                    }

                    var data = new
                    {
                        id = id,
                        brand = updatedComputerCase.Brand,
                        model = updatedComputerCase.Model,
                        country = updatedComputerCase.Country,
                        material = updatedComputerCase.Material,
                        width = updatedComputerCase.Width,
                        height = updatedComputerCase.Height,
                        depth = updatedComputerCase.Depth,
                        price = updatedComputerCase.Price,
                        description = updatedComputerCase.Description,
                        image = imagePath,
                        amount = updatedComputerCase.Amount,
                        power = updatedComputerCase.Power,
                        likes = updatedComputerCase.Likes,
                    };

                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("UPDATE public.computer_case SET Brand = @brand, Model = @model, Country = @country," +
                        " Material = @material, Width = @width, Height = @height," +
                        " Depth = @depth, Price = @price, Description = @description," +
                        " Image = @image, Amount = @amount, Power = @power, Likes = @likes WHERE Id = @id", data);

                    logger.LogInformation("ComputerCase data updated in the database");

                    return Ok(new { id = id, data });

                }

                
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update ComputerCase data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("deleteComputerCase/{id}")]
        public async Task<IActionResult> DeleteComputerCase(int id)
        {
            try
            {
                string filePath;

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.computer_case WHERE Id = @id",
                        new { Id = id });
                    BackupWriter.Delete(filePath);

                    connection.Execute("DELETE FROM public.computer_case WHERE Id = @id", new { id });

                    connection.Execute("DELETE FROM public.like WHERE componentid = @id AND component = computer_case",
                        new { id });

                    connection.Execute("DELETE FROM public.comment WHERE component_id = @id AND component = computer_case",
                        new { id });

                    logger.LogInformation("ComputerCase data deleted from the database");

                    return Ok(new {id = id});
                }

            }
            catch(Exception ex)
            {
                logger.LogError($"Failed to delete ComputerCase data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchComputerCase(string keyword, int limit = 1, int offset = 0)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var computerCases = connection.Query<ComputerCase<string>>(@"SELECT * FROM public.computer_case " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT @Limit OFFSET @Offset", new { Keyword = "%" + keyword + "%", Limit = limit, Offset = offset });

                    return Ok(new { computerCases });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with search");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> FilterComputerCase(string country, string brand,
            string model, int minPrice, int maxPrice,
            int limit = 1, int offset = 0)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var computerCases = connection.Query<ComputerCase<string>>(@"SELECT * FROM public.computer_case " +
                    "WHERE country = @Country AND brand = @Brand AND model = @Model " +
                    "AND price >= @MinPrice AND price <= @MaxPrice " +
                    "LIMIT @Limit OFFSET @Offset", new { Country = country, Brand = brand, 
                        Model = model, Limit = limit, MinPrice = minPrice, MaxPrice = maxPrice, Offset = offset });

                    return Ok(new { computerCases });

                }
            }
            catch(Exception ex)
            {
                logger.LogError("Error with country filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        

        [HttpPost("addComment")]
        public async Task<IActionResult> AddComputerCaseComment(Comment computerCaseComment)
        {
            return await AddComment(computerCaseComment);
        }

        [HttpPut("updateComment")]
        public async Task<IActionResult> UpdateComputerCaseComment(Comment computerCaseComment)
        {
            return await UpdateComment(computerCaseComment);
        }

        [HttpDelete("{productId}/deleteComment/{commentId}")]
        public async Task<IActionResult> DeleteComputerCaseComment(int productId, int commentId)
        {
            return await DeleteComment(productId, commentId, "computer_case");
        }

        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int productId, int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "computer_case", productId);
        }

        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "computer_case");
        }

        [HttpPut("likeComputerCase/{id}")]
        public async Task<IActionResult> LikeComputerCase(int id, User user)
        {
            return await LikeComponent(id, user, "computer_case");
        }
    }
}

