namespace MatrixProjectTests.Configurations
{
    using MatrixProject.Configurations;

    public class ConfigurationLoaderTest
    {
        [Fact]
        public void LoadMatrixFromFile_ShouldLoadMatrixCorrectly_WhenFileExistsAndValid()
        {
            // Arrange
            string validJsonContent = "{\"Matrix\": [\"abcdef\", \"ghijkl\", \"mnopqr\"]}";
            string tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, validJsonContent);

            // Act
            var result = ConfigurationLoader.LoadMatrixFromFile(tempFile);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Equal("abcdef", result.ElementAt(0));
            Assert.Equal("ghijkl", result.ElementAt(1));
            Assert.Equal("mnopqr", result.ElementAt(2));

            // Cleanup
            File.Delete(tempFile);
        }

        [Fact]
        public void LoadMatrixFromFile_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
        {
            // Arrange
            string invalidFilePath = "non_existent_file.json";

            // Act & Assert
            var exception = Assert.Throws<FileNotFoundException>(() =>
                ConfigurationLoader.LoadMatrixFromFile(invalidFilePath));

            Assert.Equal($"Configuration file not found at path: {invalidFilePath}", exception.Message);
        }

        [Fact]
        public void LoadMatrixFromFile_ShouldThrowInvalidDataException_WhenMatrixIsInvalid()
        {
            // Arrange
            string invalidJsonContent = "{\"Matrix\": [\"abcdef\", \"ghijklm\"]}"; // Rows have different lengths
            string tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, invalidJsonContent);

            // Act & Assert
            var exception = Assert.Throws<InvalidDataException>(() =>
                ConfigurationLoader.LoadMatrixFromFile(tempFile));

            Assert.Equal("All rows in the matrix must have the same number of characters.", exception.Message);

            // Cleanup
            File.Delete(tempFile);
        }

        [Fact]
        public void LoadMatrixFromFile_ShouldThrowInvalidDataException_WhenMatrixExceedsMaxSize()
        {
            // Arrange
            var largeMatrix = new List<string>();
            for (int i = 0; i < 65; i++) // More than 64 rows
            {
                largeMatrix.Add(new string('a', 64));
            }

            string largeJsonContent = "{\"Matrix\": " + Newtonsoft.Json.JsonConvert.SerializeObject(largeMatrix) + "}";
            string tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, largeJsonContent);

            // Act & Assert
            var exception = Assert.Throws<InvalidDataException>(() =>
                ConfigurationLoader.LoadMatrixFromFile(tempFile));

            Assert.Equal("The matrix size exceeds the maximum allowed size of 64x64.", exception.Message);

            // Cleanup
            File.Delete(tempFile);
        }

        [Fact]
        public void LoadMatrixFromFile_ShouldThrowInvalidDataException_WhenMatrixIsEmpty()
        {
            // Arrange
            string emptyJsonContent = "{\"Matrix\": []}"; // No rows
            string tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, emptyJsonContent);

            // Act & Assert
            var exception = Assert.Throws<InvalidDataException>(() =>
                ConfigurationLoader.LoadMatrixFromFile(tempFile));

            Assert.Equal("The configuration file is not in the expected format or is empty.", exception.Message);

            // Cleanup
            File.Delete(tempFile);
        }

        [Fact]
        public void ValidateFileExists_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
        {
            // Arrange
            string invalidFilePath = "invalid_file_path.json";

            // Act & Assert
            var exception = Assert.Throws<FileNotFoundException>(() =>
                ConfigurationLoader.LoadMatrixFromFile(invalidFilePath));

            Assert.Equal($"Configuration file not found at path: {invalidFilePath}", exception.Message);
        }
    }
}
