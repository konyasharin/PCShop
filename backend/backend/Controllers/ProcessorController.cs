using backend.Entities;
using backend.UpdatedEntities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Diagnostics;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessorController : ComponentController
    {
        
        public ProcessorController(ILogger<ProcessorController> logger):base(logger)
        {
           
        }

        [HttpPost("createProcessor")]
        public async Task<IActionResult> CreateProcessor([FromForm] Processor<IFormFile> processor)
        {

            try
            {
                string imagePath = BackupWriter.Write(processor.Image);
                
                
                if (processor.Price < 0)
                {
                    return BadRequest(new { error = "Price must be less than 0" });
                }

                if ((processor.Clock_frequency < 0 || processor.Clock_frequency >= 50000)
                    && (processor.Clock_frequency > processor.Turbo_frequency))
                {
                    return BadRequest(new { error = "Clock_frequency must be between 0 and 50000 and less than turbo_frequency" });
                }

                if ((processor.Turbo_frequency < processor.Clock_frequency)
                    && (processor.Turbo_frequency >= 100000 || processor.Turbo_frequency < 0))
                {
                    return BadRequest(new { error = "Turbo_frequency must be bigger than clock_frequency and 100000 and less than 0" });
                }

                if (processor.Cores < 0 || processor.Cores >= 8)
                {
                    return BadRequest(new { error = "Cores most be between 0 and 8" });
                }

                if (processor.Heat_dissipation < 0 || processor.Heat_dissipation > 10000)
                {
                    return BadRequest(new { error = "Heat_dissipation must be between 0 and 10000" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var data = new
                    {
                        brand = processor.Brand,
                        model = processor.Model,
                        cores = processor.Cores,
                        country = processor.Country,
                        clock_frquency = processor.Clock_frequency,
                        turbo_frequency = processor.Turbo_frequency,
                        heat_dissipation = processor.Heat_dissipation,
                        price = processor.Price,
                        description = processor.Description,
                        image = imagePath,
                    };

                    connection.Open();
                    int id = connection.QueryFirstOrDefault<int>("INSERT INTO public.processor (Id, Brand, Model, Country, Cores, Clock_frequency, Turbo_frequency," +
                        " Heat_dissipation," +
                        "Price, Description, Image)" +
                        "VALUES (@Id, @Brand, @Model, @Country, @Cores, @Clock_frequency, @Turbo_frequency, @Heat_dissipation, @Price," +
                        " @Description, @Image) RETURNING id", data);

                    logger.LogInformation("Processor data saved to database");
                    return Ok(new { id = id, data });
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Processor data did not save in database. Exeption: {ex}");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("getProcessor/{id}")]
        public async Task<IActionResult> GetProcessorById(int id)
        {
            try
            {

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");


                    var processor = connection.QueryFirstOrDefault<Processor<string>>("SELECT * FROM public.processor WHERE Id = @Id",
                        new { Id = id });

                    if (processor != null)
                    {
                        logger.LogInformation($"Retrieved Processor with Id {id} from the database");
                        return Ok(new { id = id, processor });

                    }
                    else
                    {
                        logger.LogInformation($"Processor with Id {id} not found in the database");
                        return NotFound(new {error = $"Processor NotFound with {id}"});
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve Processor data from the database. \nException {ex}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPut("updateProcessor/{id}")]
        public async Task<IActionResult> UpdateProcessor(int id, [FromForm] UpdatedProcessor updatedProcessor)
        {
            try
            {
               

                if ((updatedProcessor.Clock_frequency < 0 || updatedProcessor.Clock_frequency >= 50000)
                    && (updatedProcessor.Clock_frequency > updatedProcessor.Turbo_frequency))
                {
                    return BadRequest(new { error = "Clock_frequency must be between 0 and 50000 and less than turbo_frequency" });
                }

                if ((updatedProcessor.Turbo_frequency < updatedProcessor.Clock_frequency) 
                    && (updatedProcessor.Turbo_frequency > 100000 || updatedProcessor.Turbo_frequency < 0))
                {
                    return BadRequest(new { error = "Turbo_frequency must be bigger than clock_frequency and 100000 and less than 0" });
                }
                
                if (updatedProcessor.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }

                if (updatedProcessor.Cores < 0 || updatedProcessor.Cores > 8)
                {
                    return BadRequest(new { error = "Cores most be between 0 and 8" });
                }

                if (updatedProcessor.Heat_dissipation < 0 || updatedProcessor.Heat_dissipation > 10000)
                {
                    return BadRequest(new { error = "Heat_dissipation must be between 0 and 10000" });
                }

                string imagePath = string.Empty;

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    string filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.processor WHERE Id = @id");

                    if (updatedProcessor.updated)
                    {

                        BackupWriter.Delete(filePath);
                        imagePath = BackupWriter.Write(updatedProcessor.Image);
                    }
                    else
                    {
                        imagePath = filePath;
                    }

                    var data = new
                    {
                        id = id,
                        brand = updatedProcessor.Brand,
                        model = updatedProcessor.Model,
                        country = updatedProcessor.Country,
                        cores = updatedProcessor.Cores,
                        clock_frequency = updatedProcessor.Clock_frequency,
                        turbo_frequency = updatedProcessor.Turbo_frequency,
                        heat_dissipation = updatedProcessor.Heat_dissipation,
                        price = updatedProcessor.Price,
                        description = updatedProcessor.Description,
                        image = imagePath,
                    };

                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("UPDATE public.processor SET Brand = @brand, Model = @model, Country = @country, Cores = @cores," +
                        " Clock_frequency = @clock_frequency, Turbo_frequency = @turbo_frequency, Heat_dissipation = @heat_dissipation," +
                        " Depth = @depth, Price = @price, Description = @description, Image = @image WHERE Id = @id", data);

                    logger.LogInformation("Processor data updated in the database");

                    return Ok(new { id = id, data});

                }

                
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update Processor data in database. \nException: {ex}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpDelete("deleteProcessor/{id}")]
        public async Task<IActionResult> DeleteComputerCase(int id)
        {
            try
            {
                string filePath;
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.processor WHERE Id = @id", new { Id = id });
                    BackupWriter.Delete(filePath);

                    connection.Execute("DELETE FROM public.processor WHERE Id = @id", new { id });

                    logger.LogInformation("Processor data deleted from the database");

                    return Ok(new {id=id});
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete Processor data in database. \nException: {ex}");
                return StatusCode(500, new {error = ex.Message});
            }
        }

        [HttpGet("getAllProcessors")]
        public async Task<IActionResult> GetAllprocessors(int limit, int offset)
        {
            logger.LogInformation("Get method has started");
            try
            {

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var processors = connection.Query<Processor<string>>("SELECT * FROM public.processor LIMIT @Limit OFFSET @Offset",
                        new {Limit = limit, Offset = offset});

                    logger.LogInformation("Retrieved all Processor data from the database");

                    return Ok(new {data=processors});
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"Processor data did not get from database. Exception: {ex}");
                return NotFound(new {error=ex.Message});
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProcessor(string keyword)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var processors = connection.Query<Processor<string>>(@"SELECT * FROM public.processor " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT 3", new { Keyword = "%" + keyword + "%" });

                    return Ok(new { processors });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with search");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
