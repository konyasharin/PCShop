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
        
        public class OrderCreationModel
        {
            public Orders Order { get; set; }
            public List<OrdersInfo> OrderInfoList { get; set; }
        }
        
        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreationModel orderModel)
        {
            try
            {
                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    logger.LogInformation("Connection opened");
                    
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var orderId = await connection.ExecuteScalarAsync<int>(
                                @"INSERT INTO Orders (order_status, user_id) 
                                VALUES (@OrderStatus, @UserId) RETURNING order_id",
                                new { orderModel.Order.OrderStatus, orderModel.Order.UserId }, transaction);
                            
                            foreach (var orderInfo in orderModel.OrderInfoList)
                            {
                                orderInfo.OrderId = orderId;
                                await connection.ExecuteAsync(
                                    @"INSERT INTO orders_info (order_id, product_type, product_id) 
                                    VALUES (@OrderId, @ProductType, @ProductId)",
                                    orderInfo, transaction);
                            }
                            
                            transaction.Commit();

                            logger.LogInformation("Order created successfully");
                            
                            return Ok(new { OrderId = orderId, OrderInfo = orderModel.OrderInfoList });
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
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
        
        [HttpGet("getOrder/{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            logger.LogInformation(orderId.ToString());
            try
            {
                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var order = connection.QueryFirstOrDefault<Orders>(
                        $"SELECT order_id AS OrderId, order_status AS OrderStatus, user_id AS UserId FROM public.orders" +
                        $" WHERE order_id = {orderId}" );
                    logger.LogInformation(order.OrderStatus);

                    if (order == null)
                    {
                        return NotFound(new { error = "Order not found" });
                    }

                    return Ok(new {order});
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while retrieving order by ID");
                return BadRequest(new { error = "Error occurred while retrieving order by ID" });
            }
        }
        
        [HttpPut("updateOrderStatus/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] string newStatus)
        {
            try
            {
                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    logger.LogInformation("Connection opened");

                    int affectedRows = await connection.ExecuteAsync(
                        "UPDATE orders SET order_status = @NewStatus WHERE order_id = @OrderId",
                        new { NewStatus = newStatus, OrderId = orderId });

                    if (affectedRows == 0)
                    {
                        return NotFound(new { error = "Order not found" });
                    }

                    logger.LogInformation($"Order {orderId} status updated successfully");
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
