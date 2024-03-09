using backend.Entities;
using backend.Password;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ComponentController
    {
        public UserController(ILogger<UserController> logger) :base(logger)
        { }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUpUser(User user)
        {
            try
            {
                if (!user.Email.Contains("@"))
                {
                    return BadRequest(new { error = "Email does not contain @" });
                }


                await using var connection = new NpgsqlConnection(connectionString);
                {
                    string hashedPassword = PasswordHelper.HashPassword(user.Password);
                    
                    var data = new
                    {
                        username = user.Username,
                        email = user.Email,
                        password = hashedPassword,
                        role = user.Role,
                    };

                    connection.Open();
                    int id = connection.QueryFirstOrDefault<int>("INSERT INTO public.user (username, email, password, role) " +
                        "VALUES (@username, @email, @password, @role) RETURNING id", data);

                    logger.LogInformation($"User with {id} signed up");

                    return Ok(new { id = id, data });
                }
            }

            catch (Exception ex)
            {
                logger.LogError("Error with sign up user");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("signIn")]
        public async Task<IActionResult> SignInUser(string email, string password)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var userData = await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM public.user WHERE email = @Email",
                        new { Email = email });

                    if (userData == null)
                    {
                        return BadRequest(new { error = "User with this email not found" });
                    }

                    if (!PasswordHelper.VerifyPassword(userData.Password, password))
                    {
                        return BadRequest(new { error = "Incorrect password" });
                    }

                    logger.LogInformation($"User with email {email} signed in successfully");
                    return Ok(new { userData });
                }
            }
            catch(Exception ex)
            {
                logger.LogError("Error with sign in user");
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
