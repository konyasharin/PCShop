using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessorController : ControllerBase
    {
        private readonly ILogger<ProcessorController> logger;

        public ProcessorController(ILogger<ProcessorController> logger)
        {
            this.logger = logger;


        }

        [HttpPost("CreateProcessor")]
        public async void CreateProcessor(Processor processor)
        {

            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = processor.Id,
                        brand = processor.Brand,
                        model = processor.Model,
                        country = processor.Country,
                        clock_frquency = processor.Clock_frequency,
                        turbo_frequency = processor.Turbo_frequency,
                        heat_dissipation = processor.Heat_dissipation,
                        price = processor.Price,
                        description = processor.Description,
                        image = processor.Image,

                    };

                    connection.Open();
                    logger.LogInformation("Connection started");
                    connection.Execute("INSERT INTO public.processor (Id, Brand, Model, Country, Clock_frequency, Turbo_frequency, Heat_dissipation" +
                        "Price, Description, Image)" +
                        "VALUES (@Id, @Brand, @Model, @Country, @Clock_frequency, @Turbo_frequency, @Heat_dissipation, @Price, @Description, @Image)", processor);
                    logger.LogInformation("Processor data saved to database");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Processor data did not save in database. Exeption: {ex}");
            }
        }

        [HttpGet("GetProcessor/{id}")]
        public async Task<IActionResult> GetProcessorById(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");


                    var processor = connection.QueryFirstOrDefault<Processor>("SELECT * FROM public.processor WHERE Id = @Id",
                        new { Id = id });

                    if (processor != null)
                    {
                        logger.LogInformation($"Retrieved Processor with Id {id} from the database");
                        return Ok(processor);

                    }
                    else
                    {
                        logger.LogInformation($"Processor with Id {id} not found in the database");
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve Processor data from the database. \nException {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("UpdateProcessor/{id}")]
        public async Task<IActionResult> UpdateProcessor(int id, Processor updatedProcessor)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                if ((updatedProcessor.Clock_frequency < 0 || updatedProcessor.Clock_frequency > 50000)
                    && (updatedProcessor.Clock_frequency > updatedProcessor.Turbo_frequency))
                {
                    return BadRequest("Clock_frequency must be between 0 and 50000 and less than turbo_frequency");
                }

                if ((updatedProcessor.Turbo_frequency < updatedProcessor.Clock_frequency) 
                    && (updatedProcessor.Turbo_frequency > 100000 || updatedProcessor.Turbo_frequency < 0))
                {
                    return BadRequest("Turbo_frequency must be bigger than clock_frequency and 100000 and less than 0");
                }
                
                if (updatedProcessor.Price < 0)
                {
                    return BadRequest("Price must not be less than 0");
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = id,
                        brand = updatedProcessor.Brand,
                        model = updatedProcessor.Model,
                        country = updatedProcessor.Country,
                        clock_frequency = updatedProcessor.Clock_frequency,
                        turbo_frequency = updatedProcessor.Turbo_frequency,
                        heat_dissipation = updatedProcessor.Heat_dissipation,
                        price = updatedProcessor.Price,
                        description = updatedProcessor.Description,
                        image = updatedProcessor.Image
                    };

                }

                connection.Open();
                logger.LogInformation("Connection started");

                connection.Execute("UPDATE public.processor SET Brand = @brand, Model = @model, Country = @country," +
                    " Clock_frequency = @clock_frequency, Turbo_frequency = @turbo_frequency, Heat_dissipation = @heat_dissipation," +
                    " Depth = @depth, Price = @price, Description = @description, Image = @image WHERE Id = @id", updatedProcessor);

                logger.LogInformation("Processor data updated in the database");

                return Ok("Processor data updated successfully");
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update Processor data in database. \nException: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeleteProcessor/{id}")]
        public async Task<IActionResult> DeleteComputerCase(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("DELETE FROM public.processor WHERE Id = @id", new { id });

                    logger.LogInformation("Processor data deleted from the database");

                    return Ok("Processor data deleted successfully");
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete Processor data in database. \nException: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetAllProcessors")]
        public async Task<IActionResult> GetAllprocessors()
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

                    var processors = connection.Query<Processor>("SELECT * FROM public.processor");

                    logger.LogInformation("Retrieved all Processor data from the database");

                    return Ok(processors);
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"Processor data did not get gtom database. Exception: {ex}");
                return NotFound();
            }
        }
    }
}
