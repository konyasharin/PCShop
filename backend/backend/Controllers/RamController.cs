using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamController : ControllerBase
    {
        private readonly ILogger<RamController> logger;

        public RamController(ILogger<RamController> logger)
        {
            this.logger = logger;


        }

        [HttpPost("createRam")]
        public async Task<IActionResult> CreateRam(RAM ram)
        {

            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                if (ram.Frequency < 0 || ram.Frequency > 100000)
                {
                    return BadRequest("Frequency must be between 0 and 100000");
                }

                if (ram.Timings < 0 || ram.Timings > 10000)
                {
                    return BadRequest("Timings must be between 0 and 10000");
                }

                if (ram.Capacity_db < 0 || ram.Capacity_db > 10000)
                {
                    return BadRequest("Capacity_db must be between 0 and 10000");
                }

                if (ram.Price < 0)
                {
                    return BadRequest("Price must not be less than 0");
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = ram.Id,
                        brand = ram.Brand,
                        model = ram.Model,
                        country = ram.Country,
                        frequency = ram.Frequency,
                        timings = ram.Timings,
                        capacity_db = ram.Capacity_db,
                        price = ram.Price,
                        description = ram.Description,
                        image = ram.Image,

                    };

                    connection.Open();
                    logger.LogInformation("Connection started");
                    connection.Execute("INSERT INTO public.ram (id, brand, model, country, frequency, timings, capacity_db," +
                        "price, description, image)" +
                        "VALUES (@Id, @Brand, @Model, @Country, @Frequency, @Timings, @Capacity_db, @Price, @Description, @Image)", ram);

                    logger.LogInformation("Ram data saved to database");

                    String result = "Ram data saved to database";
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Ram data did not save in database. Exception: {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getRam/{id}")]
        public async Task<IActionResult> GetRamById(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");


                    var ram = connection.QueryFirstOrDefault<RAM>("SELECT * FROM public.ram WHERE Id = @Id",
                        new { Id = id });

                    if (ram != null)
                    {
                        logger.LogInformation($"Retrieved Ram with Id {id} from the database");
                        return Ok(ram);

                    }
                    else
                    {
                        logger.LogInformation($"Ram with Id {id} not found in the database");
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve Ram data from the database. \nException {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("updateRam/{id}")]
        public async Task<IActionResult> UpdateRam(int id, RAM updatedRam)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                if (updatedRam.Frequency < 0 || updatedRam.Frequency > 100000)
                {
                    return BadRequest("Frequency must be between 0 and 100000");
                }

                if (updatedRam.Timings <0 || updatedRam.Timings > 10000)
                {
                    return BadRequest("Timings must be between 0 and 10000");
                }

                if (updatedRam.Capacity_db < 0 || updatedRam.Capacity_db > 10000)
                {
                    return BadRequest("Capacity_db must be between 0 and 10000");
                }

                if (updatedRam.Price < 0)
                {
                    return BadRequest("Price must not be less than 0");
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = id,
                        brand = updatedRam.Brand,
                        model = updatedRam.Model,
                        country = updatedRam.Country,
                        frequency = updatedRam.Frequency,
                        timings = updatedRam.Timings,
                        capacity_db = updatedRam.Capacity_db,
                        price = updatedRam.Price,
                        description = updatedRam.Description,
                        image = updatedRam.Image
                    };

                }

                connection.Open();
                logger.LogInformation("Connection started");

                connection.Execute("UPDATE public.ram SET Brand = @brand, Model = @model, Country = @country, Frequency = @frequency," +
                    " Timings = @timings, Capacity_db = @capacity_db," +
                    " Price = @price, Description = @description, Image = @image WHERE Id = @id", updatedRam);

                logger.LogInformation("RAM data updated in the database");

                return Ok("RAM data updated successfully");
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update RAM data in database. \nException: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("deleteRam/{id}")]
        public async Task<IActionResult> DeleteRam(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("DELETE FROM public.ram WHERE Id = @id", new { id });

                    logger.LogInformation("RAM data deleted from the database");

                    return Ok("RAM data deleted successfully");
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete RAM data in database. \nException: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getAllRam")]
        public async Task<IActionResult> GetAllRams()
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

                    var computerCases = connection.Query<RAM>("SELECT * FROM public.ram");

                    logger.LogInformation("Retrieved all RAM data from the database");

                    return Ok(computerCases);
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"RAM data did not get ftom database. Exception: {ex}");
                return NotFound();
            }
        }
    }
}
