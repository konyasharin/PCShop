﻿using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("CreateMotherBoard")]
        public async void CreateMotherBoard(MotherBoard motherBoard)
        {

            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = motherBoard.Id,
                        brand = motherBoard.Brand,
                        model = motherBoard.Model,
                        country = motherBoard.Country,
                        frequency = motherBoard.Frequency,
                        socket = motherBoard.Socket,
                        chipset = motherBoard.Chipset,
                        price = motherBoard.Price,
                        description = motherBoard.Description,
                        image = motherBoard.Image,

                    };

                    connection.Open();
                    logger.LogInformation("Connection started");
                    connection.Execute("INSERT INTO public.motherboard (Id, Brand, Model, Country, Frequency, Socket, Chipset," +
                        "Price, Description, Image)" +
                        "VALUES (@Id, @Brand, @Model, @Country, @Frequency, @Socket, @Chipset, @Price, @Description, @Image)", motherBoard);
                    logger.LogInformation("MotherBoard data saved to database");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"MotherBoard data did not save in database. Exception: {ex}");
               
            }
        }

        [HttpGet("GetMotherBoard/{id}")]
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

        [HttpPut("UpdateMotherBoard/{id}")]
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

        [HttpDelete("DeleteMotherBoard/{id}")]
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

        [HttpGet("GetAllMotherBoards")]
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
