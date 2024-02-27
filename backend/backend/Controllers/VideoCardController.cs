using backend.Data;
using backend.Entities;
using backend.IRepositories;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VideoCardController : ControllerBase
    {
        private readonly ILogger<VideoCardController> logger;
        private readonly DataContext dataContext;
        private readonly IVideoCardRepository videoCardRepository;

        public VideoCardController(ILogger<VideoCardController> logger, DataContext dataContext,
            IVideoCardRepository videoCardRepository)
        {
            this.logger = logger;
            this.dataContext = dataContext;
            this.videoCardRepository = videoCardRepository;
        }

        [HttpPost("createvideocard")]
        public async Task<IActionResult> CreateVideoCard(VideoCard videoCard)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await videoCardRepository.AddVideoCard(videoCard);

                logger.LogInformation("VideoCard created with ID {VideoCardId}", videoCard.Id);

                return Ok(new
                {
                    Component = "VideoCard",
                    id = videoCard.Id,
                    Data = new
                    {
                        brand = videoCard.Brand,
                        model = videoCard.Model,
                        country = videoCard.Country,
                        memory_db = videoCard.Memoty_db,
                        memory_type = videoCard.Memory_type,
                        price = videoCard.Price,
                        description = videoCard.Description,
                        image = videoCard.Image
                    }
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating VideoCard");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVideoCards()
        {
            try
            {
                var videoCards = await videoCardRepository.GetAllVideoCards();
                return Ok(videoCards);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all VideoCards");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVideoCardById(long id)
        {
            try
            {
                var videoCard = await videoCardRepository.GetVideoCardById(id);

                if (videoCard == null)
                {
                    return NotFound();
                }

                return Ok(videoCard);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting VideoCard");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideoCard(long id, VideoCard updateVideoCard)
        {
            
            try
            {
                var videocard = await videoCardRepository.GetVideoCardById(id);

                if (videocard == null)
                {
                    return NotFound();
                }

                videocard.Brand = updateVideoCard.Brand;
                videocard.Model = updateVideoCard.Model;
                videocard.Country = updateVideoCard.Country;
                videocard.Memoty_db = updateVideoCard.Memoty_db;
                videocard.Memory_type = updateVideoCard.Memory_type;
                videocard.Price = updateVideoCard.Price;
                videocard.Description = updateVideoCard.Description;
                videocard.Image = updateVideoCard.Image;

                await videoCardRepository.UpdateVideoCard(videocard);

                return Ok(new
                {
                    Component = "VideoCard",
                    id = videocard.Id,
                    Data = new
                    {
                        brand = videocard.Brand,
                        model = videocard.Model,
                        country = videocard.Country,
                        memory_db = videocard.Memoty_db,
                        memory_type = videocard.Memory_type,
                        price = videocard.Price,
                        description = videocard.Description,
                        image = videocard.Image
                    }
                });

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating VideoCard");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoCard(long id)
        {
            try
            {
                await videoCardRepository.DeleteVideoCard(id);
                return Ok($"VideoCard data with Index {id} deleted");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting VideoCard");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
