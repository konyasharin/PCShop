using backend.Entities.User;
using backend.Password;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected readonly ILogger<UserController> logger;
        protected readonly string connectionString;
        private readonly JwtProvider _jwtProvider;

        public UserController(ILogger<UserController> logger)
        {
            this.logger = logger;
            DotNetEnv.Env.Load();
            connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            _jwtProvider = new JwtProvider();

        }
        
        /// <summary>
        /// Метод для регистрации нового пользователя.
        /// </summary>
        /// <param name="user">Пользователь для регистрации.</param>
        /// <returns>Результат операции регистрации пользователя.</returns>
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
                        role = UserRoles.EUserRole.User,
                        balance = 0,
                    };

                    connection.Open();
                    int id = connection.QueryFirstOrDefault<int>("INSERT INTO public.users (username, email, password, role," +
                        " balance) " +
                        "VALUES (@username, @email, @password, @role, @balance) RETURNING id", data);

                    logger.LogInformation($"User with {id} signed up");

                    var returnData = new
                    {
                        userName = data.username,
                        email = data.email,
                        role = data.role,
                        balance = data.balance,
                    };


                    return Ok(new { id = id, data = returnData  });
                }
            }

            catch (Exception ex)
            {
                logger.LogError("Error with sign up user");
                return BadRequest(new { error = ex.Message });
            }
        }
        
        /// <summary>
        /// Метод для входа зарегистрированного пользователя.
        /// </summary>
        /// <param name="requestData">Данные для входа пользователя.</param>
        /// <returns>Результат операции входа пользователя.</returns>
        [HttpPost("signIn")]
        public async Task<IActionResult> SignInUser(UserLogin requestData)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    if (!requestData.Email.Contains("@"))
                    {
                        return BadRequest(new { error = "Invalid email" });
                    }
                    
                    logger.LogInformation("123");
                    var userData = await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM public.users WHERE email = @email",
                        new { requestData.Email });
                    logger.LogInformation("1234");

                    if (userData == null)
                    {
                        return BadRequest(new { error = "User with this email not found" });
                    }

                    if (!PasswordHelper.VerifyPassword(userData.Password, requestData.Password))
                    {
                        return BadRequest(new { error = "Incorrect password" });
                    }

                    var token = _jwtProvider.GenerateToken(userData);

                    logger.LogInformation($"User with email {requestData.Email} signed in successfully");
                    return Ok(new { data = token });
                }
            }
            catch(Exception ex)
            {
                logger.LogError("Error with sign in user");
                return BadRequest(new { error = ex.Message });
            }
        }
        
        /// <summary>
        /// Метод для обновления профиля пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="updatedUser">Обновленные данные пользователя.</param>
        /// <returns>Результат операции обновления профиля пользователя.</returns>
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
                    connection.Execute("UPDATE public.users SET username = @username, email = @email,"
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
        
        /// <summary>
        /// Метод для удаления профиля пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Результат операции удаления профиля пользователя.</returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProfile (int id)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {

                    

                    connection.Open();
                    connection.Execute("DELETE FROM public.users WHERE Id = @id", new { id });

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


        [HttpGet("getUser")]
        public async Task<IActionResult> GetUser(string token)
        {

            
            try
            {
                var claims = _jwtProvider.DecodeToken(token);

                if (claims.Count == 0)
                {
                    return BadRequest(new { error = "Empty token claims" });
                }

                string email = claims[0].Value.ToString();


                await using var connection = new NpgsqlConnection(connectionString);
                {
                   
                    logger.LogInformation(connectionString);


                    var userData = await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM public.users WHERE email = @email",
                        new { email });

                    if (userData == null)
                    {
                        return BadRequest(new { error = "User with this email not found" });
                    }

                    logger.LogInformation($"User with email {email} was found successfully");
                    return Ok(new { data = userData });
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with get user data in user");
                return BadRequest(new { error = ex.Message });
            }

        }

     
    }
}
