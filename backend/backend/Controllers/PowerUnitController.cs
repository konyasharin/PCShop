using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerUnitController : ControllerBase
    {
        private readonly ILogger<PowerUnitController> logger;

        public PowerUnitController(ILogger<PowerUnitController> logger)
        {
            this.logger = logger;


        }

        [HttpPost("CreatePowerUnit")]
        public async Task<IActionResult> CreatePowerUnit(PowerUnit powerunit)
        {

            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                if (powerunit.Price < 0)
                {
                    return BadRequest("Price must not be less than 0");
                }

                if (powerunit.Voltage < 0 || powerunit.Voltage > 50000)
                {
                    return BadRequest("Voltage must be between 0 and 50000");
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = powerunit.Id,
                        brand = powerunit.Brand,
                        model = powerunit.Model,
                        country = powerunit.Country,
                        battery = powerunit.Battery,
                        voltage = powerunit.Voltage,
                        price = powerunit.Price,
                        description = powerunit.Description,
                        image = powerunit.Image,

                    };

                    connection.Open();
                    logger.LogInformation("Connection started");
                    connection.Execute("INSERT INTO public.power_unit (Id, Brand, Model, Country, Battery, Voltage," +
                        "Price, Description, Image)" +
                        "VALUES (@Id, @Brand, @Model, @Country, @Battery, @Voltage, @Price, @Description, @Image)", powerunit);

                    logger.LogInformation("powerUnit data saved to database");

                    String result = "PowerUnit data saved to database";
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                logger.LogError("PowerUnit data did not save in database");
                return BadRequest(ex);
            }
        }

        [HttpGet("GetPowerUnit/{id}")]
        public async Task<IActionResult> GetPowerUnitById(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");


                    var powerUnit = connection.QueryFirstOrDefault<PowerUnit>("SELECT * FROM public.power_unit WHERE Id = @Id",
                        new { Id = id });

                    if (powerUnit != null)
                    {
                        logger.LogInformation($"Retrieved PowerUnit with Id {id} from the database");
                        return Ok(powerUnit);

                    }
                    else
                    {
                        logger.LogInformation($"PowerUnit with Id {id} not found in the database");
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve PowerUnit data from the database. \nException {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("UpdatePowerUnit/{id}")]
        public async Task<IActionResult> UpdatePowerUnit(int id, PowerUnit updatedPowerUnit)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                if (updatedPowerUnit.Price < 0)
                {
                    return BadRequest("Price must not be less than 0");
                }

                if (updatedPowerUnit.Voltage < 0 || updatedPowerUnit.Voltage > 50000)
                {
                    return BadRequest("Voltage must be between 0 and 50000");
                }
                

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = id,
                        brand = updatedPowerUnit.Brand,
                        model = updatedPowerUnit.Model,
                        country = updatedPowerUnit.Country,
                        battery = updatedPowerUnit.Battery,
                        voltage = updatedPowerUnit.Voltage,
                        price = updatedPowerUnit.Price,
                        description = updatedPowerUnit.Description,
                        image = updatedPowerUnit.Image
                    };

                }

                connection.Open();
                logger.LogInformation("Connection started");

                connection.Execute("UPDATE public.power_unit SET Brand = @brand, Model = @model, Country = @country, Battery = @battery," +
                    " Voltage = @voltage," +
                    " Price = @price, Description = @description, Image = @image WHERE Id = @id", updatedPowerUnit);

                logger.LogInformation("PowerUnit data updated in the database");

                return Ok("PowerUnit data updated successfully");
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update PowerUnit data in database. \nException: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeletePowerUnit/{id}")]
        public async Task<IActionResult> DeletePowerUnit(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("DELETE FROM public.power_unit WHERE Id = @id", new { id });

                    logger.LogInformation("PowerUnit data deleted from the database");

                    return Ok("PowerUnit data deleted successfully");
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete PowerUnit data in database. \nException: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetAllPowerUnits")]
        public async Task<IActionResult> GetAllPowerUnits()
        {
            logger.LogInformation("Get method has started");
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var powerUnits = connection.Query<PowerUnit>("SELECT * FROM public.power_unit");

                    logger.LogInformation("Retrieved all PowerUnit data from the database");

                    return Ok(powerUnits);
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"PowerUnit data did not get gtom database. Exception: {ex}");
                return NotFound();
            }
        }
    }
}
