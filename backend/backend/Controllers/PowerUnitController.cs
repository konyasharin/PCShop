using backend.Entities;
using backend.Entities.CommentEntities;
using backend.Entities.ComponentsInfo;
using backend.Entities.User;
using backend.UpdatedEntities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerUnitController : ComponentController
    {
       
        public PowerUnitController(ILogger<PowerUnitController> logger):base(logger)
        {
        }

        [HttpPost("createPowerUnit")]
        public async Task<IActionResult> CreatePowerUnit([FromForm] PowerUnit<IFormFile> powerunit)
        {


            if (powerunit.Voltage < 0 || powerunit.Voltage > 50000)
            {
                return BadRequest(new { error = "Voltage must be between 0 and 50000" });
            }

            powerunit.Likes = 0;
            powerunit.ProductType = "power_unit";

            return await CreateComponent<PowerUnit<IFormFile>>(powerunit, ["battery", "voltage"], "power_unit");
        }

        [HttpGet("getPowerUnit/{id}")]
        public async Task<IActionResult> GetPowerUnitById(int id)
        {
            return await GetComponent<PowerUnitInfo>(id, "power_unit", ["battery", "voltage"]);
        }

        [HttpPut("updatePowerUnit/{id}")]
        public async Task<IActionResult> UpdatePowerUnit(int id, [FromForm] PowerUnit<IFormFile> powerUnit, [FromQuery] bool isUpdated)
        {
            powerUnit.ProductId = id;
            powerUnit.ProductType = "power_unit";
            return await UpdateComponent<PowerUnit<IFormFile>>(powerUnit, isUpdated, "power_unit", ["battery", "voltage"]);
        }

        [HttpDelete("deletePowerUnit/{id}")]
        public async Task<IActionResult> DeletePowerUnit(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("getAllPowerUnits")]
        public async Task<IActionResult> GetAllPowerUnits(int limit, int offset)
        {
            return await GetAllComponents<PowerUnitInfo>(limit, offset, "power_unit", ["battery", "voltage"]);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPowerUnit(string keyword, int limit = 1, int offset = 0)
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

                    var powerUnits = connection.Query<PowerUnit<string>>(@"SELECT * FROM public.power_unit " +
                    "WHERE country = @Country " +
                    "LIMIT @Limit OFFSET @Offset", new { Country = country, Limit = limit, Offset = offset });

                    return Ok(new { powerUnits });

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

                    var powerUnits = connection.Query<PowerUnit<string>>(@"SELECT * FROM public.power_unit " +
                    "WHERE brand = @Brand " +
                    "LIMIT @Limit OFFSET @Offset", new { Brand = brand, Limit = limit, Offset = offset });

                    return Ok(new { powerUnits });

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

                    var powerUnits = connection.Query<PowerUnit<string>>(@"SELECT * FROM public.power_unit " +
                    "WHERE model = @Model " +
                    "LIMIT @Limit OFFSET @Offset", new { Model = model, Limit = limit, Offset = offset });

                    return Ok(new { powerUnits });

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

                    var powerUnits = connection.Query<PowerUnit<string>>(@"SELECT * FROM public.power_unit " +
                    "WHERE price >=  @MinPrice AND price <= @MaxPrice " +
                    "LIMIT @Limit OFFSET @Offset", new
                    {
                        MinPrice = minPrice,
                        MaxPrice = maxPrice,
                        Limit = limit,
                        Offset = offset
                    });

                    return Ok(new { powerUnits });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with price filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("FilterByVoltage")]
        public async Task<IActionResult> FilterByVoltage(int minVoltage, int maxVoltage, int limit, int offset)
        {
            try
            {
                if (minVoltage < 0 || maxVoltage < 0)
                {
                    return BadRequest(new { error = "voltage must not be 0" });
                }

                if (maxVoltage < minVoltage)
                {
                    return BadRequest(new { error = "maxVoltage could not be less than minVoltage" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var motherBoards = connection.Query<PowerUnit<string>>(@"SELECT * FROM public.power_unit " +
                    "WHERE voltage >=  @MinVoltage AND voltage <= @MaxVoltage " +
                    "LIMIT @Limit OFFSET @Offset", new
                    {
                        MinVoltage = minVoltage,
                        MaxVoltage = maxVoltage,
                        Limit = limit,
                        Offset = offset
                    });

                    return Ok(new { motherBoards });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with voltage filter");
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
            return await DeleteComment(productId, commentId, "power_unit");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int productId, int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "power_unit", productId);
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "power_unit");
        }

        [HttpPut("likePowerUnit/{id}")]
        public async Task<IActionResult> LikePowerUnit(int id, User user)
        {
            return await LikeComponent(id, user, "power_unit");
        }
    }
}
