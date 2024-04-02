using System.Globalization;
using System.Text;

namespace backend.Utils;

public static class DatabaseRequestsHelper
{
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
        
        public static string SnakeCaseToPascalCase(string snakeCase)
        {
            string[] words = snakeCase.Split('_');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words[i]);
            }
            return string.Concat(words);
        }

        public static string[] SnakeCasesToPascalCase(string[] snakeCases)
        {
            string[] pascalCases = new string[snakeCases.Length];
            for (int i = 0; i < snakeCases.Length; i++)
            {
                pascalCases[i] = SnakeCaseToPascalCase(snakeCases[i]);
            }

            return pascalCases;
        }

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