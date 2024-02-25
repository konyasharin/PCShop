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

                logger.LogInformation("MotherBoard created with ID {MotherBoardID}", motherBoard.Id);

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

        [HttpGet]
        public async Task<IActionResult> GetAllMotherBoards()
        {
            try
            {
                var motherboards = await motherBoardRepository.GetAllMotherBoards();
                return Ok(motherboards);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all MotherBoards");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoolerById(long id)
        {
            try
            {
                var motherboard = await motherBoardRepository.GetMotherBoardById(id);

                if (motherboard == null)
                {
                    return NotFound();
                }

                return Ok(motherboard);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting MotherBoard");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMotherBoard(long id, MotherBoard motherBoard)
        {
            if (id != motherBoard.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                await motherBoardRepository.UpdateMotherBoard(motherBoard);
                return Ok($"MotherBoard data with Index {id} updated");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating MotherBoard");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotherBoard(long id)
        {
            try
            {
                await motherBoardRepository.DeleteMotherBoard(id);
                return Ok($"MotherBoard data with Index {id} deleted");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting MotherBoard");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
