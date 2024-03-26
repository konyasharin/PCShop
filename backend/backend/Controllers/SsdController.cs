using backend.Entities;
using backend.UpdatedEntities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Runtime.Intrinsics.X86;
using backend.Entities.CommentEntities;
using System.Runtime.Intrinsics.Arm;
using backend.Entities.User;

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
            
                if (ssd.Capacity < 0 || ssd.Capacity > 10000)
                {
                    return BadRequest(new { error = "Capacity must be between 0 and 10000" });
                }

                ssd.Likes = 0;

            return await CreateComponent<SSD<IFormFile>>(ssd, ["capacity"], "ssd");
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

                if (updatedSsd.Amount < 0)
                {
                    return BadRequest(new { error = "Amount must be less than 0" });
                }

                if (updatedSsd.Power < 0 || updatedSsd.Power > 10)
                {
                    return BadRequest(new { error = "Power must be between 0 and 10" });
                }

                if(updatedSsd.Likes < 0)
                {
                    return BadRequest(new { error = "Likes must not be less than 0" });
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
                        amount = updatedSsd.Amount,
                        power = updatedSsd.Power,
                        likes = updatedSsd.Likes,
                    };

                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("UPDATE public.ssd SET Brand = @brand, Model = @model," +
                        " Country = @country, Capacity = @capacity," +
                        " Price = @price, Description = @description," +
                        " Image = @image, Amount = @amount, Power = @power, Likes = @likes WHERE Id = @id", data);

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

                    filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.ssd WHERE Id = @id",
                        new { Id = id });
                    BackupWriter.Delete(filePath);

                    connection.Execute("DELETE FROM public.ssd WHERE Id = @id", new { id });

                    connection.Execute("DELETE FROM public.like WHERE componentid = @id AND component = ssd",
                        new { id });

                    connection.Execute("DELETE FROM public.comment WHERE component_id = @id AND component = ssd",
                        new { id });

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
        public async Task<IActionResult> SearchSsd(string keyword, int limit = 1, int offset = 0)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var ssd = connection.Query<RAM<string>>(@"SELECT * FROM public.ssd " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT @Limit OFFSET @Offset", new { Keyword = "%" + keyword + "%", Limit = limit, Offset = offset });

                    return Ok(new { ssd });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with search");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("FilterByCountry")]
        public async Task<IActionResult> FilterByCountry(string country, int limit, int offset)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var ssd = connection.Query<SSD<string>>(@"SELECT * FROM public.ssd " +
                    "WHERE country = @Country " +
                    "LIMIT @Limit OFFSET @Offset", new { Country = country, Limit = limit, Offset = offset });

                    return Ok(new { ssd });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with country filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("FilterByBrand")]
        public async Task<IActionResult> FilterByBrand(string brand, int limit, int offset)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var ssd = connection.Query<SSD<string>>(@"SELECT * FROM public.ssd " +
                    "WHERE brand = @Brand " +
                    "LIMIT @Limit OFFSET @Offset", new { Brand = brand, Limit = limit, Offset = offset });

                    return Ok(new { ssd });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with brand filter");
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet("FilterByModel")]
        public async Task<IActionResult> FilterByModel(string model, int limit, int offset)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var ssd = connection.Query<SSD<string>>(@"SELECT * FROM public.ssd " +
                    "WHERE model = @Model " +
                    "LIMIT @Limit OFFSET @Offset", new { Model = model, Limit = limit, Offset = offset });

                    return Ok(new { ssd });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with model filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("FilterByPrice")]
        public async Task<IActionResult> FilterByPrice(int minPrice, int maxPrice, int limit, int offset)
        {
            try
            {
                if (minPrice < 0 || maxPrice < 0)
                {
                    return BadRequest(new { error = "price must not be 0" });
                }

                if (maxPrice < minPrice)
                {
                    return BadRequest(new { error = "maxPrice could not be less than minPrice" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var ram = connection.Query<SSD<string>>(@"SELECT * FROM public.ssd " +
                    "WHERE price >=  @MinPrice AND price <= @MaxPrice " +
                    "LIMIT @Limit OFFSET @Offset", new
                    {
                        MinPrice = minPrice,
                        MaxPrice = maxPrice,
                        Limit = limit,
                        Offset = offset
                    });

                    return Ok(new { ram });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with price filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("FilterByCapacity")]
        public async Task<IActionResult> FilterByCapacity(int minCapacity, int maxCapacity, int limit, int offset)
        {
            try
            {
                if (minCapacity < 0 || maxCapacity < 0)
                {
                    return BadRequest(new { error = "capacity_db must not be 0" });
                }

                if (maxCapacity < minCapacity)
                {
                    return BadRequest(new { error = "maxCapacity could not be less than minCapacity" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var ssd = connection.Query<SSD<string>>(@"SELECT * FROM public.ssd " +
                    "WHERE capacity >=  @MinCapacity AND capacity_db <= @MaxCapacity " +
                    "LIMIT @Limit OFFSET @Offset", new
                    {
                        MinCapacity = minCapacity,
                        MaxCapacity = maxCapacity,
                        Limit = limit,
                        Offset = offset
                    });

                    return Ok(new { ssd });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with capacity filter");
                return BadRequest(new { error = ex.Message });
            }
        }
        
        [HttpPost("addComment")]
        public async Task<IActionResult> AddComputerCaseComment(Comment computerCaseComment)
        {
            return await AddComment(computerCaseComment);
        }
        
        [HttpPut("updateComment")]
        public async Task<IActionResult> UpdateComputerCaseComment(Comment computerCaseComment)
        {
            return await UpdateComment(computerCaseComment);
        }
        
        [HttpDelete("{productId}/deleteComment/{commentId}")]
        public async Task<IActionResult> DeleteComputerCaseComment(int productId, int commentId)
        {
            return await DeleteComment(productId, commentId, "ssd");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int productId, int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "ssd", productId);
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "ssd");
        }

        [HttpPut("likeSsd/{id}")]
        public async Task<IActionResult> LikeSsd(int id, User user)
        {
            return await LikeComponent(id, user, "ssd");
        }
    }
}
