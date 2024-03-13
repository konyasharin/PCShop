using backend.Entities;
using backend.Password;
using backend.UpdatedEntities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

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

                if (user.Balance < 0)
                {
                    return BadRequest(new { error = "Balance must not be less than 0" });
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
                        balance = user.Balance,
                    };

                    connection.Open();
                    int id = connection.QueryFirstOrDefault<int>("INSERT INTO public.user (username, email, password, role," +
                        " balance) " +
                        "VALUES (@username, @email, @password, @role, @balance) RETURNING id", data);

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

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateProfile (int id, User updatedUser)
        {
            try
            {
                if (!updatedUser.Email.Contains("@"))
                {
                    return BadRequest(new { error = "Email does not contain @" });
                }

                if (updatedUser.Balance < 0)
                {
                    return BadRequest(new { error = "Balance must not be less than 0" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {

                    string hashedPassword = PasswordHelper.HashPassword(updatedUser.Password);

                    var data = new
                    {
                        id = id,
                        username = updatedUser.Username,
                        email = updatedUser.Email,
                        password = hashedPassword,
                        balance = updatedUser.Balance,
                    };

                    connection.Open();
                    connection.Execute("UPDATE public.user SET username = @username, email = @email,"
                + " password = @password, balance = @balance WHERE id = @id", data);

                    logger.LogInformation("User data updated in the database");
                    return Ok(new {id = id, data});

                }
            }
            catch(Exception ex)
            {
                logger.LogError("Error with edit profile");
                return StatusCode(500, new {error = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProfile (int id)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {

                    

                    connection.Open();
                    connection.Execute("DELETE FROM public.user WHERE Id = @id", new { id });

                    logger.LogInformation("User data deleted in the database");
                    return Ok(new { id = id});

                }
            }
            catch(Exception ex)
            {
                logger.LogError("Error with delete profile");
                return StatusCode(500, new { error = ex.Message });
            }
        }

     
    }
}
