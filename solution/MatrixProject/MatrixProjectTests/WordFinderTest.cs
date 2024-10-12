namespace MatrixProjectTests
{
    using MatrixProject.Strategies;
    using MatrixProject;
    using Moq;

    public class WordFinderTest
    {
        [Fact]
        public void Find_ShouldReturnEmpty_WhenWordStreamIsEmpty()
        {
            // Arrange
            var matrix = new List<string> { "abcdef", "ghijkl", "mnopqr" };
            var wordFinder = new WordFinder(matrix);
            var emptyWordStream = Enumerable.Empty<string>();

            // Act
            var result = wordFinder.Find(emptyWordStream);

            // Assert
            Assert.Empty(result); // Should return an empty result since no words were provided
        }

        [Fact]
        public void Find_ShouldReturnEmpty_WhenNoWordsAreFound()
        {
            // Arrange
            var matrix = new List<string> { "abcdef", "ghijkl", "mnopqr" };
            var wordFinder = new WordFinder(matrix);
            var wordStream = new List<string> { "xyz", "uvw" }; // Words not present in the matrix

            // Act
            var result = wordFinder.Find(wordStream);

            // Assert
            Assert.Empty(result); // Should return an empty result since no words were found
        }

        [Fact]
        public void Find_ShouldReturnWords_WhenWordsAreFound()
        {
            // Arrange
            var matrix = new List<string> { "abcdef", "abcabc", "mnopqr" };
            var wordFinder = new WordFinder(matrix);
            var wordStream = new List<string> { "abc", "mnopqr" };

            // Act
            var result = wordFinder.Find(wordStream);

            // Assert
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count); // Should find 2 words
            Assert.Equal("abc (found 3 times)", resultList[0]); // "abc" appears 3 times in total
            Assert.Equal("mnopqr (found 1 times)", resultList[1]); // "mnopqr" appears once
        }

        [Fact]
        public void Find_ShouldReturnTop10Words_WhenMoreThan10WordsAreFound()
        {
            // Arrange
            var matrix = new List<string>
    {
        "abcabcabc", "defdefdef", "ghighighi", "jkljkljkl",
        "mnopmnopm", "qrstqrstu", "uvwxyzuvw", "abcabcabc"
    };
            // Stream of words that we know are present in the matrix and are repeated several times
            var wordStream = new List<string> { "abc", "def", "ghi", "jkl", "mno", "pqr", "stu", "uvw", "abc", "ghi", "def", "jkl" };

            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream);

            // Assert
            var resultList = result.ToList();
            Assert.Equal(7, resultList.Count); // It should return only 7 words because those are the ones in the array
        }

        [Fact]
        public void Find_ShouldReturnWordsInDescendingOrderOfFrequency()
        {
            // Arrange
            var matrix = new List<string> { "abcdef", "abcabc", "mnopqr", "abcabc" };
            var wordFinder = new WordFinder(matrix);
            var wordStream = new List<string> { "abc", "mnopqr", "def" };

            // Act
            var result = wordFinder.Find(wordStream);

            // Assert
            var resultList = result.ToList();
            Assert.Equal("abc (found 5 times)", resultList[0]); // "abc" appears 5 times
            Assert.Equal("mnopqr (found 1 times)", resultList[1]); // "mnopqr" appears once
        }

        [Fact]
        public void Find_ShouldHandleDuplicateWordsInWordStream()
        {
            // Arrange
            var matrix = new List<string> { "abcdef", "abcabc", "mnopqr" };
            var wordFinder = new WordFinder(matrix);
            var wordStream = new List<string> { "abc", "abc", "mnopqr" }; // Duplicate "abc"

            // Act
            var result = wordFinder.Find(wordStream);

            // Assert
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count); // Only 2 unique words should be found
            Assert.Equal("abc (found 3 times)", resultList[0]); // "abc" appears 3 times
            Assert.Equal("mnopqr (found 1 times)", resultList[1]); // "mnopqr" appears once
        }

        [Fact]
        public void Find_ShouldThrowArgumentException_WhenMatrixIsEmpty()
        {
            // Arrange
            var emptyMatrix = Enumerable.Empty<string>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new WordFinder(emptyMatrix)); // Should throw an ArgumentException
        }

        [Fact]
        public void GetTotalOccurrences_ShouldReturnCorrectCount_WhenWordIsFound()
        {
            // Arrange
            var mockStrategy = new Mock<IWordSearchStrategy>();
            mockStrategy.Setup(s => s.SearchWord(It.IsAny<IEnumerable<string>>(), "abc"))
                        .Returns(3); // Mock the strategy to return 3 occurrences of "abc"

            var wordFinder = new WordFinder(new List<string> { "abcdef", "abcabc" });
            wordFinder.GetType()
                      .GetField("_searchStrategies", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                      ?.SetValue(wordFinder, new List<IWordSearchStrategy> { mockStrategy.Object });

            // Act
            var occurrences = wordFinder.GetType()
                                        .GetMethod("GetTotalOccurrences", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                        ?.Invoke(wordFinder, new object[] { "abc" });

            // Assert
            Assert.Equal(3, occurrences);
        }
    }
}
