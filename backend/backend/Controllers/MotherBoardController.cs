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
    public class MotherBoardController : ControllerBase
    {
        private readonly ILogger<MotherBoardController> logger;
        private readonly DataContext dataContext;
        private readonly IMotherBoardRepository motherBoardRepository;

        public MotherBoardController(ILogger<MotherBoardController> logger, DataContext dataContext,
            IMotherBoardRepository motherBoardRepository)
        {
            this.logger = logger;
            this.dataContext = dataContext;
            this.motherBoardRepository = motherBoardRepository;
        }

        [HttpPost("createMotherBoard")]
        public async Task<IActionResult> CreateMotherBoard(MotherBoard motherBoard)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await motherBoardRepository.AddMotherBoard(motherBoard);

                logger.LogInformation("MotherBoard created with ID {CoolerId}", motherBoard.Id);

                return Ok(new
                {
                    Component = "MotherBoard",
                    id = motherBoard.Id,
                    Data = motherBoard
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating MotherBoard");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
