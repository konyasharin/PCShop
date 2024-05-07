using backend.Entities;
using backend.Entities.User;
using backend.Password;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        protected readonly ILogger<CartController> logger;
        protected readonly string connectionString;

        public CartController(ILogger<CartController> logger)
        {
            this.logger = logger;
            DotNetEnv.Env.Load();
            connectionString = Environment.GetEnvironmentVariable("ConnectionString");

        }

        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProductInCard(Cart cart)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var data = new
                    {
                        user_id = cart.UserId,
                        product_id = cart.ProductId,
                    };

                    connection.Open();
                    int id = connection.QueryFirstOrDefault<int>("INSERT INTO public.cart (user_id, product_id) " +
                        "VALUES (@user_id, @product_id) RETURNING cart_id", data);

                    logger.LogInformation($"Product with {cart.ProductId} and user with {cart.UserId} were added in cart");

                    return Ok(new {id = id, data = data});

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with add in cart");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("deleteProduct/{productId}")]
        public async Task<IActionResult> DeleteProductFromCart(int productId)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    

                    connection.Open();
                    connection.Execute($"DELETE FROM public.cart WHERE product_id = @product_id", new {product_id = productId});

                    logger.LogInformation($"Product with Id {productId} wer deleted from cart");

                    return Ok(new { id = productId});

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with delete from cart");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("getProducts")]
        public async Task<IActionResult> GetAllProductsFromCart()
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    var products = await connection.QueryAsync<Cart>("SELECT cart_id AS CartId, user_id AS UserId, product_id AS ProductId FROM public.cart");

                    var productsArray = products.ToArray();

                    return Ok(new { productsArray });
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with get all data from cart");
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
