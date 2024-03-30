using backend.Entities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Runtime.Intrinsics.Arm;
using backend.Entities.CommentEntities;
using backend.Entities.User;
using backend.Entities.ComponentsInfo;
using System.Diagnostics;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamController : ComponentController
    {
        
        public RamController(ILogger<RamController> logger):base(logger)
        {
           
        }

        [HttpPost("createRam")]
        public async Task<IActionResult> CreateRam([FromForm] RAM<IFormFile> ram)
        {

           
            if (ram.Frequency < 0 || ram.Frequency > 100000)
            {
                return BadRequest(new { error = "Frequency must be between 0 and 100000" });
            }

            if (ram.Timings < 0 || ram.Timings > 10000)
            {
                return BadRequest(new { error = "Timings must be between 0 and 10000" });
            }

            if (ram.Capacity_db < 0 || ram.Capacity_db > 10000)
            {
                return BadRequest(new { error = "Capacity_db must be between 0 and 10000" });
            }

            ram.Likes = 0;
            ram.ProductType = "ram";
            return await CreateComponent<RAM<IFormFile>>(ram, ["frequency", "timings", "capacity_db"], "ram");
        }

        [HttpGet("getRam/{id}")]
        public async Task<IActionResult> GetRamById(int id)
        {
            return await GetComponent<RAM<string>>(id, "power_unit", ["battery", "voltage"]);
        }

        [HttpPut("updateRam/{id}")]
        public async Task<IActionResult> UpdateRam(int id, [FromForm] RAM<IFormFile> ram, [FromQuery] bool isUpdated)
        {
            ram.ProductId = id;
            ram.ProductType = "ram";
            return await UpdateComponent<RAM<IFormFile>>(ram, isUpdated, "ram",
                ["frequency", "timings", "capacity_db"]);
        }

        [HttpDelete("deleteRam/{id}")]
        public async Task<IActionResult> DeleteRam(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("getAllRam")]
        public async Task<IActionResult> GetAllRams(int limit, int offset)
        {
            return await GetAllComponents<RAM<string>>(limit, offset, "ram", ["frequency", "timings", "capacity_db"]);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRam(string keyword, int limit = 1, int offset = 0)
        {
            return await SearchComponent(keyword, limit, offset);
        }

        [HttpGet("FilterByCountry")]
        public async Task<IActionResult> FilterByCountry(string country, int limit, int offset)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var ram = connection.Query<RAM<string>>(@"SELECT * FROM public.ram " +
                    "WHERE country = @Country " +
                    "LIMIT @Limit OFFSET @Offset", new { Country = country, Limit = limit, Offset = offset });

                    return Ok(new { ram });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with country filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("FilterByBrand")]
        public async Task<IActionResult> FilterByBrand(string brand, int limit, int offset)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var ram = connection.Query<RAM<string>>(@"SELECT * FROM public.ram " +
                    "WHERE brand = @Brand " +
                    "LIMIT @Limit OFFSET @Offset", new { Brand = brand, Limit = limit, Offset = offset });

                    return Ok(new { ram });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with brand filter");
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet("FilterByModel")]
        public async Task<IActionResult> FilterByModel(string model, int limit, int offset)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var ram = connection.Query<RAM<string>>(@"SELECT * FROM public.ram " +
                    "WHERE model = @Model " +
                    "LIMIT @Limit OFFSET @Offset", new { Model = model, Limit = limit, Offset = offset });

                    return Ok(new { ram });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with model filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("FilterByPrice")]
        public async Task<IActionResult> FilterByPrice(int minPrice, int maxPrice, int limit, int offset)
        {
            try
            {
                if (minPrice < 0 || maxPrice < 0)
                {
                    return BadRequest(new { error = "price must not be 0" });
                }

                if (maxPrice < minPrice)
                {
                    return BadRequest(new { error = "maxPrice could not be less than minPrice" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var ram = connection.Query<RAM<string>>(@"SELECT * FROM public.ram " +
                    "WHERE price >=  @MinPrice AND price <= @MaxPrice " +
                    "LIMIT @Limit OFFSET @Offset", new
                    {
                        MinPrice = minPrice,
                        MaxPrice = maxPrice,
                        Limit = limit,
                        Offset = offset
                    });

                    return Ok(new { ram });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with price filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("FilterByFrequency")]
        public async Task<IActionResult> FilterByFrequency(int minFrequency, int maxFrequency, int limit, int offset)
        {
            try
            {
                if (minFrequency < 0 || maxFrequency < 0)
                {
                    return BadRequest(new { error = "frequency must not be 0" });
                }

                if (maxFrequency < minFrequency)
                {
                    return BadRequest(new { error = "maxFrequecny could not be less than minFrequency" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var ram = connection.Query<RAM<string>>(@"SELECT * FROM public.ram " +
                    "WHERE frequency >=  @MinFrequency AND frequency <= @MaxFrequency " +
                    "LIMIT @Limit OFFSET @Offset", new
                    {
                        MinFrequency = minFrequency,
                        MaxFrequency = maxFrequency,
                        Limit = limit,
                        Offset = offset
                    });

                    return Ok(new { ram });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with frequency filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("FilterByCapacity")]
        public async Task<IActionResult> FilterByCapacity(int minCapacity, int maxCapacity, int limit, int offset)
        {
            try
            {
                if (minCapacity < 0 || maxCapacity < 0)
                {
                    return BadRequest(new { error = "capacity_db must not be 0" });
                }

                if (maxCapacity < minCapacity)
                {
                    return BadRequest(new { error = "maxCapacity could not be less than minCapacity" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var ram = connection.Query<RAM<string>>(@"SELECT * FROM public.ram " +
                    "WHERE capacity_db >=  @MinCapacity AND capacity_db <= @MaxCapacity " +
                    "LIMIT @Limit OFFSET @Offset", new
                    {
                        MinCapacity = minCapacity,
                        MaxCapacity = maxCapacity,
                        Limit = limit,
                        Offset = offset
                    });

                    return Ok(new { ram });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with capacity_db filter");
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
            return await DeleteComment(productId, commentId, "ram");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int productId, int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "ram", productId);
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "ram");
        }

        [HttpPut("likeRam/{id}")]
        public async Task<IActionResult> LikeRam(int id, User user)
        {
            return await LikeComponent(id, user, "ram");
        }
    }
}
