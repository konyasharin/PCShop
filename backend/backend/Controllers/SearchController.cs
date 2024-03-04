using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace backend.Controllers
{
    [Route("api")]
    [ApiController]
    public class SearchController : ComponentController
    {
        public SearchController(ILogger<SearchController> logger) : base(logger)
        { }

        [HttpGet("search")]
        public async Task<IActionResult> SearchComponent (string keyword)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var computerCases = connection.Query<ComputerCase<string>>(@"SELECT * FROM public.computer_case " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT 3", new { Keyword = "%" + keyword + "%" });

                    var processors = connection.Query<Processor<string>>(@"SELECT * FROM public.processor " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT 3", new { Keyword = "%" + keyword + "%" });

                    var coolers = connection.Query<Cooler<string>>(@"SELECT * FROM public.cooler " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT 3", new { Keyword = "%" + keyword + "%" });

                    var motherBoards = connection.Query<MotherBoard<string>>(@"SELECT * FROM public.mother_board " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT 3", new { Keyword = "%" + keyword + "%" });

                    var powerUnits = connection.Query<PowerUnit<string>>(@"SELECT * FROM public.power_unit " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT 3", new { Keyword = "%" + keyword + "%" });

                    var ssd = connection.Query<SSD<string>>(@"SELECT * FROM public.ssd " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT 3", new { Keyword = "%" + keyword + "%" });

                    var ram = connection.Query<RAM<string>>(@"SELECT * FROM public.ssd " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT 3", new { Keyword = "%" + keyword + "%" });

                    var allResults = new List<object>();
                    allResults.AddRange(computerCases);
                    allResults.AddRange(processors);
                    allResults.AddRange(coolers);
                    allResults.AddRange(motherBoards);
                    allResults.AddRange(powerUnits);
                    allResults.AddRange(ssd);
                    allResults.AddRange(ram);

                    foreach(object i in allResults){
                        Console.WriteLine(i);
                    }
                    


                    return Ok(new { allResults});

                }
            }
            catch(Exception ex)
            {
                logger.LogError("Error with search");
                return BadRequest(new {error = ex.Message});
            }
        }
    }
}
