namespace MatrixProject.Strategies
{
    /// <summary>
    /// Interface that defines the strategy for searching words in a character matrix.
    /// Each implementation of this interface must provide a mechanism to search for a word
    /// and return the number of times the word was found.
    /// </summary>
    public interface IWordSearchStrategy
    {
        /// <summary>
        /// Searches for a word in the provided matrix.
        /// The method should return the number of times the word was found in the matrix.
        /// </summary>
        /// <param name="matrix">The character matrix where the search will be performed, represented as a set of rows.</param>
        /// <param name="word">The word to search for in the matrix.</param>
        /// <returns>The total number of times the word was found in the matrix.</returns>
        int SearchWord(IEnumerable<string> matrix, string word);
    }
}