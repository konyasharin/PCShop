using backend.Entities;
using backend.UpdatedEntities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Drawing;
using backend.Entities.CommentEntities;
using backend.Entities.User;
using backend.Entities.ComponentsInfo;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoolerController : ComponentController
    {

        public CoolerController(ILogger<CoolerController> logger):base(logger)
        { 
        }

        [HttpPost("createCooler")]
        public async Task<IActionResult> CreateCooler([FromForm] Cooler<IFormFile> cooler)
        {
                if (cooler.Speed <= 0 || cooler.Speed > 10000)
                {
                    return BadRequest(new { error = "Speed must be between 0 and 10000" });
                }

                if (cooler.cooler_power < 0 || cooler.cooler_power > 10000)
                {
                    return BadRequest(new { error = "Cooler_power must be between 0 and 10000" });
                }

                cooler.Likes = 0;
                cooler.ProductType = "cooler";

            return await CreateComponent<Cooler<IFormFile>>(cooler, ["speed", "cooler_power"], "coolers");

        }

        [HttpGet("getCooler/{id}")]
        public async Task<IActionResult> GetCoolerById(int id)
        {
            return await GetComponent<CoolerInfo>(id, "cooler", ["speed", "cooler_power"]);
        }

        [HttpPut("updateCooler/{id}")]
        public async Task<IActionResult> UpdateCooler(int id, [FromForm] Cooler<IFormFile> cooler, [FromQuery] bool isUpdated)
        {
            cooler.ProductId = id;
            cooler.ProductType = "cooler";
            return await UpdateComponent<Cooler<IFormFile>>(cooler, isUpdated, "cooler", ["speed", "cooler_power"]);
        }

        [HttpDelete("deleteCooler/{id}")]
        public async Task<IActionResult> DeleteCooler(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("getAllCoolers")]
        public async Task<IActionResult> GetAllCoolers(int limit, int offset)
        {
            return await GetAllComponents<CoolerInfo>(limit, offset, "cooler", ["speed", "cooler_power"]);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCooler(string keyword, int limit = 1, int offset = 0)
        {
            return await SearchComponent(keyword, limit, offset);
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> FilterCooler(string country, string brand, string model, int minPrice, 
            int maxPrice, int minSpeed, int maxSpeed, int limit, int offset)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var coolers = connection.Query<Cooler<string>>(@"SELECT * FROM public.cooler " +
                    "WHERE country = @Country AND brand = @Brand AND model = @Model " +
                    "AND price >=  @MinPrice AND price <= @MaxPrice AND speed >=  @MinSpeed AND speed <= @MaxSpeed " +
                    "LIMIT @Limit OFFSET @Offset", new { Country = country, Brand = brand, Model = model,
                        MinPrice = minPrice, MaxPrice = maxPrice, MinSpeed = minSpeed, MaxSpeed = maxSpeed,
                        Limit = limit, Offset = offset });

                    return Ok(new { coolers });

                }
            }
            catch (Exception ex)
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
            return await DeleteComment(productId, commentId, "cooler");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int productId, int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "cooler", productId);
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "cooler");
        }

        [HttpPut("likeCooler/{id}")]
        public async Task<IActionResult> LikeCooler(int id, User user)
        {
            return await LikeComponent(id, user, "cooler");
        }
    }
}
