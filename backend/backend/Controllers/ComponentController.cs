using System.Globalization;
using System.Reflection;
using System.Text;
using backend.Entities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ProductController
    {
        private readonly ILogger<ComponentController> _logger;
        private readonly string _connectionString;
        protected readonly string ComponentType;
        
        public ComponentController(ILogger<ComponentController> logger, string componentType) : base(logger)
        {
            ComponentType = componentType;
            _logger = logger;
            DotNetEnv.Env.Load();
            _connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        }
        
        /// <summary>
        /// Создает новый компонент и сохраняет его в базе данных.
        /// </summary>
        /// <param name="component">Компонент, который необходимо создать.</param>
        /// <param name="characteristics">Характеристики компонента, которые будут сохранены в базе данных.</param>
        /// <param name="databaseName">Название таблицы в базе данных, в которой будет храниться информация о компоненте.</param>
        /// <typeparam name="T">Тип компонента, производный от класса Component с типом IFormFile.</typeparam>
        /// <returns>Результат операции создания компонента в базе данных.</returns>
        protected async Task<IActionResult> CreateComponent<T>(T component, string[] characteristics, string databaseName) where T: Component<IFormFile>
        {
            try
            {
                string[] characteristicsBase = ["brand", "model", "country", "price", "description", "image", "amount", "power", "likes", "product_type"];
                string imagePath = BackupWriter.Write(component.Image);


                if (component.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }
                if (component.Amount < 0)
                {
                    return BadRequest(new {error = "Amount must not be less than 0"});
                }

                if(component.Power < 0 || component.Power > 10)
                {
                    return BadRequest(new { error = "Power must be between 0 and 10" });
                }

                component.Likes = 0;
                PropertyInfo[] properties = typeof(T).GetProperties();
                var requestData = new Dictionary<string, object>();
                foreach (var property in properties)
                {
                    if (property.Name != "Image")
                    {
                        requestData[property.Name] = property.GetValue(component);
                    }
                    else
                    {
                        requestData["Image"] = imagePath;
                    }
                }

                requestData["product_type"] = ComponentType;
                await using var connection = new NpgsqlConnection(_connectionString);
                connection.Open();
                requestData["product_id"] = connection.QueryFirstOrDefault<int>($"INSERT INTO public.products " +
                    $"({DatabaseRequestsHelper.TransformCharacteristicsToString(characteristicsBase)}) " +
                    $"VALUES ({DatabaseRequestsHelper.TransformCharacteristicsToString(characteristicsBase, "@")}) RETURNING product_id", requestData);
                connection.QueryFirstOrDefault<int>($"INSERT INTO public.{databaseName} " +
                                                    $"(product_id, {DatabaseRequestsHelper.TransformCharacteristicsToString(characteristics)}) " +
                                                    $"VALUES (@product_id, {DatabaseRequestsHelper.TransformCharacteristicsToString(DatabaseRequestsHelper.SnakeCasesToPascalCase(characteristics), "@")})", requestData);
                _logger.LogInformation($"Component data saved to database with id {requestData["product_id"]}");
                return Ok(new { id = requestData["product_id"], requestData });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Component data failed to save in database. Exception: {ex}");
                return BadRequest(new { error = ex.Message});
            }
        }

        protected async Task<IActionResult> GetAllComponents<T>(int limit, int offset, string viewName,
            string[] characteristics) where T: Component<string>
        {
            string[] characteristicsBase = ["product_id AS productId", "brand", "model", "country", "price", "description", "image", "amount", "power", "likes", "product_type AS productType"];
            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                {
                    connection.Open();
                    var components = 
                        connection.Query<T>($"SELECT {DatabaseRequestsHelper.TransformCharacteristicsToString(characteristicsBase.Concat(characteristics).ToArray())} " +
                                                            $"FROM public.{viewName} " +
                                                            $"LIMIT {limit} OFFSET {offset}");
                    return Ok(new { data = components });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Component data did not get from database. Exception: {ex}");
                return NotFound(new { error = ex.Message });
            }
        }
        
        /// <summary>
        /// Метод для получения компонента из базы данных.
        /// </summary>
        /// <param name="componentId">Идентификатор компонента, который необходимо получить.</param>
        /// <param name="viewName">Название представления в базе данных, из которого будет извлечен компонент.</param>
        /// <param name="characteristics">Характеристики компонента, которые также необходимо получить из базы данных.</param>
        /// <typeparam name="T">Тип компонента, производный от класса Component с типом IFormFile.</typeparam>
        /// <returns>Результат операции получения компонента из базы данных.</returns>
        protected async Task<IActionResult> GetComponent<T>(int componentId, string viewName, string[] characteristics) where T: Component<string>
        {
            string[] characteristicsBase = ["product_id AS productId", "brand", "model", "country", "price", "description", "image", "amount", "power", "likes", "product_type AS productType"];
            
            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                {
                    connection.Open();
                    
                    var component = connection.QueryFirstOrDefault<T>($"SELECT {DatabaseRequestsHelper.TransformCharacteristicsToString(characteristicsBase.Concat(characteristics).ToArray())} FROM public.{viewName} " +
                        $"WHERE product_id = {componentId}");
                    
                    if (component != null)
                    {
                        _logger.LogInformation($"Retrieved component with Id {componentId} from the database");
                        return Ok(component);
                    }
                    else
                    {
                        _logger.LogInformation($"Component with Id {componentId} not found in the database");
                        return NotFound(new {error = $"Not found component with {componentId}"});
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to retrieve component data from the database. \nException {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }
        
        /// <summary>
        /// Метод для обновления данных компонента в базе данных.
        /// </summary>
        /// <param name="component">Обновленный компонент.</param>
        /// <param name="isUpdated">Флаг, указывающий, были ли обновлены данные компонента.</param>
        /// <param name="databaseName">Название таблицы в базе данных, в которой хранятся данные компонента.</param>
        /// <param name="characteristics">Характеристики компонента, которые требуется обновить.</param>
        /// <typeparam name="T">Тип компонента, производный от класса Component с типом IFormFile.</typeparam>
        /// <returns>Результат операции обновления данных компонента в базе данных.</returns>
        protected async Task<IActionResult> UpdateComponent<T>(T component, bool isUpdated, string databaseName,
            string[] characteristics) where T: Component<IFormFile>
        {
            string[] characteristicsBase = ["brand", "model", "country", "price", "description", "image", "amount", "power", "likes", "product_type"];
            try
            {
                if (component.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }

                if (component.Amount < 0)
                {
                    return BadRequest(new { error = "Amount must not be less than 0" });
                }

                if (component.Power < 0 || component.Power > 10)
                {
                    return BadRequest(new { error = "Power must be between 0 and 10" });
                }

                if(component.Likes < 0)
                {
                    return BadRequest(new { error = "Likes must not be less than 0" });
                }

                await using var connection = new NpgsqlConnection(_connectionString);
                {
                    connection.Open();
                    string imagePath;
                    string oldImagePath = connection.QueryFirstOrDefault<string>($"SELECT image FROM public.products WHERE " +
                        $"product_id = {component.ProductId}");
                    if (isUpdated)
                    {
                        BackupWriter.Delete(oldImagePath);
                        imagePath = BackupWriter.Write(component.Image);
                    }
                    else
                    {
                        imagePath = oldImagePath;
                    }

                    PropertyInfo[] properties = typeof(T).GetProperties();
                    Dictionary<string, object> baseComponentCharacteristics = new Dictionary<string, object>();
                    Dictionary<string, object> componentCharacteristics = new Dictionary<string, object>();
                    foreach (var property in properties)
                    {
                        bool isContain = characteristicsBase.Any(characteristic => DatabaseRequestsHelper.SnakeCaseToPascalCase(characteristic) == property.Name);
                        if (isContain)
                        {
                            if (property.Name == "Image")
                            {
                                baseComponentCharacteristics[property.Name] = imagePath;
                            }
                            else
                            {
                                baseComponentCharacteristics[property.Name] = property.GetValue(component);      
                            }
                        }
                        else if(property.Name != "ProductId")
                        {
                            componentCharacteristics[property.Name] = property.GetValue(component);
                        }
                    }
                    Dictionary<string, object> response = new Dictionary<string, object>();
                    foreach (var elem in baseComponentCharacteristics)
                    {
                        response[elem.Key] = elem.Value;
                    }
                    foreach (var elem in componentCharacteristics)
                    {
                        response[elem.Key] = elem.Value;
                    }
                    connection.Execute($"UPDATE public.products SET {DatabaseRequestsHelper.TransformCharacteristicsToSetString(characteristicsBase)} " +
                                       $"WHERE product_id = {component.ProductId}", response);
                    connection.Execute($"UPDATE public.{databaseName} SET {DatabaseRequestsHelper.TransformCharacteristicsToSetString(characteristics)} " +
                                       $"WHERE product_id = {component.ProductId}", response);
                    _logger.LogInformation("Component data updated in the database");
                    
                    return Ok(new { id = component.ProductId, data = response });
                
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update component data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }
        
        /// <summary>
        /// Метод для удаления компонента из базы данных.
        /// </summary>
        /// <param name="id">Идентификатор компонента, который необходимо удалить.</param>
        /// <returns>Результат операции удаления компонента из базы данных.</returns>
        protected async Task<IActionResult> DeleteComponent(int id)
        {
            try
            {
                string filePath;
                await using var connection = new NpgsqlConnection(_connectionString);
                {
                    connection.Open();

                    filePath = connection.QueryFirstOrDefault<string>($"SELECT image FROM public.products WHERE product_id = {id}");
                    BackupWriter.Delete(filePath);

                    connection.Execute($"DELETE FROM public.products WHERE product_id = {id}");

                    _logger.LogInformation("Component data deleted from the database");

                    return Ok(new {id = id});
                }

            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to delete component data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }
        
        /// <summary>
        /// Метод для добавления нового фильтра в базу данных.
        /// </summary>
        /// <param name="newFilter">Новый фильтр, который необходимо добавить.</param>
        /// <returns>Результат операции добавления нового фильтра в базу данных.</returns>
        protected async Task<IActionResult> AddFilter(Filter newFilter)
        {
            string[] filterCharacteristics = ["filter_name", "component_type", "filter_value"];
            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                {
                    connection.Open();
                    int id = connection.QueryFirstOrDefault<int>(
                        $"INSERT INTO public.filters ({DatabaseRequestsHelper.TransformCharacteristicsToString(filterCharacteristics)}) " +
                        $"VALUES ({DatabaseRequestsHelper.TransformCharacteristicsToString(DatabaseRequestsHelper.SnakeCasesToPascalCase(filterCharacteristics), "@")})", newFilter);
                    newFilter.Id = id;
                    _logger.LogInformation("Filter add success");
                    return Ok(newFilter);
                }
            }
            catch (Exception err)
            {
                _logger.LogError($"Filter wasn't create: {err.Message}");
                return BadRequest(new { error = err });
            }
        }
        
        /// <summary>
        /// Метод для фильтрации компонентов в базе данных.
        /// </summary>
        /// <param name="viewName">Название представления в базе данных, из которого будут извлечены компоненты.</param>
        /// <param name="filters">Массив фильтров, по которым будет производиться фильтрация компонентов.</param>
        /// <param name="characteristics">Характеристики компонентов, которые также будут извлечены из базы данных.</param>
        /// <typeparam name="T">Тип компонента, производный от класса Component с типом IFormFile.</typeparam>
        /// <returns>Результат операции фильтрации компонентов в базе данных.</returns>
        protected async Task<IActionResult> FilterComponents<T>(string viewName, Filter[] filters, string[] characteristics) where T: Component<string>
        {
            string[] characteristicsBase =
            [
                "brand", "model", "country", "price", "description", "image", "amount", "power", "likes",
                "product_type AS productType"
            ];
            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                {
                    connection.Open();
                    var components =
                        connection.Query(
                            $"SELECT {DatabaseRequestsHelper.TransformCharacteristicsToString(characteristicsBase.Concat(characteristics).ToArray())} " +
                            $"FROM public.{viewName} WHERE {TransformFiltersToString(filters)}");
                    if (components != null)
                    {
                        return Ok(new { components });    
                    }

                    return NotFound(new {error = "Not found components"});
                }
            }
            catch (Exception err)
            {
                _logger.LogError($"Filter wasn't do: {err.Message}");
                return BadRequest(new { error = err });
            }
        }
        
        /// <summary>
        /// Метод для получения фильтров компонентов из базы данных.
        /// </summary>
        /// <typeparam name="T">Тип компонента, производный от класса Component с типом IFormFile.</typeparam>
        /// <returns>Результат операции получения фильтров компонентов из базы данных.</returns>
        protected async Task<IActionResult> GetComponentFilters<T>() where T: Component<string>
        {
            string[] filterCharacteristics = ["id", "filter_name AS filterName", "component_type AS componentType", "filter_value AS filterValue"];
            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                {
                    connection.Open();
                    var filters = connection.Query<Filter>($"SELECT {DatabaseRequestsHelper.TransformCharacteristicsToString(filterCharacteristics)} " +
                                                           $"FROM public.filters " +
                                                           $"WHERE component_type = '{ComponentType}'");
                    PropertyInfo[] properties = typeof(T).GetProperties();
                    Dictionary<string, List<string>> response = new Dictionary<string, List<string>>();
                    foreach (var property in properties)
                    {
                        foreach (var filter in filters)
                        {
                            if (filter.FilterName == char.ToLower(property.Name[0]) + property.Name.Substring(1))
                            {
                                if (!response.ContainsKey(property.Name))
                                {
                                    response.Add(property.Name, new List<string>());
                                }
                                response[property.Name].Add(filter.FilterValue);
                            }
                        }
                    }

                    Dictionary<string, List<string>> responseCamelCase = new Dictionary<string, List<string>>();
                    foreach (var kvp in response)
                    {
                        string newKey = char.ToLower(kvp.Key[0]) + kvp.Key.Substring(1);
                        responseCamelCase[newKey] = response[kvp.Key];
                    }

                    return Ok(responseCamelCase);
                }
            }
            catch (Exception err)
            {
                _logger.LogError($"Filter wasn't get: {err.Message}");
                return NotFound(new { error = err });
            }
        }
        
        /// <summary>
        /// Метод для преобразования массива фильтров в строку условия SQL запроса.
        /// </summary>
        /// <param name="filters">Массив фильтров, который требуется преобразовать.</param>
        /// <returns>Строка условия SQL запроса на основе фильтров.</returns>
        private string TransformFiltersToString(Filter[] filters)
        {
            string filtersString = "";
            for (int i = 0; i < filters.Length; i++)
            {
                if (i != 0)
                {
                    filtersString += $" OR {DatabaseRequestsHelper.PascalCaseToSnakeCase(filters[i].FilterName)} = '{filters[i].FilterValue}'";
                }
                else
                {
                    filtersString += $"{DatabaseRequestsHelper.PascalCaseToSnakeCase(filters[i].FilterName)} = '{filters[i].FilterValue}'";
                }
            }

            return filtersString;
        }
        
        
        /// <summary>
        /// Метод для поиска компонентов по ключевому слову в базе данных.
        /// </summary>
        /// <param name="keyword">Ключевое слово для поиска компонентов.</param>
        /// <param name="limit">Максимальное количество компонентов, которое требуется вернуть (по умолчанию 1).</param>
        /// <param name="offset">Смещение для запроса компонентов (по умолчанию 0).</param>
        /// <returns>Результат операции поиска компонентов в базе данных.</returns>
        protected async Task<IActionResult> SearchComponent(string keyword, int limit = 1, int offset = 0)
        {
            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                {
                    connection.Open();
                    _logger.LogInformation("Connection started");

                    var component = connection.Query<VideoCard<string>>(@"SELECT * FROM public.products " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT @Limit OFFSET @Offset", new { Keyword = "%" + keyword + "%", Limit = limit, Offset = offset });

                    return Ok(new { component });

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with search");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
