using backend.Entities;
using backend.Entities.CommentEntities;
using backend.Entities.User;
using backend.UpdatedEntities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Runtime.Intrinsics.Arm;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoCardController : ComponentController
    {
        
        public VideoCardController(ILogger<VideoCardController> logger) : base(logger)
        {
        }

        [HttpPost("createVideoCard")]
        public async Task<IActionResult> CreateVideoCard([FromForm] VideoCard<IFormFile> videoCard)
        {
            try
            {
                string imagePath = BackupWriter.Write(videoCard.Image);
                
                if (videoCard.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }

                if(videoCard.Amount < 0)
                {
                    return BadRequest(new { error = "Amount must not be less than 0" });
                }

                if (videoCard.Power < 0 || videoCard.Power > 10)
                {
                    return BadRequest(new { error = "Power must be between 0 and 10" });
                }

                videoCard.Likes = 0;

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var data = new
                    {
                        id = videoCard.Id,
                        brand = videoCard.Brand,
                        model = videoCard.Model,
                        country = videoCard.Country,
                        memory_db = videoCard.MemoryDb,
                        memory_type = videoCard.MemoryType,
                        price = videoCard.Price,
                        description = videoCard.Description,
                        image = imagePath,
                        amount = videoCard.Amount,
                        power = videoCard.Power,
                        likes = videoCard.Likes,
                    };

                    connection.Open();
                    int id = connection.QuerySingleOrDefault<int>("INSERT INTO public.video_card (brand, model, country, memory_db, memory_type," +
                        "price, description, image, amount, power, likes)" +
                        "VALUES (@brand, @model, @country, @memory_db, @memory_type," +
                        " @price, @description, @image, @amount, @power, @likes) RETURNING id", data);
                    logger.LogInformation("VideoCard data saved to database");;
                    return Ok(new { id = id, data });
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"VideoCard data did not save in database. \nException: {ex}");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("getVideoCard/{id}")]
        public async Task<IActionResult> GetVideoCardById(int id)
        {
            try
            {
                
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");


                    var videoCard = connection.QueryFirstOrDefault<VideoCard<string>>("SELECT * FROM public.video_card WHERE Id = @Id",
                        new { Id = id });

                    if (videoCard != null)
                    {
                        logger.LogInformation($"Retrieved VideoCard with Id {id} from the database");
                        return Ok(new {id=id, videoCard});

                    }
                    else
                    {
                        logger.LogInformation($"VideoCard with Id {id} not found in the database");
                        return NotFound(new {error = $"VideoCard NotFound with {id}"});
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve VideoCard data from the database. \nException {ex}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPut("updateVideoCard/{id}")]
        public async Task<IActionResult> UpdateVideoCard(int id, [FromForm] UpdatedVideoCard updatedVideoCard)
        {
            try
            {
                
                if (updatedVideoCard.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }

                if (updatedVideoCard.Amount < 0)
                {
                    return BadRequest(new { error = "Amount must not be less than 0" });
                }

                if (updatedVideoCard.Power < 0 || updatedVideoCard.Power > 10)
                {
                    return BadRequest(new { error = "Power must be between 0 and 10" });
                }

                if(updatedVideoCard.Likes < 0)
                {
                    return BadRequest(new { error = "Likes must not be less than 0" });
                }

                string imagePath = string.Empty;
               
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    string filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.video_card WHERE Id = @id");

                    if (updatedVideoCard.updated)
                    {

                        BackupWriter.Delete(filePath);
                        imagePath = BackupWriter.Write(updatedVideoCard.Image);
                    }
                    else
                    {
                        imagePath = filePath;
                    }

                    var data = new
                    {
                        id = id,
                        brand = updatedVideoCard.Brand,
                        model = updatedVideoCard.Model,
                        country = updatedVideoCard.Country,
                        memory_db = updatedVideoCard.MemoryDb,
                        memory_type = updatedVideoCard.MemoryType,
                        price = updatedVideoCard.Price,
                        description = updatedVideoCard.Description,
                        image = imagePath,
                        amount = updatedVideoCard.Amount,
                        power = updatedVideoCard.Power,
                        likes = updatedVideoCard.Likes,
                    };

                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("UPDATE public.video_card SET Brand = @brand, Model = @model," +
                        " Country = @country, Memory_db = @memory_db," +
                        " Memory_type = @memory_db," +
                        " Price = @price, Description = @description," +
                        " Image = @image, Amount = @amount, Power = @power, Likes = @likes WHERE Id = @id", data);

                    logger.LogInformation("VideoCard data updated in the database");

                    return Ok(new { id = id, data });
                }

                
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update VideoCard data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("deleteVideoCard/{id}")]
        public async Task<IActionResult> DeleteVideoCard(int id)
        {
            try
            {
                string filePath;
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.video_card WHERE Id = @id", new { Id = id });
                    BackupWriter.Delete(filePath);

                    connection.Execute("DELETE FROM public.video_card WHERE Id = @id", new { id });

                    logger.LogInformation("VideoCard data deleted from the database");

                    return Ok(new {id=id});
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete VideoCard data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("getAllVideoCards")]
        public async Task<IActionResult> GetAllComputerCases(int limit, int offset)
        {
            logger.LogInformation("Get method has started");
            try
            {
                
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var videoCards = connection.Query<VideoCard<string>>("SELECT * FROM public.video_card LIMIT @Limit OFFSET @Offset",
                        new {Limit = limit, Offset = offset});

                    logger.LogInformation("Retrieved all VideoCard data from the database");

                    return Ok(new {data=videoCards});
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"VideoCard data did not get ftom database. Exception: {ex}");
                return NotFound(new {error = ex.Message});
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRam(string keyword, int limit = 1, int offset = 0)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var videoCard = connection.Query<VideoCard<string>>(@"SELECT * FROM public.video_card " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT @Limit OFFSET @Offset", new { Keyword = "%" + keyword + "%", Limit = limit, Offset = offset });

                    return Ok(new { videoCard });

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

                    var videoCards = connection.Query<VideoCard<string>>(@"SELECT * FROM public.video_card " +
                    "WHERE country = @Country " +
                    "LIMIT @Limit OFFSET @Offset", new { Country = country, Limit = limit, Offset = offset });

                    return Ok(new { videoCards });

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

                    var videoCards = connection.Query<VideoCard<string>>(@"SELECT * FROM public.video_card " +
                    "WHERE brand = @Brand " +
                    "LIMIT @Limit OFFSET @Offset", new { Brand = brand, Limit = limit, Offset = offset });

                    return Ok(new { videoCards });

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

                    var videoCards = connection.Query<VideoCard<string>>(@"SELECT * FROM public.video_card " +
                    "WHERE model = @Model " +
                    "LIMIT @Limit OFFSET @Offset", new { Model = model, Limit = limit, Offset = offset });

                    return Ok(new { videoCards });

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

                    var videoCards = connection.Query<VideoCard<string>>(@"SELECT * FROM public.video_card " +
                    "WHERE price >=  @MinPrice AND price <= @MaxPrice " +
                    "LIMIT @Limit OFFSET @Offset", new
                    {
                        MinPrice = minPrice,
                        MaxPrice = maxPrice,
                        Limit = limit,
                        Offset = offset
                    });

                    return Ok(new { videoCards });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with price filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("FilterByMemory")]
        public async Task<IActionResult> FilterByCapacity(int minMemory, int maxMemory, int limit, int offset)
        {
            try
            {
                if (minMemory < 0 || maxMemory < 0)
                {
                    return BadRequest(new { error = "memory_db must not be 0" });
                }

                if (maxMemory < minMemory)
                {
                    return BadRequest(new { error = "maxMemory could not be less than minMemory" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var videoCards = connection.Query<VideoCard<string>>(@"SELECT * FROM public.video_card " +
                    "WHERE memory_db >=  @MinCapacity AND memory_db <= @MaxCapacity " +
                    "LIMIT @Limit OFFSET @Offset", new
                    {
                        MinMemory = minMemory,
                        MaxMemory = maxMemory,
                        Limit = limit,
                        Offset = offset
                    });

                    return Ok(new { videoCards });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with memory_db filter");
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost("addComment")]
        public async Task<IActionResult> AddComputerCaseComment(Comment videoCardComment)
        {
            return await AddComment(videoCardComment, "video_card_comment");
        }
        
        [HttpPut("updateComment")]
        public async Task<IActionResult> UpdateComputerCaseComment(Comment videoCardComment)
        {
            return await UpdateComment(videoCardComment, "video_card_comment");
        }
        
        [HttpDelete("{productId}/deleteComment/{commentId}")]
        public async Task<IActionResult> DeleteComputerCaseComment(int productId, int commentId)
        {
            return await DeleteComment(productId, commentId, "video_card_comment");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "video_card_comment");
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "video_card_comment");
        }

        [HttpPut("likeVideoCard/{id}")]
        public async Task<IActionResult> LikeVideoCard(int id, User user)
        {
            return await LikeComponent(id, user, "video_card");
        }
    }
}
