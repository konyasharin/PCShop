using backend.Data;
using backend.Entities;
using backend.IRepositories;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost("createVideoCard")]
        public async Task<IActionResult> CreateVideoCard(VideoCard videoCard)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await videoCardRepository.AddVideoCard(videoCard);

                logger.LogInformation("Ssd created with ID {VideoCardId}", videoCard.Id);

                return Ok(new
                {
                    Component = "VideoCard",
                    id = videoCard.Id,
                    Data = videoCard
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
        public async Task<IActionResult> UpdateVideoCard(long id, VideoCard videoCard)
        {
            if (id != videoCard.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                await videoCardRepository.UpdateVideoCard(videoCard);
                return Ok();
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
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting VideoCard");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
