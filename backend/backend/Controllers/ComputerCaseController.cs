using backend.Entities;
using backend.Entities.CommentEntities;
using backend.Entities.ComponentsInfo;
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
            return await GetAllComponents<ComputerCaseInfo>(limit, offset, "computer_cases", ["material", "width", "height", "depth"]);
        }

        [HttpGet("getComputerCase/{id}")]
        public async Task<IActionResult> GetComputerCaseById (int id)
        {
            return await getComponent<ComputerCaseInfo>(id, "computer_cases", ["material", "width", "height", "depth"]);
        }

        [HttpPut("updateComputerCase/{id}")]
        public async Task<IActionResult> UpdateComputerCase(int id, [FromForm] ComputerCase<IFormFile> computerCase, [FromQuery] bool isUpdated)
        {
            computerCase.ProductId = id;
            return await UpdateComponent<ComputerCase<IFormFile>>(computerCase, isUpdated, "computer_cases", ["material", "width", "height", "depth"]);
        }

        [HttpDelete("deleteComputerCase/{id}")]
        public async Task<IActionResult> DeleteComputerCase(int id)
        {
            return await DeleteComponent(id);
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

