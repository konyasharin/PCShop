using backend.Entities;
using backend.Entities.CommentEntities;
using backend.Entities.ComponentsInfo;
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
            
                videoCard.Likes = 0;

            return await CreateComponent<VideoCard<IFormFile>>(videoCard, ["memory_db", "memory_type"], "video_card");
        }

        [HttpGet("getVideoCard/{id}")]
        public async Task<IActionResult> GetVideoCardById(int id)
        {
            return await getComponent<VideoCardInfo>(id, "video_card", ["memory_db", "memory_type"]);
        }

        [HttpPut("updateVideoCard/{id}")]
        public async Task<IActionResult> UpdateVideoCard(int id, [FromForm] VideoCard<IFormFile> videoCard, [FromQuery] bool isUpdated)
        {
            videoCard.ProductId = id;
            return await UpdateComponent<VideoCard<IFormFile>>(videoCard, isUpdated, "video_card",
                ["memory_db", "memory_type"]);
        }

        [HttpDelete("deleteVideoCard/{id}")]
        public async Task<IActionResult> DeleteVideoCard(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("getAllVideoCards")]
        public async Task<IActionResult> GetAllComputerCases(int limit, int offset)
        {
            return await GetAllComponents<VideoCardInfo>(limit, offset, "video_card",
               ["memory_db", "memory_type"]);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRam(string keyword, int limit = 1, int offset = 0)
        {
            return await SearchComponent(keyword, limit, offset);
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
            return await AddComment(videoCardComment);
        }
        
        [HttpPut("updateComment")]
        public async Task<IActionResult> UpdateComputerCaseComment(Comment videoCardComment)
        {
            return await UpdateComment(videoCardComment);
        }
        
        [HttpDelete("{productId}/deleteComment/{commentId}")]
        public async Task<IActionResult> DeleteComputerCaseComment(int productId, int commentId)
        {
            return await DeleteComment(productId, commentId, "video_card");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int productId, int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "video_card", productId);
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "video_card");
        }

        [HttpPut("likeVideoCard/{id}")]
        public async Task<IActionResult> LikeVideoCard(int id, User user)
        {
            return await LikeComponent(id, user, "video_card");
        }
    }
}
