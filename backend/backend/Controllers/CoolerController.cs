using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Linq.Expressions;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoolerController : ControllerBase
    {
        private readonly ILogger<CoolerController> logger;

        public CoolerController(ILogger<CoolerController> logger)
        {
            this.logger = logger;


        }

        [HttpPost("createcooler")]
        public async void CreateCooler(Cooler cooler)
        {

            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = cooler.Id,
                        brand = cooler.Brand,
                        model = cooler.Model,
                        country = cooler.Country,
                        speed = cooler.Speed,
                        power = cooler.Power,
                        price = cooler.Price,
                        description = cooler.Description,
                        image = cooler.Image,

                    };

                    connection.Open();
                    logger.LogInformation("Connection started");
                    connection.Execute("INSERT INTO public.cooler (Id, Brand, Model, Country, Speed, Power," +
                        "Price, Description, Image)" +
                        "VALUES (@Id, @Brand, @Model, @Country, @Speed, @Power, @Price, @Description, @Image)", cooler);
                    logger.LogInformation("Cooler data saved to database");
                }
            }catch(Exception ex)
            {
                logger.LogError("Cooler data did not save in database");
            }
        }
    }
}
