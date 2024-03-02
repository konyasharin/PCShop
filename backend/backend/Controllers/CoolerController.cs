﻿using backend.Entities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoolerController : ComponentController
    {
        private readonly ILogger<CoolerController> logger;

        public CoolerController(ILogger<CoolerController> logger):base(logger)
        { 
        }

        [HttpPost("createCooler")]
        public async Task<IActionResult> CreateCooler(Cooler<IFormFile> cooler)
        {
            try
            {
                string imagePath = BackupWriter.Write(cooler.Image);

                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                if (cooler.Speed <= 0 || cooler.Speed > 10000)
                {
                    return BadRequest(new { error = "Speed must be between 0 and 10000" });
                }

                if (cooler.Power < 0 || cooler.Power > 10000)
                {
                    return BadRequest(new { error = "Power must be between 0 and 10000" });
                }

                if (cooler.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less then 0" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var data = new
                    {
                        brand = cooler.Brand,
                        model = cooler.Model,
                        country = cooler.Country,
                        speed = cooler.Speed,
                        power = cooler.Power,
                        price = cooler.Price,
                        description = cooler.Description,
                        image = imagePath,
                    };

                    connection.Open();
                    int id = connection.QueryFirstOrDefault<int>("INSERT INTO public.cooler (brand, model, country, speed, power," +
                        "price, description, image)" +
                        "VALUES (@brand, @model, @country, @speed, @power, @price, @description, @image) RETURNING id", data);

                    logger.LogInformation("Cooler data saved to database");

                    return Ok(new { id = id, data });
                }
            }catch(Exception ex)
            {
                logger.LogError("Cooler data did not save in database");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("getCooler/{id}")]
        public async Task<IActionResult> GetCoolerById(int id)
        {
            try
            {
                

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");


                    var cooler = connection.QueryFirstOrDefault<Cooler<string>>("SELECT * FROM public.cooler WHERE Id = @Id",
                        new { Id = id });

                    if (cooler != null)
                    {
                        logger.LogInformation($"Retrieved Cooler with Id {id} from the database");
                        return Ok(new { id = id, cooler });

                    }
                    else
                    {
                        logger.LogInformation($"Cooler with Id {id} not found in the database");
                        return NotFound(new {error = $"Cooler Not Found with {id}"});
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve Cooler data from the database. \nException {ex}");
                return StatusCode(500, new { error = ex.Message});
            }
        }

        [HttpPut("updateCooler/{id}")]
        public async Task<IActionResult> UpdateCooler(int id, Cooler<IFormFile> updatedCooler)
        {
            try
            {
                
                if (updatedCooler.Speed > 0 || updatedCooler.Speed < 10000)
                {
                    return BadRequest(new { error = "Speed must be between 0 and 10000" });
                }
                
                if (updatedCooler.Power > 0 || updatedCooler.Power < 10000)
                {
                    return BadRequest(new { error = "Speed must be between 0 and 10000" });
                }

                if (updatedCooler.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = id,
                        brand = updatedCooler.Brand,
                        model = updatedCooler.Model,
                        country = updatedCooler.Country,
                        speed = updatedCooler.Speed,
                        power = updatedCooler.Power,
                        price = updatedCooler.Price,
                        description = updatedCooler.Description,
                        image = updatedCooler.Image
                    };

                }

                connection.Open();
                logger.LogInformation("Connection started");

                connection.Execute("UPDATE public.cooler SET Brand = @brand, Model = @model, Country = @country, Speed = @speed," +
                    " Power = @power," +
                    " Price = @price, Description = @description, Image = @image WHERE Id = @id", updatedCooler);

                logger.LogInformation("Cooler data updated in the database");

                return Ok("Cooler data updated successfully");
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update Cooler data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("deleteCooler/{id}")]
        public async Task<IActionResult> DeleteCooler(int id)
        {
            try
            {
             

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("DELETE FROM public.cooler WHERE Id = @id", new { id });

                    logger.LogInformation("Cooler data deleted from the database");

                    return Ok(new {id = id});
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete Cooler data in database. \nException: {ex}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("getAllCoolers")]
        public async Task<IActionResult> GetAllCoolers()
        {
            logger.LogInformation("Get method has started");
            try
            {
               

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var coolers = connection.Query<Cooler<string>>("SELECT * FROM public.cooler");

                    logger.LogInformation("Retrieved all Cooler data from the database");

                    return Ok(new { data = coolers });
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"Cooler data did not get gtom database. Exception: {ex}");
                return NotFound(new {error = ex.Message});
            }
        }
    }
}
