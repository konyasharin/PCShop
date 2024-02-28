using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SsdController : ControllerBase
    {
        private readonly ILogger<SsdController> logger;

        public SsdController(ILogger<SsdController> logger)
        {
            this.logger = logger;


        }

        [HttpPost("createSsd")]
        public async Task<IActionResult> CreateSsd(SSD ssd)
        {


            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                if (ssd.Capacity < 0 || ssd.Capacity > 10000)
                {
                    return BadRequest("Capacity must be between 0 and 10000");
                }

                if (ssd.Price < 0)
                {
                    return BadRequest("Price must not be less than 0");
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = ssd.Id,
                        brand = ssd.Brand,
                        model = ssd.Model,
                        country = ssd.Country,
                        capacity = ssd.Capacity,
                        price = ssd.Price,
                        description = ssd.Description,
                        image = ssd.Image,

                    };

                    connection.Open();
                    logger.LogInformation("Connection started");
                    connection.Execute("INSERT INTO public.ssd (Id, Brand, Model, Country, Capacity," +
                        "Price, Description, Image)" +
                        "VALUES (@Id, @Brand, @Model, @Country, @Capacity, @Price, @Description, @Image)", ssd);

                    logger.LogInformation("SSD data saved to database");

                    String result = "Ssd data saved to database";
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"SSD data did not save in database. \nEsception: {ex}");
                return BadRequest(ex);
            }
        }

        [HttpGet("getSsd/{id}")]
        public async Task<IActionResult> GetSsdById(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");


                    var ssd = connection.QueryFirstOrDefault<SSD>("SELECT * FROM public.ssd WHERE Id = @Id",
                        new { Id = id });

                    if (ssd != null)
                    {
                        logger.LogInformation($"Retrieved SSD with Id {id} from the database");
                        return Ok(ssd);

                    }
                    else
                    {
                        logger.LogInformation($"SSD with Id {id} not found in the database");
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve SSD data from the database. \nException {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("UpdateSsd/{id}")]
        public async Task<IActionResult> UpdateSsd(int id, SSD updatedSsd)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                if (updatedSsd.Capacity < 0 || updatedSsd.Capacity > 10000)
                {
                    return BadRequest("Capacity must be between 0 and 10000");
                }

                if (updatedSsd.Price < 0)
                {
                    return BadRequest("Price must not be less than 0");
                }
                

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = id,
                        brand = updatedSsd.Brand,
                        model = updatedSsd.Model,
                        country = updatedSsd.Country,
                        capacity = updatedSsd.Capacity,
                        price = updatedSsd.Price,
                        description = updatedSsd.Description,
                        image = updatedSsd.Image
                    };

                }

                connection.Open();
                logger.LogInformation("Connection started");

                connection.Execute("UPDATE public.ssd SET Brand = @brand, Model = @model, Country = @country, Capacity = @capacity," +
                    " Price = @price, Description = @description, Image = @image WHERE Id = @id", updatedSsd);

                logger.LogInformation("SSD data updated in the database");

                return Ok("SSD data updated successfully");
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update SSD data in database. \nException: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeleteSsd/{id}")]
        public async Task<IActionResult> DeleteSsd(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("DELETE FROM public.ssd WHERE Id = @id", new { id });

                    logger.LogInformation("SSD data deleted from the database");

                    return Ok("SSD data deleted successfully");
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete SSD data in database. \nException: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("GetAllSsd")]
        public async Task<IActionResult> GetAllSsd()
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

                    var ssd = connection.Query<SSD>("SELECT * FROM public.ssd");

                    logger.LogInformation("Retrieved all SSD data from the database");

                    return Ok(ssd);
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"SSD data did not get gtom database. Exception: {ex}");
                return NotFound();
            }
        }
    }
}
