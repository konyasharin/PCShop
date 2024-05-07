using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController: ControllerBase
{
    protected readonly ILogger<ProductController> logger;
    protected readonly string connectionString;
        
    public ProductsController(ILogger<ProductController> logger)
    {
        this.logger = logger;
        DotNetEnv.Env.Load();
        connectionString = Environment.GetEnvironmentVariable("ConnectionString");
    }
    
    [HttpGet("getProduct/{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        try
        {
            await using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var product = connection.QueryFirstOrDefault<Product<string>>(
                    $"SELECT product_id AS ProductId, brand, model, country, price, description, image, amount, power, likes, product_type AS productType FROM public.products" +
                    $" WHERE product_id = {id}" );

                return Ok(new {product});
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while retrieving product by ID");
            return BadRequest(new { error = "Error occurred while retrieving product by ID" });
        }
    }
}