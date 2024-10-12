namespace MatrixProjectTests.Strategies
{
    using MatrixProject.Strategies;

    public class VerticalSearchStrategyTest
    {
        private readonly VerticalSearchStrategy _verticalSearchStrategy;

        public VerticalSearchStrategyTest()
        {
            // Initialize the strategy
            _verticalSearchStrategy = new VerticalSearchStrategy();
        }

        [Fact]
        public void SearchWord_ShouldReturnZero_WhenMatrixIsEmpty()
        {
            // Arrange
            var emptyMatrix = new List<string>();
            string wordToSearch = "test";

            // Act
            int result = _verticalSearchStrategy.SearchWord(emptyMatrix, wordToSearch);

            // Assert
            Assert.Equal(0, result); // Should return 0 since there are no rows in the matrix
        }

        [Fact]
        public void SearchWord_ShouldReturnZero_WhenWordIsNotFound()
        {
            // Arrange
            var matrix = new List<string> { "abcdef", "ghijkl", "mnopqr" };
            string wordToSearch = "xyz"; // This word does not exist in the matrix

            // Act
            int result = _verticalSearchStrategy.SearchWord(matrix, wordToSearch);

            // Assert
            Assert.Equal(0, result); // Should return 0 because the word is not found in the columns
        }

        [Fact]
        public void SearchWord_ShouldReturnCorrectCount_WhenWordIsFoundInColumns()
        {
            // Arrange
            var matrix = new List<string> { "abcf", 
                                            "defg", 
                                            "ghia", 
                                            "mnof" };
            string wordToSearch = "af";

            // Act
            int result = _verticalSearchStrategy.SearchWord(matrix, wordToSearch);

            // Assert
            Assert.Equal(1, result); // "af" is found once vertically
        }

        [Fact]
        public void SearchWord_ShouldReturnCorrectCount_WhenWordOccursMultipleTimesInColumns()
        {
            // Arrange
            var matrix = new List<string> { "aaab", "aaab", "aaab" };
            string wordToSearch = "aaa";

            // Act
            int result = _verticalSearchStrategy.SearchWord(matrix, wordToSearch);

            // Assert
            Assert.Equal(3, result); // "aaa" is found three times in the first three columns
        }

        [Fact]
        public void SearchWord_ShouldThrowArgumentNullException_WhenMatrixIsNull()
        {
            // Arrange
            string wordToSearch = "test";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                _verticalSearchStrategy.SearchWord(null, wordToSearch)); // Should throw an exception
        }

        [Fact]
        public void SearchWord_ShouldReturnZero_WhenWordIsEmpty()
        {
            // Arrange
            var matrix = new List<string> { "abcdef", "ghijkl", "mnopqr" };
            string wordToSearch = ""; // Empty word should not be found

            // Act
            int result = _verticalSearchStrategy.SearchWord(matrix, wordToSearch);

            // Assert
            Assert.Equal(0, result); // Empty word, should return 0
        }

        [Fact]
        public void GetColumnAsString_ShouldReturnCorrectColumn_WhenCalled()
        {
            // Arrange
            var matrix = new List<string> { "abcd", "efgh", "ijkl" }.ToArray();
            int columnIndex = 1; // Second column (index 1)

            // Act
            string result = _verticalSearchStrategy.GetType()
                .GetMethod("GetColumnAsString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.Invoke(_verticalSearchStrategy, new object[] { matrix, columnIndex }) as string;

            // Assert
            Assert.Equal("bfj", result); // The second column should return "bfj"
        }
    }
}
