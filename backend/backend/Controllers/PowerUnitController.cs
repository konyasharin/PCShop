using backend.Entities;
using backend.Entities.CommentEntities;
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

            return await CreateComponent<PowerUnit<IFormFile>>(powerunit, ["battery", "voltage"], "power_unit");
        }

        [HttpGet("getPowerUnit/{id}")]
        public async Task<IActionResult> GetPowerUnitById(int id)
        {
            try
            {

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");


                    var powerUnit = connection.QueryFirstOrDefault<PowerUnit<string>>("SELECT * FROM public.power_unit" +
                        " WHERE Id = @Id",
                        new { Id = id });

                    if (powerUnit != null)
                    {
                        logger.LogInformation($"Retrieved PowerUnit with Id {id} from the database");
                        return Ok(new { id = id, powerUnit });

                    }
                    else
                    {
                        logger.LogInformation($"PowerUnit with Id {id} not found in the database");
                        return NotFound(new {error = $"PowerUnit NotFound with {id}"});
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve PowerUnit data from the database. \nException {ex}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPut("updatePowerUnit/{id}")]
        public async Task<IActionResult> UpdatePowerUnit(int id, [FromForm] UpdatedPowerUnit updatedPowerUnit)
        {
            try
            {

                if (updatedPowerUnit.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }

                if (updatedPowerUnit.Voltage < 0 || updatedPowerUnit.Voltage > 50000)
                {
                    return BadRequest(new { error = "Voltage must be between 0 and 50000" });
                }

                if (updatedPowerUnit.Amount < 0)
                {
                    return BadRequest(new { error = "Amount must be less than 0" });
                }

                if (updatedPowerUnit.Power < 0 || updatedPowerUnit.Power > 10)
                {
                    return BadRequest(new { error = "Power must be between 0 and 10" });
                }

                if(updatedPowerUnit.Likes < 0)
                {
                    return BadRequest(new { error = "Likes must not be less than 0" });
                }

                string imagePath = string.Empty;

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    string filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.power_unit WHERE Id = @id");

                    if (updatedPowerUnit.updated)
                    {

                        BackupWriter.Delete(filePath);
                        imagePath = BackupWriter.Write(updatedPowerUnit.Image);
                    }
                    else
                    {
                        imagePath = filePath;
                    }

                    var data = new
                    {
                        id = id,
                        brand = updatedPowerUnit.Brand,
                        model = updatedPowerUnit.Model,
                        country = updatedPowerUnit.Country,
                        battery = updatedPowerUnit.Battery,
                        voltage = updatedPowerUnit.Voltage,
                        price = updatedPowerUnit.Price,
                        description = updatedPowerUnit.Description,
                        image = imagePath,
                        amount = updatedPowerUnit.Amount,
                        power = updatedPowerUnit.Power,
                        likes = updatedPowerUnit.Likes,
                    };

                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("UPDATE public.power_unit SET Brand = @brand, Model = @model," +
                        " Country = @country, Battery = @battery," +
                        " Voltage = @voltage," +
                        " Price = @price, Description = @description," +
                        " Image = @image, Amount = @amount, Power = @power, Likes = @likes WHERE Id = @id", data);

                    logger.LogInformation("PowerUnit data updated in the database");

                    return Ok(new { id = id, data });

                }

  
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update PowerUnit data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("deletePowerUnit/{id}")]
        public async Task<IActionResult> DeletePowerUnit(int id)
        {
            try
            {
                string filePath;
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.power_unit WHERE Id = @id",
                        new { Id = id });
                    BackupWriter.Delete(filePath);

                    connection.Execute("DELETE FROM public.power_unit WHERE Id = @id", new { id });

                    connection.Execute("DELETE FROM public.like WHERE componentid = @id AND component = power_unit",
                        new { id });

                    connection.Execute("DELETE FROM public.comment WHERE component_id = @id AND component = power_unit",
                        new { id });

                    logger.LogInformation("PowerUnit data deleted from the database");

                    return Ok(new {id=id});
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete PowerUnit data in database. \nException: {ex}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("getAllPowerUnits")]
        public async Task<IActionResult> GetAllPowerUnits(int limit, int offset)
        {
            logger.LogInformation("Get method has started");
            try
            {

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var powerUnits = connection.Query<PowerUnit<string>>("SELECT * FROM public.power_unit LIMIT @Limit OFFSET @Offset",
                        new {Limit = limit, Offset = offset});

                    logger.LogInformation("Retrieved all PowerUnit data from the database");

                    return Ok(new { powerUnits });
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"PowerUnit data did not get gtom database. Exception: {ex}");
                return NotFound(new {error = ex.Message});
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPowerUnit(string keyword, int limit = 1, int offset = 0)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var powerUnits = connection.Query<PowerUnit<string>>(@"SELECT * FROM public.power_unit " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT @Limit @Offset", new { Keyword = "%" + keyword + "%", Limit = limit, Offset = offset });

                    return Ok(new { powerUnits });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with search");
                return StatusCode(500, new { error = ex.Message });
            }
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
