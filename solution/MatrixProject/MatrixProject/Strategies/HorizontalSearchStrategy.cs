namespace MatrixProject.Strategies
{
    using MatrixProject.Services;

    /// <summary>
    /// Horizontal search strategy for finding words in a character matrix.
    /// This strategy uses the Boyer-Moore algorithm to search for the word in each row of the matrix.
    /// </summary>
    public class HorizontalSearchStrategy : IWordSearchStrategy
    {
        private readonly BoyerMooreSearcher _boyerMooreSearcher = new BoyerMooreSearcher();

        /// <summary>
        /// Searches for a word in the rows of the matrix using the horizontal search strategy.
        /// Applies the Boyer-Moore algorithm to optimize searches in each row.
        /// </summary>
        /// <param name="matrix">The character matrix represented as a set of strings, where each string is a row.</param>
        /// <param name="word">The word to search for in the matrix.</param>
        /// <returns>The total number of occurrences of the word in the rows of the matrix.</returns>
        public int SearchWord(IEnumerable<string> matrix, string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return 0;
            }

            // Sum the occurrences of the word in each row using Boyer-Moore
            return matrix.Sum(row => _boyerMooreSearcher.Search(row, word));
        }
    }
}
