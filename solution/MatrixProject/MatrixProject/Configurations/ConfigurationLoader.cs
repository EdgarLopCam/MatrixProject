namespace MatrixProject.Configurations
{
    using System.Text.Json;

    /// <summary>
    /// Class responsible for loading the matrix configuration from a file and validating its format.
    /// </summary>
    public class ConfigurationLoader
    {
        // Constants for the maximum matrix size
        private const int MaxMatrixSize = 64;
        private const int MaxRowLength = 64;

        /// <summary>
        /// Loads the matrix from a JSON file and validates its format.
        /// </summary>
        /// <param name="filePath">The path to the JSON file containing the matrix configuration.</param>
        /// <returns>An enumeration of strings representing the loaded matrix.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the configuration file is not found.</exception>
        /// <exception cref="InvalidDataException">Thrown if the matrix format is invalid or exceeds allowed limits.</exception>
        public static IEnumerable<string> LoadMatrixFromFile(string filePath)
        {
            ValidateFileExists(filePath);

            var jsonContent = File.ReadAllText(filePath);
            var config = DeserializeConfig(jsonContent);

            ValidateMatrixFormat(config.Matrix);

            // Print the lengths of each row for debugging purposes
            foreach (var row in config.Matrix)
            {
                Console.WriteLine($"Row length: {row.Length} - Content: '{row}'");
            }

            return config.Matrix;
        }

        /// <summary>
        /// Validates if the configuration file exists at the specified path.
        /// </summary>
        /// <param name="filePath">The path to the JSON file.</param>
        /// <exception cref="FileNotFoundException">Thrown if the configuration file is not found.</exception>
        private static void ValidateFileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Configuration file not found at path: {filePath}");
            }
        }

        /// <summary>
        /// Deserializes the JSON content into a <see cref="MatrixConfig"/> object.
        /// </summary>
        /// <param name="jsonContent">The JSON content representing the matrix configuration.</param>
        /// <returns>A <see cref="MatrixConfig"/> object containing the matrix configuration.</returns>
        /// <exception cref="InvalidDataException">Thrown if the JSON file is empty or in an incorrect format.</exception>
        private static MatrixConfig DeserializeConfig(string jsonContent)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignore case in property names
            };

            var config = JsonSerializer.Deserialize<MatrixConfig>(jsonContent, options);

            if (config == null || config.Matrix == null || !config.Matrix.Any())
            {
                throw new InvalidDataException("The configuration file is not in the expected format or is empty.");
            }

            return config;
        }

        /// <summary>
        /// Validates that the matrix format is correct in terms of size and uniform row lengths.
        /// </summary>
        /// <param name="matrix">The matrix to validate.</param>
        /// <exception cref="InvalidDataException">
        /// Thrown if the matrix has more rows or columns than allowed, or if the rows are not of equal length.
        /// </exception>
        private static void ValidateMatrixFormat(IEnumerable<string> matrix)
        {
            _ = IsValidMatrixSize(matrix) && HasUniformRowLength(matrix)
                ? true
                : throw new InvalidDataException(
                    IsValidMatrixSize(matrix)
                        ? "All rows in the matrix must have the same number of characters."
                        : $"The matrix size exceeds the maximum allowed size of {MaxMatrixSize}x{MaxRowLength}."
                );
        }

        /// <summary>
        /// Verifies whether the matrix size is valid according to defined limits.
        /// </summary>
        /// <param name="matrix">The matrix to validate.</param>
        /// <returns><c>true</c> if the matrix size is valid; otherwise, <c>false</c>.</returns>
        private static bool IsValidMatrixSize(IEnumerable<string> matrix)
        {
            return matrix.Count() <= MaxMatrixSize && matrix.All(row => row.Length <= MaxRowLength);
        }

        /// <summary>
        /// Verifies whether all rows in the matrix have the same length.
        /// </summary>
        /// <param name="matrix">The matrix to validate.</param>
        /// <returns><c>true</c> if all rows have the same length; otherwise, <c>false</c>.</returns>
        private static bool HasUniformRowLength(IEnumerable<string> matrix)
        {
            return matrix.Select(row => row.Length).Distinct().Count() == 1;
            // Count() == 1: Verifies if there is only one unique length. If so, it means all rows have the same length.
        }
    }
}