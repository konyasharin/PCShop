using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PowerUnitController : ControllerBase
    {
        private readonly ILogger<PowerUnitController> logger;

        public PowerUnitController(ILogger<PowerUnitController> logger)
        {
            this.logger = logger;


        }

        [HttpPost("createpowerunit")]
        public async void CreatePowerUnit(PowerUnit powerunit)
        {

            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = powerunit.Id,
                        brand = powerunit.Brand,
                        model = powerunit.Model,
                        country = powerunit.Country,
                        battery = powerunit.Battery,
                        voltage = powerunit.Voltage,
                        price = powerunit.Price,
                        description = powerunit.Description,
                        image = powerunit.Image,

                    };

                    connection.Open();
                    logger.LogInformation("Connection started");
                    connection.Execute("INSERT INTO public.powerunit (Id, Brand, Model, Country, Battery, Voltage," +
                        "Price, Description, Image)" +
                        "VALUES (@Id, @Brand, @Model, @Country, @Battery, @Voltage, @Price, @Description, @Image)", powerunit);
                    logger.LogInformation("powerUnit data saved to database");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("PowerUnit data did not save in database");
            }
        }
    }
}
