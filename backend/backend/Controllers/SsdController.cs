using backend.Entities;
using backend.UpdatedEntities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Runtime.Intrinsics.X86;
using backend.Entities.CommentEntities;
using System.Runtime.Intrinsics.Arm;
using backend.Entities.User;
using backend.Entities.ComponentsInfo;
using System.Diagnostics;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SsdController : ComponentController
    {
       
        public SsdController(ILogger<SsdController> logger) : base(logger)
        {
        }

        [HttpPost("createSsd")]
        public async Task<IActionResult> CreateSsd([FromForm] SSD<IFormFile> ssd)
        {
            
            if (ssd.Capacity < 0 || ssd.Capacity > 10000)
            {
                return BadRequest(new { error = "Capacity must be between 0 and 10000" });
            }

            ssd.Likes = 0;
            ssd.ProductType = "ssd";
            return await CreateComponent<SSD<IFormFile>>(ssd, ["capacity"], "ssd");
        }

        [HttpGet("getSsd/{id}")]
        public async Task<IActionResult> GetSsdById(int id)
        {
            return await getComponent<PowerUnitInfo>(id, "power_unit", ["battery", "voltage"]);
        }

        [HttpPut("updateSsd/{id}")]
        public async Task<IActionResult> UpdateSsd(int id, [FromForm] SSD<IFormFile> ssd, [FromQuery] bool isUpdated)
        {
            ssd.ProductId = id;
            ssd.ProductType = "ssd";
            return await UpdateComponent<SSD<IFormFile>>(ssd, isUpdated, "ssd", ["capacity"]);
        }

        [HttpDelete("deleteSsd/{id}")]
        public async Task<IActionResult> DeleteSsd(int id)
        {
            return await DeleteComponent(id);
        }


        [HttpGet("getAllSsd")]
        public async Task<IActionResult> GetAllSsd(int limit, int offset)
        {
            return await GetAllComponents<ProcessorInfo>(limit, offset, "processor", ["capacity"]);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchSsd(string keyword, int limit = 1, int offset = 0)
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

                    var ssd = connection.Query<SSD<string>>(@"SELECT * FROM public.ssd " +
                    "WHERE country = @Country " +
                    "LIMIT @Limit OFFSET @Offset", new { Country = country, Limit = limit, Offset = offset });

                    return Ok(new { ssd });

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

                    var ssd = connection.Query<SSD<string>>(@"SELECT * FROM public.ssd " +
                    "WHERE brand = @Brand " +
                    "LIMIT @Limit OFFSET @Offset", new { Brand = brand, Limit = limit, Offset = offset });

                    return Ok(new { ssd });

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

                    var ssd = connection.Query<SSD<string>>(@"SELECT * FROM public.ssd " +
                    "WHERE model = @Model " +
                    "LIMIT @Limit OFFSET @Offset", new { Model = model, Limit = limit, Offset = offset });

                    return Ok(new { ssd });

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

                    var ram = connection.Query<SSD<string>>(@"SELECT * FROM public.ssd " +
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

                    var ssd = connection.Query<SSD<string>>(@"SELECT * FROM public.ssd " +
                    "WHERE capacity >=  @MinCapacity AND capacity_db <= @MaxCapacity " +
                    "LIMIT @Limit OFFSET @Offset", new
                    {
                        MinCapacity = minCapacity,
                        MaxCapacity = maxCapacity,
                        Limit = limit,
                        Offset = offset
                    });

                    return Ok(new { ssd });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with capacity filter");
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
            return await DeleteComment(productId, commentId, "ssd");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int productId, int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "ssd", productId);
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "ssd");
        }

        [HttpPut("likeSsd/{id}")]
        public async Task<IActionResult> LikeSsd(int id, User user)
        {
            return await LikeComponent(id, user, "ssd");
        }
    }
}
