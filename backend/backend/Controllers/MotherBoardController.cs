using backend.Data;
using backend.Entities;
using backend.IRepositories;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

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
                    Data = new
                    {
                        brand = motherBoard.Brand,
                        model = motherBoard.Model,
                        country = motherBoard.Country,
                        frequency = motherBoard.Frequency,
                        socket = motherBoard.Socket,
                        chipset = motherBoard.Chipset,
                        price = motherBoard.Price,
                        description = motherBoard.Description,
                        image = motherBoard.Image
                    }
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
        public async Task<IActionResult> GetMotherBoardById(long id)
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
        public async Task<IActionResult> UpdateMotherBoard(long id, MotherBoard updatedMotherBoard)
        {
            try
            {
                var motherBoard = await motherBoardRepository.GetMotherBoardById(id);

                if (motherBoard == null)
                {
                    return NotFound();
                }

                motherBoard.Brand = updatedMotherBoard.Brand;
                motherBoard.Model = updatedMotherBoard.Model;
                motherBoard.Country = updatedMotherBoard.Country;
                motherBoard.Frequency = updatedMotherBoard.Frequency;
                motherBoard.Socket = updatedMotherBoard.Socket;
                motherBoard.Chipset = updatedMotherBoard.Chipset;
                motherBoard.Price = updatedMotherBoard.Price;
                motherBoard.Description = updatedMotherBoard.Description;
                motherBoard.Image = updatedMotherBoard.Image;

                await motherBoardRepository.UpdateMotherBoard(motherBoard);

                return Ok(new
                {
                    Component = "MotherBoard",
                    id = motherBoard.Id,
                    Data = new
                    {
                        brand = motherBoard.Brand,
                        model = motherBoard.Model,
                        country = motherBoard.Country,
                        frequency = motherBoard.Frequency,
                        socket = motherBoard.Socket,
                        chipset = motherBoard.Chipset,
                        price = motherBoard.Price,
                        description = motherBoard.Description,
                        image = motherBoard.Image
                    }
                });

            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error updating Motherboard");
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
