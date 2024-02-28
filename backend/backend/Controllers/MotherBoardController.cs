using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MotherBoardController : ControllerBase
    {
        private readonly ILogger<MotherBoardController> logger;

        public MotherBoardController(ILogger<MotherBoardController> logger)
        {
            this.logger = logger;


        }

        [HttpPost("createmotherboard")]
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
                logger.LogError("MotherBoard data did not save in database");
            }
        }
    }
}
