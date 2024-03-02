using backend.Entities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotherBoardController : ControllerBase
    {
        private readonly ILogger<MotherBoardController> logger;

        public MotherBoardController(ILogger<MotherBoardController> logger)
        {
            this.logger = logger;


        }

        [HttpPost("createMotherBoard")]
        public async Task<IActionResult> CreateMotherBoard(MotherBoard motherBoard)
        {
            if (motherBoard.Frequency < 0 || motherBoard.Frequency > 100000)
            {
                return BadRequest(new { error = "Frequency must be between 0 and 100000" });
            }

            if (motherBoard.Price < 0)
            {
                return BadRequest(new { error = "Price must not be less than 0" });
            }

            try
            {
                string imagePath = BackupWriter.Write(motherBoard.Image);
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var data = new
                    {
                        brand = motherBoard.Brand,
                        model = motherBoard.Model,
                        country = motherBoard.Country,
                        frequency = motherBoard.Frequency,
                        socket = motherBoard.Socket,
                        chipset = motherBoard.Chipset,
                        price = motherBoard.Price,
                        description = motherBoard.Description,
                        image = imagePath,
                    };

                    connection.Open();
                    int id = connection.QueryFirstOrDefault<int>("INSERT INTO public.mother_board (brand, model, country, frequency, socket, chipset," +
                        "price, description, image)" +
                        "VALUES (@brand, @model, @country, @frequency, @socket, @chipset, @price, @description, @image) RETURNING id", data);

                    logger.LogInformation("MotherBoard data saved to database");
                    return Ok(new { id =  id, data});
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"MotherBoard data did not save in database. Exception: {ex}");
                return BadRequest(ex.Message);
               
            }
        }

        [HttpGet("getMotherBoard/{id}")]
        public async Task<IActionResult> GetComputerCaseById(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");


                    var motherBoard = connection.QueryFirstOrDefault<MotherBoard>("SELECT * FROM public.mother_board WHERE Id = @Id",
                        new { Id = id });

                    if (motherBoard != null)
                    {
                        logger.LogInformation($"Retrieved MotherBoard with Id {id} from the database");
                        return Ok(motherBoard);

                    }
                    else
                    {
                        logger.LogInformation($"MotherBoard with Id {id} not found in the database");
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve MotherBoard data from the database. \nException {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("updateMotherBoard/{id}")]
        public async Task<IActionResult> UpdateMotherBoard(int id, MotherBoard updatedMotherBoard)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                if (updatedMotherBoard.Frequency < 0 || updatedMotherBoard.Frequency > 100000)
                {
                    return BadRequest("Frequency must be between 0 and 100000");
                }

                if (updatedMotherBoard.Price < 0)
                {
                    return BadRequest("Price must not be less than 0");
                }
                
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = id,
                        brand = updatedMotherBoard.Brand,
                        model = updatedMotherBoard.Model,
                        country = updatedMotherBoard.Country,
                        frequency = updatedMotherBoard.Frequency,
                        socket = updatedMotherBoard.Socket,
                        chipset = updatedMotherBoard.Chipset,
                        price = updatedMotherBoard.Price,
                        description = updatedMotherBoard.Description,
                        image = updatedMotherBoard.Image
                    };

                }

                connection.Open();
                logger.LogInformation("Connection started");

                connection.Execute("UPDATE public.mother_board SET Brand = @brand, Model = @model, Country = @country, Frequency = @frequency," +
                    " Socket = @socket, Chipset = @chipset," +
                    " Price = @price, Description = @description, Image = @image WHERE Id = @id", updatedMotherBoard);

                logger.LogInformation("MotherBoard data updated in the database");

                return Ok("MotherBoard data updated successfully");
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update MotherBoard data in database. \nException: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("deleteMotherBoard/{id}")]
        public async Task<IActionResult> DeleteMotherBoard(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("DELETE FROM public.mother_board WHERE Id = @id", new { id });

                    logger.LogInformation("MotherBoard data deleted from the database");

                    return Ok("MotherBoard data deleted successfully");
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete MotherBoard data in database. \nException: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getAllMotherBoards")]
        public async Task<IActionResult> GetAllMotherBoards()
        {
            logger.LogInformation("Get method has started");
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var motherboards = connection.Query<MotherBoard>("SELECT * FROM public.mother_board");

                    logger.LogInformation("Retrieved all MotherBoard data from the database");

                    return Ok(motherboards);
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"MotherBoard data did not get gtom database. Exception: {ex}");
                return NotFound();
            }
        }
    }
}
