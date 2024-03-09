using backend.Entities;
using backend.UpdatedEntities;
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
    public class MotherBoardController : ComponentController
    {

        public MotherBoardController(ILogger<MotherBoardController> logger):base(logger)
        {
          
        }

        [HttpPost("createMotherBoard")]
        public async Task<IActionResult> CreateMotherBoard([FromForm] MotherBoard<IFormFile> motherBoard)
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
                return BadRequest(new { error = ex.Message });
               
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


                    var motherBoard = connection.QueryFirstOrDefault<MotherBoard<string>>("SELECT * FROM public.mother_board WHERE Id = @Id",
                        new { Id = id });

                    if (motherBoard != null)
                    {
                        logger.LogInformation($"Retrieved MotherBoard with Id {id} from the database");
                        return Ok(new { id = id, motherBoard });

                    }
                    else
                    {
                        logger.LogInformation($"MotherBoard with Id {id} not found in the database");
                        return NotFound(new { error = $"MotherBoard NotFound wit {id}"});
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve MotherBoard data from the database. \nException {ex}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPut("updateMotherBoard/{id}")]
        public async Task<IActionResult> UpdateMotherBoard(int id, [FromForm] UpdatedMotherBoard updatedMotherBoard)
        {
            try
            {
               

                if (updatedMotherBoard.Frequency < 0 || updatedMotherBoard.Frequency > 100000)
                {
                    return BadRequest(new { error = "Frequency must be between 0 and 100000" });
                }

                if (updatedMotherBoard.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }
                string imagePath = string.Empty;

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    string filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.mother_board WHERE Id = @id");

                    if (updatedMotherBoard.updated)
                    {

                        BackupWriter.Delete(filePath);
                        imagePath = BackupWriter.Write(updatedMotherBoard.Image);
                    }
                    else
                    {
                        imagePath = filePath;
                    }

                    var data = new
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
                        image = imagePath,
                    };

                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("UPDATE public.mother_board SET Brand = @brand, Model = @model, Country = @country, Frequency = @frequency," +
                        " Socket = @socket, Chipset = @chipset," +
                        " Price = @price, Description = @description, Image = @image WHERE Id = @id", data);

                    logger.LogInformation("MotherBoard data updated in the database");

                    return Ok(new { id = id, data});

                }

                
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update MotherBoard data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("deleteMotherBoard/{id}")]
        public async Task<IActionResult> DeleteMotherBoard(int id)
        {
            try
            {
                string filePath;
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.mother_board WHERE Id = @id", new { Id = id });
                    BackupWriter.Delete(filePath);

                    connection.Execute("DELETE FROM public.mother_board WHERE Id = @id", new { id });

                    logger.LogInformation("MotherBoard data deleted from the database");

                    //id удалённого компонента
                    return Ok(new {id = id});
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete MotherBoard data in database. \nException: {ex}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("getAllMotherBoards")]
        public async Task<IActionResult> GetAllMotherBoards(int limit, int offset)
        {
            logger.LogInformation("Get method has started");
            try
            {
               

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var motherboards = connection.Query<MotherBoard<string>>("SELECT * FROM public.mother_board LIMIT @Limit OFFSET @Offset",
                        new {Limit = limit, Offset = offset});

                    logger.LogInformation("Retrieved all MotherBoard data from the database");

                    return Ok(new { motherboards });
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"MotherBoard data did not get gtom database. Exception: {ex}");
                return NotFound(new {error = ex.Message});
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMotherBoard(string keyword, int limit, int offset)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var motherBoards = connection.Query<MotherBoard<string>>(@"SELECT * FROM public.mother_board " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT @Limit OFFSET @Offset", new { Keyword = "%" + keyword + "%", Limit = limit, Offset = offset });

                    return Ok(new { motherBoards });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with search");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
