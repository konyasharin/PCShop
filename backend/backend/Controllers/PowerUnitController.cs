using backend.Entities;
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

            try
            {
                string imagePath = BackupWriter.Write(powerunit.Image);
                

                if (powerunit.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }

                if (powerunit.Voltage < 0 || powerunit.Voltage > 50000)
                {
                    return BadRequest(new { error = "Voltage must be between 0 and 50000" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var data = new
                    {
                        brand = powerunit.Brand,
                        model = powerunit.Model,
                        country = powerunit.Country,
                        battery = powerunit.Battery,
                        voltage = powerunit.Voltage,
                        price = powerunit.Price,
                        description = powerunit.Description,
                        image = imagePath,

                    };

                    connection.Open();
                    int id = connection.QueryFirstOrDefault<int>("INSERT INTO public.power_unit (brand, model, country, battery, voltage," +
                        "price, description, image)" +
                        "VALUES (@brand, @model, @country, @battery, @voltage, @price, @description, @image) RETURNING id", data);

                    logger.LogInformation("powerUnit data saved to database");
                    return Ok(new { id = id, data });
                }
            }
            catch (Exception ex)
            {
                logger.LogError("PowerUnit data did not save in database");
                return BadRequest(new { error = ex.Message });
            }
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


                    var powerUnit = connection.QueryFirstOrDefault<PowerUnit<string>>("SELECT * FROM public.power_unit WHERE Id = @Id",
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
                    };

                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("UPDATE public.power_unit SET Brand = @brand, Model = @model, Country = @country, Battery = @battery," +
                        " Voltage = @voltage," +
                        " Price = @price, Description = @description, Image = @image WHERE Id = @id", data);

                    logger.LogInformation("PowerUnit data updated in the database");

                    return Ok(new { id = id, data });

                }

                connection.Open();
                logger.LogInformation("Connection started");

                connection.Execute("UPDATE public.power_unit SET Brand = @brand, Model = @model, Country = @country, Battery = @battery," +
                    " Voltage = @voltage," +
                    " Price = @price, Description = @description, Image = @image WHERE Id = @id", updatedPowerUnit);

                logger.LogInformation("PowerUnit data updated in the database");

                return Ok(new {id=id, updatedPowerUnit});
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

                    filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.power_unit WHERE Id = @id", new { Id = id });
                    BackupWriter.Delete(filePath);

                    connection.Execute("DELETE FROM public.power_unit WHERE Id = @id", new { id });

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
    }
}
