using Microsoft.AspNetCore.Mvc;
using backend.Entities;
using Npgsql;
using Dapper;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        protected readonly ILogger<OrderController> logger;
        protected readonly string connectionString;

        public OrderController(ILogger<OrderController> logger)
        {
            this.logger = logger;
            DotNetEnv.Env.Load();
            connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        }
        
        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderAdd order)
        {
            try
            {
                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    logger.LogInformation("Connection opened");
                    var orderId = await connection.ExecuteScalarAsync<int>(
                        @"INSERT INTO Orders (order_status, user_id) 
                                VALUES (@OrderStatus, @UserId) RETURNING order_id",
                        new { order.Order.OrderStatus, order.Order.UserId });
                    
                    foreach (var orderInfo in order.OrderInfo)
                    {
                        orderInfo.OrderId = orderId;
                        await connection.ExecuteAsync(
                            @"INSERT INTO orders_info (order_id, product_type, product_id) 
                                    VALUES (@OrderId, @ProductType, @ProductId)",
                            orderInfo);
                    }
                    
                    return Ok(new { orderInfo = order.OrderInfo });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while creating order");
                return BadRequest("Error occurred while creating order");
            }
        }
        
        [HttpDelete("deleteOrder/{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    logger.LogInformation("Connection opened");

                    int affectedRows = await connection.ExecuteAsync(
                        "DELETE FROM orders WHERE order_id = @OrderId",
                        new { OrderId = orderId });

                    if (affectedRows == 0)
                    {
                        return NotFound(new { error = "Order not found" });
                    }

                    logger.LogInformation($"Order {orderId} deleted successfully");
                    return Ok(new { orderId });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while deleting order");
                return BadRequest(new { error = "Error occurred while deleting order" });
            }
        }
        
        [HttpGet("getOrderInfo/{orderId}")]
        public async Task<IActionResult> GetOrderInfo(int orderId)
        {
            try
            {
                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var order = connection.Query<OrderInfo>(
                        $"SELECT order_id AS OrderId, product_type AS ProductType, product_id AS productId FROM public.orders_info" +
                        $" WHERE order_id = {orderId}" );

                    return Ok(new { orderInfo = order });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while retrieving order by ID");
                return BadRequest(new { error = "Error occurred while retrieving order by ID" });
            }
        }
        
        [HttpGet("getOrder/{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            try
            {
                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var order = connection.QueryFirstOrDefault<Order>(
                        $"SELECT order_id AS OrderId, order_status AS orderStatus, user_id AS userId FROM public.orders" +
                        $" WHERE order_id = {orderId}" );

                    return Ok(new { order });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while retrieving order by ID");
                return BadRequest(new { error = "Error occurred while retrieving order by ID" });
            }
        }
        
        [HttpGet("getOrders")]
        public async Task<IActionResult> GetOrders(int limit, int offset)
        {
            try
            {
                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    var orders = connection.Query<Order>(
                        $"SELECT order_id AS OrderId, order_status AS OrderStatus, user_id AS UserId FROM public.orders" +
                        $" LIMIT {limit} OFFSET {offset}" );

                    return Ok(new {orders});
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while retrieving order by ID");
                return BadRequest(new { error = "Error occurred while retrieving order by ID" });
            }
        }
        
        [HttpPut("updateOrderStatus/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromQuery] string newStatus)
        {
            try
            {
                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    int affectedRows = await connection.ExecuteAsync(
                        $"UPDATE orders SET order_status = '{newStatus}' WHERE order_id = {orderId}");

                    if (affectedRows == 0)
                    {
                        return NotFound(new { error = "Order not found" });
                    }
                    return Ok(new { orderId, newStatus });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while updating order status");
                return BadRequest(new { error = "Error occurred while updating order status" });
            }
        }
        
    }
    
}
