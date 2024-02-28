using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoolerController : ControllerBase
    {
        private readonly ILogger<CoolerController> logger;

        public CoolerController(ILogger<CoolerController> logger)
        {
            this.logger = logger;


        }

        [HttpPost("CreateCooler")]
        public async Task<IActionResult> CreateCooler(Cooler cooler)
        {

            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                if (cooler.Speed > 0 || cooler.Speed < 10000)
                {
                    return BadRequest("Speed must be between 0 and 10000");
                }

                if (cooler.Power > 0 || cooler.Power < 10000)
                {
                    return BadRequest("Speed must be between 0 and 10000");
                }

                if (cooler.Price < 0)
                {
                    return BadRequest("Price must not be less then 0");
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = cooler.Id,
                        brand = cooler.Brand,
                        model = cooler.Model,
                        country = cooler.Country,
                        speed = cooler.Speed,
                        power = cooler.Power,
                        price = cooler.Price,
                        description = cooler.Description,
                        image = cooler.Image,

                    };

                    connection.Open();
                    logger.LogInformation("Connection started");
                    connection.Execute("INSERT INTO public.cooler (Id, Brand, Model, Country, Speed, Power," +
                        "Price, Description, Image)" +
                        "VALUES (@Id, @Brand, @Model, @Country, @Speed, @Power, @Price, @Description, @Image)", cooler);

                    logger.LogInformation("Cooler data saved to database");
                    string result = "Cooler data saved to database";

                    return Ok(result);
                }
            }catch(Exception ex)
            {
                logger.LogError("Cooler data did not save in database");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCooler/{id}")]
        public async Task<IActionResult> GetCoolerById(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");


                    var cooler = connection.QueryFirstOrDefault<Cooler>("SELECT * FROM public.cooler WHERE Id = @Id",
                        new { Id = id });

                    if (cooler != null)
                    {
                        logger.LogInformation($"Retrieved Cooler with Id {id} from the database");
                        return Ok(cooler);

                    }
                    else
                    {
                        logger.LogInformation($"Cooler with Id {id} not found in the database");
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve Cooler data from the database. \nException {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("UpdateCooler/{id}")]
        public async Task<IActionResult> UpdateCooler(int id, Cooler updatedCooler)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                if (updatedCooler.Speed > 0 || updatedCooler.Speed < 10000)
                {
                    return BadRequest("Speed must be between 0 and 10000");
                }
                
                if (updatedCooler.Power > 0 || updatedCooler.Power < 10000)
                {
                    return BadRequest("Speed must be between 0 and 10000");
                }

                if (updatedCooler.Price < 0)
                {
                    return BadRequest("Price must not be less than 0");
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = id,
                        brand = updatedCooler.Brand,
                        model = updatedCooler.Model,
                        country = updatedCooler.Country,
                        speed = updatedCooler.Speed,
                        power = updatedCooler.Power,
                        price = updatedCooler.Price,
                        description = updatedCooler.Description,
                        image = updatedCooler.Image
                    };

                }

                connection.Open();
                logger.LogInformation("Connection started");

                connection.Execute("UPDATE public.cooler SET Brand = @brand, Model = @model, Country = @country, Speed = @speed," +
                    " Power = @power," +
                    " Price = @price, Description = @description, Image = @image WHERE Id = @id", updatedCooler);

                logger.LogInformation("Cooler data updated in the database");

                return Ok("Cooler data updated successfully");
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update Cooler data in database. \nException: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeleteCooler/{id}")]
        public async Task<IActionResult> DeleteCooler(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("DELETE FROM public.cooler WHERE Id = @id", new { id });

                    logger.LogInformation("Cooler data deleted from the database");

                    return Ok("Cooler data deleted successfully");
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete Cooler data in database. \nException: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetAllCoolers")]
        public async Task<IActionResult> GetAllCoolers()
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

                    var coolers = connection.Query<Cooler>("SELECT * FROM public.cooler");

                    logger.LogInformation("Retrieved all Cooler data from the database");

                    return Ok(coolers);
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"Cooler data did not get gtom database. Exception: {ex}");
                return NotFound();
            }
        }
    }
}
