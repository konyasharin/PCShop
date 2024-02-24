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

        [HttpPost("createSsd")]
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
    }
}
