using backend.Entities;
using backend.UpdatedEntities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SsdController : ComponentController
    {
       
        public SsdController(ILogger<SsdController> logger) : base(logger)
        {
        }

        [HttpPost("createSsd")]
        public async Task<IActionResult> CreateSsd([FromForm] SSD<IFormFile> ssd)
        {
            try
            {
                string imagePath = BackupWriter.Write(ssd.Image);
               
                if (ssd.Capacity < 0 || ssd.Capacity > 10000)
                {
                    return BadRequest(new { error = "Capacity must be between 0 and 10000" });
                }

                if (ssd.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var data = new
                    {
                        brand = ssd.Brand,
                        model = ssd.Model,
                        country = ssd.Country,
                        capacity = ssd.Capacity,
                        price = ssd.Price,
                        description = ssd.Description,
                        image = imagePath,
                    };

                    connection.Open();
                    int id = connection.QueryFirstOrDefault<int>("INSERT INTO public.ssd (brand, model, country, capacity," +
                        "price, description, image)" +
                        "VALUES (@brand, @model, @country, @capacity, @price, @description, @image) RETURNING id", data);

                    logger.LogInformation("SSD data saved to database");
                    return Ok(new { id = id, data });
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"SSD data did not save in database. \nEsception: {ex}");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("getSsd/{id}")]
        public async Task<IActionResult> GetSsdById(int id)
        {
            try
            {
               
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");


                    var ssd = connection.QueryFirstOrDefault<SSD<string>>("SELECT * FROM public.ssd WHERE Id = @Id",
                        new { Id = id });

                    if (ssd != null)
                    {
                        logger.LogInformation($"Retrieved SSD with Id {id} from the database");
                        return Ok(new {id=id, ssd});

                    }
                    else
                    {
                        logger.LogInformation($"SSD with Id {id} not found in the database");
                        return NotFound(new {error = $"SSD NotFound with {id}"});
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve SSD data from the database. \nException {ex}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPut("updateSsd/{id}")]
        public async Task<IActionResult> UpdateSsd(int id, [FromForm] UpdatedSsd updatedSsd)
        {
            try
            {
               
                if (updatedSsd.Capacity < 0 || updatedSsd.Capacity > 10000)
                {
                    return BadRequest(new { error = "Capacity must be between 0 and 10000" });
                }

                if (updatedSsd.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }
                
                string imagePath = string.Empty;
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    string filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.ssd WHERE Id = @id");

                    if (updatedSsd.updated)
                    {

                        BackupWriter.Delete(filePath);
                        imagePath = BackupWriter.Write(updatedSsd.Image);
                    }
                    else
                    {
                        imagePath = filePath;
                    }

                    var data = new
                    {
                        id = id,
                        brand = updatedSsd.Brand,
                        model = updatedSsd.Model,
                        country = updatedSsd.Country,
                        capacity = updatedSsd.Capacity,
                        price = updatedSsd.Price,
                        description = updatedSsd.Description,
                        image = imagePath,
                    };

                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("UPDATE public.ssd SET Brand = @brand, Model = @model, Country = @country, Capacity = @capacity," +
                        " Price = @price, Description = @description, Image = @image WHERE Id = @id", data);

                    logger.LogInformation("SSD data updated in the database");

                    return Ok(new { id = id, data });

                }

                
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update SSD data in database. \nException: {ex}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpDelete("deleteSsd/{id}")]
        public async Task<IActionResult> DeleteSsd(int id)
        {
            try
            {
                string filePath;
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.ssd WHERE Id = @id", new { Id = id });
                    BackupWriter.Delete(filePath);

                    connection.Execute("DELETE FROM public.ssd WHERE Id = @id", new { id });

                    logger.LogInformation("SSD data deleted from the database");

                    return Ok(new {id=id});
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete SSD data in database. \nException: {ex}");
                return StatusCode(500, new {error = ex.Message});
            }
        }


        [HttpGet("getAllSsd")]
        public async Task<IActionResult> GetAllSsd(int limit, int offset)
        {
            logger.LogInformation("Get method has started");
            try
            {
               
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var ssd = connection.Query<SSD<string>>("SELECT * FROM public.ssd LIMIT @Limit OFFSET @Offset",
                        new {Limit = limit, Offset = offset});

                    logger.LogInformation("Retrieved all SSD data from the database");

                    return Ok(new {data = ssd});
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"SSD data did not get from database. Exception: {ex}");
                return NotFound(new {error = ex.Message});
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchSsd(string keyword)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var ssd = connection.Query<RAM<string>>(@"SELECT * FROM public.ssd " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT 3", new { Keyword = "%" + keyword + "%" });

                    return Ok(new { ssd });

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
