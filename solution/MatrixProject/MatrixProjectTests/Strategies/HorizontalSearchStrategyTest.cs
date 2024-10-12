namespace MatrixProjectTests.Strategies
{
    using MatrixProject.Strategies;

    public class HorizontalSearchStrategyTest
    {
        private readonly HorizontalSearchStrategy _horizontalSearchStrategy;

        public HorizontalSearchStrategyTest()
        {
            // Initialize the strategy
            _horizontalSearchStrategy = new HorizontalSearchStrategy();
        }

        [Fact]
        public void SearchWord_ShouldReturnZero_WhenMatrixIsEmpty()
        {
            // Arrange
            var emptyMatrix = new List<string>();
            string wordToSearch = "test";

            // Act
            int result = _horizontalSearchStrategy.SearchWord(emptyMatrix, wordToSearch);

            // Assert
            Assert.Equal(0, result); // Should return 0 because there are no rows in the matrix
        }

        [Fact]
        public void SearchWord_ShouldReturnZero_WhenWordIsNotFound()
        {
            // Arrange
            var matrix = new List<string> { "abcdef", "ghijkl", "mnopqr" };
            string wordToSearch = "xyz"; // This word does not exist in the matrix

            // Act
            int result = _horizontalSearchStrategy.SearchWord(matrix, wordToSearch);

            // Assert
            Assert.Equal(0, result); // Should return 0 because the word is not in the matrix
        }

        [Fact]
        public void SearchWord_ShouldReturnCorrectCount_WhenWordIsFound()
        {
            // Arrange
            var matrix = new List<string> { "abcdef", "abcabc", "mnopqr" };
            string wordToSearch = "abc";

            // Act
            int result = _horizontalSearchStrategy.SearchWord(matrix, wordToSearch);

            // Assert
            Assert.Equal(3, result); // The word "abc" appears 3 times in total
        }

        [Fact]
        public void SearchWord_ShouldReturnCorrectCount_WhenWordOccursMultipleTimesInARow()
        {
            // Arrange
            var matrix = new List<string> { "abcabcabc", "defdefdef" };
            string wordToSearch = "abc";

            // Act
            int result = _horizontalSearchStrategy.SearchWord(matrix, wordToSearch);

            // Assert
            Assert.Equal(3, result); // The word "abc" appears 3 times in the first row
        }

        [Fact]
        public void SearchWord_ShouldThrowArgumentNullException_WhenMatrixIsNull()
        {
            // Arrange
            string wordToSearch = "test";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                _horizontalSearchStrategy.SearchWord(null, wordToSearch)); // Expect an exception to be thrown
        }

        [Fact]
        public void SearchWord_ShouldReturnZero_WhenWordIsEmpty()
        {
            // Arrange
            var matrix = new List<string> { "abcdef", "ghijkl", "mnopqr" };
            string wordToSearch = ""; // An empty word should not be found

            // Act
            int result = _horizontalSearchStrategy.SearchWord(matrix, wordToSearch);

            // Assert
            Assert.Equal(0, result); // Empty word, should return 0
        }
    }
}
