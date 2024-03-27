using backend.Entities;
using backend.Entities.CommentEntities;
using backend.Entities.ComponentsInfo;
using backend.Entities.User;
using backend.UpdatedEntities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessorController : ComponentController
    {
        
        public ProcessorController(ILogger<ProcessorController> logger):base(logger)
        {
           
        }

        [HttpPost("createProcessor")]
        public async Task<IActionResult> CreateProcessor([FromForm] Processor<IFormFile> processor)
        {

           
                if ((processor.Clock_frequency < 0 || processor.Clock_frequency >= 50000)
                    && (processor.Clock_frequency > processor.Turbo_frequency))
                {
                    return BadRequest(new { error = "Clock_frequency must be between 0 and 50000 and less than turbo_frequency" });
                }

                if ((processor.Turbo_frequency < processor.Clock_frequency)
                    && (processor.Turbo_frequency >= 100000 || processor.Turbo_frequency < 0))
                {
                    return BadRequest(new { error = "Turbo_frequency must be bigger than clock_frequency and 100000 and less than 0" });
                }

                if (processor.Cores < 0 || processor.Cores >= 8)
                {
                    return BadRequest(new { error = "Cores most be between 0 and 8" });
                }

                if (processor.Heat_dissipation < 0 || processor.Heat_dissipation > 10000)
                {
                    return BadRequest(new { error = "Heat_dissipation must be between 0 and 10000" });
                }

               
                processor.Likes = 0;

            return await CreateComponent<Processor<IFormFile>>(processor, ["cores", "heat_dissipation", "clock_frequency", "turbo_frequency"], "processor");
        }

        [HttpGet("getProcessor/{id}")]
        public async Task<IActionResult> GetProcessorById(int id)
        {
            return await getComponent<PowerUnitInfo>(id, "power_unit", ["battery", "voltage"]);
        }

        [HttpPut("updateProcessor/{id}")]
        public async Task<IActionResult> UpdateProcessor(int id, [FromForm] Processor<IFormFile> processor, [FromQuery] bool isUpdated)
        {
            processor.ProductId = id;
            return await UpdateComponent<Processor<IFormFile>>(processor, isUpdated, "processor",
                ["cores", "clock_frequency", "turbo_frequency", "heat_dissipation"]);
        }

        [HttpDelete("deleteProcessor/{id}")]
        public async Task<IActionResult> DeleteProcessor(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("getAllProcessors")]
        public async Task<IActionResult> GetAllprocessors(int limit, int offset)
        {
            return await GetAllComponents<ProcessorInfo>(limit, offset, "processor",
                ["cores", "clock_frequency", "turbo_frequency", "heat_dissipation"]);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProcessor(string keyword, int limit = 1, int offset = 0)
        {
            return await SearchComponent(keyword, limit, offset);
        }

        [HttpGet("FilterByCountry")]
        public async Task<IActionResult> FilterByCountry(string country, int limit, int offset)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var processors = connection.Query<Processor<string>>(@"SELECT * FROM public.processor " +
                    "WHERE country = @Country " +
                    "LIMIT @Limit OFFSET @Offset", new { Country = country, Limit = limit, Offset = offset });

                    return Ok(new { processors });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with country filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("FilterByBrand")]
        public async Task<IActionResult> FilterByBrand(string brand, int limit, int offset)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var processors = connection.Query<Processor<string>>(@"SELECT * FROM public.processor " +
                    "WHERE brand = @Brand " +
                    "LIMIT @Limit OFFSET @Offset", new { Brand = brand, Limit = limit, Offset = offset });

                    return Ok(new { processors });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with brand filter");
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet("FilterByModel")]
        public async Task<IActionResult> FilterByModel(string model, int limit, int offset)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var processors = connection.Query<Processor<string>>(@"SELECT * FROM public.processor " +
                    "WHERE model = @Model " +
                    "LIMIT @Limit OFFSET @Offset", new { Model = model, Limit = limit, Offset = offset });

                    return Ok(new { processors });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with model filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("FilterByPrice")]
        public async Task<IActionResult> FilterByPrice(int minPrice, int maxPrice, int limit, int offset)
        {
            try
            {
                if (minPrice < 0 || maxPrice < 0)
                {
                    return BadRequest(new { error = "price must not be 0" });
                }

                if (maxPrice < minPrice)
                {
                    return BadRequest(new { error = "maxPrice could not be less than minPrice" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var processors = connection.Query<Processor<string>>(@"SELECT * FROM public.processor " +
                    "WHERE price >=  @MinPrice AND price <= @MaxPrice " +
                    "LIMIT @Limit OFFSET @Offset", new
                    {
                        MinPrice = minPrice,
                        MaxPrice = maxPrice,
                        Limit = limit,
                        Offset = offset
                    });

                    return Ok(new { processors });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with price filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("FilterByFrequency")]
        public async Task<IActionResult> FilterByVoltage(int minClockFrequency, int maxClockFrequency, int limit, int offset)
        {
            try
            {
                if (minClockFrequency < 0 || maxClockFrequency < 0)
                {
                    return BadRequest(new { error = "ClockFrequency must not be 0" });
                }

                if (maxClockFrequency < minClockFrequency)
                {
                    return BadRequest(new { error = "maxClockFrequency could not be less than minClockFrequency" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var processors = connection.Query<Processor<string>>(@"SELECT * FROM public.processor " +
                    "WHERE clock_frequency >=  @MinClockFrequency AND clock_frequency <= @MaxClockFrequency " +
                    "LIMIT @Limit OFFSET @Offset", new
                    {
                        MinClockFrequency = minClockFrequency,
                        MaxClockFrequency = maxClockFrequency,
                        Limit = limit,
                        Offset = offset
                    });

                    return Ok(new { processors });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with ClockFrequency filter");
                return BadRequest(new { error = ex.Message });
            }
        }
        
        [HttpPost("addComment")]
        public async Task<IActionResult> AddComputerCaseComment(Comment computerCaseComment)
        {
            return await AddComment(computerCaseComment);
        }
        
        [HttpPut("updateComment")]
        public async Task<IActionResult> UpdateComputerCaseComment(Comment computerCaseComment)
        {
            return await UpdateComment(computerCaseComment);
        }
        
        [HttpDelete("{productId}/deleteComment/{commentId}")]
        public async Task<IActionResult> DeleteComputerCaseComment(int productId, int commentId)
        {
            return await DeleteComment(productId, commentId, "processor");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int productId, int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "processor", productId);
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "processor");
        }

        [HttpPut("likeProcessor/{id}")]
        public async Task<IActionResult> LikeProcessor(int id, User user)
        {
            return await LikeComponent(id, user, "processor");
        }
    }
}
