using backend.Entities;
using backend.UpdatedEntities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Drawing;
using backend.Entities.CommentEntities;
using backend.Entities.User;

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

                if (cooler.cooler_power < 0 || cooler.cooler_power > 10000)
                {
                    return BadRequest(new { error = "Cooler_power must be between 0 and 10000" });
                }

                if (cooler.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less then 0" });
                }

                if(cooler.Amount < 0)
                {
                    return BadRequest(new { error = "Amount must not be less than 0" });
                }

                if(cooler.Power < 0 || cooler.Power > 10)
                {
                    return BadRequest(new { error = "Power must be between 0 and 10" });
                }

                cooler.Likes = 0;

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var data = new
                    {
                        brand = cooler.Brand,
                        model = cooler.Model,
                        country = cooler.Country,
                        speed = cooler.Speed,
                        cooler_power = cooler.cooler_power,
                        price = cooler.Price,
                        description = cooler.Description,
                        image = imagePath,
                        amount = cooler.Amount,
                        power = cooler.Power,
                        likes = cooler.Likes,
                    };

                    connection.Open();
                    int id = connection.QueryFirstOrDefault<int>("INSERT INTO public.cooler (brand, model, country," +
                        " speed, cooler_power," +
                        "price, description, image, amount, power, likes)" +
                        "VALUES (@brand, @model, @country, @speed, @cooler_power, @price," +
                        " @description, @image, @amount, @power, @likes) RETURNING id", data);

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
                
                if (updatedCooler.cooler_power > 0 || updatedCooler.cooler_power < 10000)
                {
                    return BadRequest(new { error = "Cooler_power must be between 0 and 10000" });
                }

                if (updatedCooler.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }

                if (updatedCooler.Amount < 0)
                {
                    return BadRequest(new { error = "Amount must not be less than 0" });
                }

                if(updatedCooler.Power < 0 || updatedCooler.Power > 10)
                {
                    return BadRequest(new { error = "Power must be between 0 and 10" });
                }

                if(updatedCooler.Likes < 0)
                {
                    return BadRequest(new { error = "Likes must not be less than 0" });
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
                        cooler_power = updatedCooler.cooler_power,
                        price = updatedCooler.Price,
                        description = updatedCooler.Description,
                        image = imagePath,
                        amount = updatedCooler.Amount,
                        power = updatedCooler.Power,
                        likes = updatedCooler.Likes,
                    };

                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("UPDATE public.cooler SET Brand = @brand, Model = @model, Country = @country, Speed = @speed," +
                        " Cooler_power = @cooler_power," +
                        " Price = @price, Description = @description," +
                        " Image = @image, Amount = @amount, Power = @power, Likes = @likes WHERE Id = @id", data);

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

        [HttpPost("addComment")]
        public async Task<IActionResult> AddComputerCaseComment(Comment computerCaseComment)
        {
            return await AddComment(computerCaseComment, "cooler_comment");
        }
        
        [HttpPut("updateComment")]
        public async Task<IActionResult> UpdateComputerCaseComment(Comment computerCaseComment)
        {
            return await UpdateComment(computerCaseComment, "cooler_comment");
        }
        
        [HttpDelete("{productId}/deleteComment/{commentId}")]
        public async Task<IActionResult> DeleteComputerCaseComment(int productId, int commentId)
        {
            return await DeleteComment(productId, commentId, "cooler_comment");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "cooler_comment");
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "cooler_comment");
        }

        [HttpPut("likeCooler/{id}")]
        public async Task<IActionResult> LikeCooler(int id, User user)
        {
            return await LikeComponent(id, user, "cooler");
        }
    }
}
