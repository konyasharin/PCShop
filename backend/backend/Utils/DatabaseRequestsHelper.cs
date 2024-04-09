using System.Globalization;
using System.Text;

namespace backend.Utils;

public static class DatabaseRequestsHelper
{       
        /// <summary>
        /// Преобразует массив характеристик в строку, разделенную запятыми, с возможностью добавления префикса к каждой характеристике.
        /// </summary>
        /// <param name="characteristics">Массив характеристик для преобразования в строку.</param>
        /// <param name="prev">Префикс, который будет добавлен к каждой характеристике (по умолчанию - пустая строка).</param>
        /// <returns>Строка, содержащая перечисление характеристик, разделенных запятыми, с добавленным префиксом.</returns>
        public static string TransformCharacteristicsToString(string[] characteristics, string prev = "")
        {
            string stringOfCharacteristics = "";
            for (int i = 0; i < characteristics.Length; i++)
            {
                if (i != 0)
                {
                    stringOfCharacteristics += $", {prev + characteristics[i]}";
                }
                else
                {
                    stringOfCharacteristics += prev + characteristics[i];
                }
            }

            return stringOfCharacteristics;
        }
        
        
        /// <summary>
        /// Преобразует массив характеристик в строку для SET выражения в SQL, где каждая характеристика представлена в формате "характеристика = @Параметр".
        /// </summary>
        /// <param name="characteristics">Массив характеристик для преобразования в строку SET выражения.</param>
        /// <returns>Строка, содержащая SET выражение для SQL запроса.</returns>
        public static string TransformCharacteristicsToSetString(string[] characteristics)
        {
            string stringOfValues = "";
            for (int i = 0; i < characteristics.Length; i++)
            {
                if (i != 0)
                {
                    stringOfValues += $", {characteristics[i]} = @{SnakeCaseToPascalCase(characteristics[i])}";   
                }
                else
                {
                    stringOfValues += $"{characteristics[i]} = @{SnakeCaseToPascalCase(characteristics[i])}"; 
                }
            }

            return stringOfValues;
        }
        
        /// <summary>
        /// Преобразует строку в формате "snake_case" в формат "PascalCase".
        /// </summary>
        /// <param name="snakeCase">Строка в формате "snake_case".</param>
        /// <returns>Строка в формате "PascalCase".</returns>
        public static string SnakeCaseToPascalCase(string snakeCase)
        {
            string[] words = snakeCase.Split('_');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words[i]);
            }
            return string.Concat(words);
        }
        
        
        /// <summary>
        /// Преобразует массив строк из формата "snake_case" в формат "PascalCase".
        /// </summary>
        /// <param name="snakeCases">Массив строк в формате "snake_case".</param>
        /// <returns>Массив строк в формате "PascalCase".</returns>
        public static string[] SnakeCasesToPascalCase(string[] snakeCases)
        {
            string[] pascalCases = new string[snakeCases.Length];
            for (int i = 0; i < snakeCases.Length; i++)
            {
                pascalCases[i] = SnakeCaseToPascalCase(snakeCases[i]);
            }

            return pascalCases;
        }
        
        
        /// <summary>
        /// Преобразует строку из формата "PascalCase" в формат "snake_case".
        /// </summary>
        /// <param name="input">Входная строка в формате "PascalCase".</param>
        /// <returns>Строка в формате "snake_case".</returns>
        public static string PascalCaseToSnakeCase(string input)
        {
            StringBuilder result = new StringBuilder();
            bool isFirst = true;

            foreach (char c in input)
            {
                if (char.IsUpper(c))
                {
                    if (!isFirst)
                    {
                        result.Append('_');
                    }
                    result.Append(char.ToLower(c));
                }
                else
                {
                    result.Append(c);
                }
                isFirst = false;
            }
            return result.ToString();
        }
}