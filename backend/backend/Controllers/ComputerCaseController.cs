using backend.Data;
using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComputerCaseController : ControllerBase
    {
        private readonly ILogger<ComputerCaseController> logger;
      

        public ComputerCaseController(ILogger<ComputerCaseController> logger)
        {
            this.logger = logger;
    
        }

        [HttpPost("createcomputercase")]
        public async void CreateCreateCase(ComputerCase computerCase)
        {

            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = computerCase.Id,
                        brand = computerCase.Brand,
                        model = computerCase.Model,
                        country = computerCase.Country,
                        material = computerCase.Material,
                        width = computerCase.Width,
                        height = computerCase.Height,
                        depth = computerCase.Depth,
                        price = computerCase.Price,
                        description = computerCase.Description,
                        image = computerCase.Image,

                    };

                    connection.Open();
                    logger.LogInformation("Connection started");
                    connection.Execute("INSERT INTO public.computer_case (Id, Brand, Model, Country, Material, Width, Height, Depth," +
                        "Price, Description, Image)" +
                        "VALUES (@Id, @Brand, @Model, @Country, @Material, @Width, @Height, @Depth, @Price, @Description, @Image)", computerCase);
                    logger.LogInformation("ComputerCase data saved to database");
                }
            }
            catch(Exception ex)
            {
                logger.LogError("ComputerCase data did not save in database");
            }
        }
    }
}
