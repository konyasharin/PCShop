using backend.Entities;
using backend.UpdatedEntities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Drawing;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoolerController : ComponentController
    {

        public CoolerController(ILogger<CoolerController> logger):base(logger)
        { 
        }

        [HttpPost("createCooler")]
        public async Task<IActionResult> CreateCooler([FromForm] Cooler<IFormFile> cooler)
        {
            try
            {
                string imagePath = BackupWriter.Write(cooler.Image);

                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                if (cooler.Speed <= 0 || cooler.Speed > 10000)
                {
                    return BadRequest(new { error = "Speed must be between 0 and 10000" });
                }

                if (cooler.Power < 0 || cooler.Power > 10000)
                {
                    return BadRequest(new { error = "Power must be between 0 and 10000" });
                }

                if (cooler.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less then 0" });
                }

                if(cooler.Amount < 0)
                {
                    return BadRequest(new { error = "Amount must not be less than 0" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var data = new
                    {
                        brand = cooler.Brand,
                        model = cooler.Model,
                        country = cooler.Country,
                        speed = cooler.Speed,
                        power = cooler.Power,
                        price = cooler.Price,
                        description = cooler.Description,
                        image = imagePath,
                        amount = cooler.Amount,
                    };

                    connection.Open();
                    int id = connection.QueryFirstOrDefault<int>("INSERT INTO public.cooler (brand, model, country, speed, power," +
                        "price, description, image, amount)" +
                        "VALUES (@brand, @model, @country, @speed, @power, @price," +
                        " @description, @image, @amount) RETURNING id", data);

                    logger.LogInformation("Cooler data saved to database");

                    return Ok(new { id = id, data });
                }
            }catch(Exception ex)
            {
                logger.LogError("Cooler data did not save in database");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("getCooler/{id}")]
        public async Task<IActionResult> GetCoolerById(int id)
        {
            try
            {


                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");


                    var cooler = connection.QueryFirstOrDefault<Cooler<string>>("SELECT * FROM public.cooler WHERE Id = @Id",
                        new { Id = id });

                    if (cooler != null)
                    {
                        logger.LogInformation($"Retrieved Cooler with Id {id} from the database");
                        return Ok(new { id = id, cooler });

                    }
                    else
                    {
                        logger.LogInformation($"Cooler with Id {id} not found in the database");
                        return NotFound(new { error = $"Not found Cooler with {id}" });
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve Cooler data from the database. \nException {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("updateCooler/{id}")]
        public async Task<IActionResult> UpdateCooler(int id, [FromForm] UpdatedCooler updatedCooler)
        {
            try
            {
                
                if (updatedCooler.Speed > 0 || updatedCooler.Speed < 10000)
                {
                    return BadRequest(new { error = "Speed must be between 0 and 10000" });
                }
                
                if (updatedCooler.Power > 0 || updatedCooler.Power < 10000)
                {
                    return BadRequest(new { error = "Speed must be between 0 and 10000" });
                }

                if (updatedCooler.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }

                if (updatedCooler.Amount < 0)
                {
                    return BadRequest(new { error = "Amount must not be less than 0" });
                }

                string imagePath = string.Empty;

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    string filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.cooler WHERE Id = @id");

                    if (updatedCooler.updated)
                    {

                        BackupWriter.Delete(filePath);
                        imagePath = BackupWriter.Write(updatedCooler.Image);
                    }
                    else
                    {
                        imagePath = filePath;
                    }

                    var data = new
                    {
                        id = id,
                        brand = updatedCooler.Brand,
                        model = updatedCooler.Model,
                        country = updatedCooler.Country,
                        speed = updatedCooler.Speed,
                        power = updatedCooler.Power,
                        price = updatedCooler.Price,
                        description = updatedCooler.Description,
                        image = imagePath,
                        amount = updatedCooler.Amount,
                    };

                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("UPDATE public.cooler SET Brand = @brand, Model = @model, Country = @country, Speed = @speed," +
                        " Power = @power," +
                        " Price = @price, Description = @description, Image = @image, Amount = @amount WHERE Id = @id", data);

                    logger.LogInformation("Cooler data updated in the database");

                    return Ok(new {id = id, data});

                }

                
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update Cooler data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("deleteCooler/{id}")]
        public async Task<IActionResult> DeleteCooler(int id)
        {
            try
            {

                string filePath;

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.cooler WHERE Id = @id",
                        new { Id = id });
                    BackupWriter.Delete(filePath);

                    connection.Execute("DELETE FROM public.cooler WHERE Id = @id", new { id });

                    logger.LogInformation("Cooler data deleted from the database");

                    return Ok(new {Id = id});
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete Cooler data in database. \nException: {ex}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("getAllCoolers")]
        public async Task<IActionResult> GetAllCoolers(int limit, int offset)
        {
            logger.LogInformation("Get method has started");
            try
            {
               

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var coolers = connection.Query<Cooler<string>>("SELECT * FROM public.cooler LIMIT @Limit OFFSET @Offset",
                        new {Limit = limit, Offset = offset});

                    logger.LogInformation("Retrieved all Cooler data from the database");

                    return Ok(new { coolers });
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"Cooler data did not get gtom database. Exception: {ex}");
                return NotFound(new {error = ex.Message});
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCooler(string keyword, int limit = 1, int offset = 0)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var coolers = connection.Query<Cooler<string>>(@"SELECT * FROM public.cooler " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT @limit OFFST @Offset", new { Keyword = "%" + keyword + "%", Limit = limit, Offset = offset});

                    return Ok(new { coolers });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with search");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> FilterCooler(string country, string brand, string model, int minPrice, 
            int maxPrice, int minSpeed, int maxSpeed, int limit, int offset)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var coolers = connection.Query<Cooler<string>>(@"SELECT * FROM public.cooler " +
                    "WHERE country = @Country AND brand = @Brand AND model = @Model " +
                    "AND price >=  @MinPrice AND price <= @MaxPrice AND speed >=  @MinSpeed AND speed <= @MaxSpeed " +
                    "LIMIT @Limit OFFSET @Offset", new { Country = country, Brand = brand, Model = model,
                        MinPrice = minPrice, MaxPrice = maxPrice, MinSpeed = minSpeed, MaxSpeed = maxSpeed,
                        Limit = limit, Offset = offset });

                    return Ok(new { coolers });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with country filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        
    }
}
