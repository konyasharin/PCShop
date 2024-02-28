using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProcessorController : ControllerBase
    {
        private readonly ILogger<ProcessorController> logger;

        public ProcessorController(ILogger<ProcessorController> logger)
        {
            this.logger = logger;


        }

        [HttpPost("createprocessor")]
        public async void CreateProcessor(Processor processor)
        {

            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = processor.Id,
                        brand = processor.Brand,
                        model = processor.Model,
                        country = processor.Country,
                        clock_frquency = processor.Clock_frequency,
                        turbo_frequency = processor.Turbo_frequency,
                        heat_dissipation = processor.Heat_dissipation,
                        price = processor.Price,
                        description = processor.Description,
                        image = processor.Image,

                    };

                    connection.Open();
                    logger.LogInformation("Connection started");
                    connection.Execute("INSERT INTO public.processor (Id, Brand, Model, Country, Clock_frequency, Turbo_frequency, Heat_dissipation" +
                        "Price, Description, Image)" +
                        "VALUES (@Id, @Brand, @Model, @Country, @Clock_frequency, @Turbo_frequency, @Heat_dissipation, @Price, @Description, @Image)", processor);
                    logger.LogInformation("Processor data saved to database");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Processor data did not save in database");
            }
        }
    }
}
