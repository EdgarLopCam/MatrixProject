namespace MatrixProject.Strategies
{
    using MatrixProject.Services;

    /// <summary>
    /// Vertical search strategy that implements the Boyer-Moore pattern to search for words in the columns of a matrix.
    /// </summary>
    public class VerticalSearchStrategy : IWordSearchStrategy
    {
        private readonly BoyerMooreSearcher _boyerMooreSearcher = new BoyerMooreSearcher();

        /// <summary>
        /// Performs the search for a word in the matrix vertically, using the Boyer-Moore algorithm.
        /// The search is conducted in each column of the matrix, and all occurrences found are summed.
        /// </summary>
        /// <param name="matrix">The matrix in which the word will be searched for.</param>
        /// <param name="word">The word to search for in the matrix.</param>
        /// <returns>The total number of times the word appears in the columns of the matrix.</returns>
        public int SearchWord(IEnumerable<string> matrix, string word)
        {
            var matrixArray = matrix.ToArray();

            // Check if the matrix is empty or the word is null/empty
            if (matrixArray.Length == 0 || string.IsNullOrEmpty(word))
            {
                return 0;
            }

            // Check if the matrix is uniform in terms of column count
            int columnCount = matrixArray[0].Length;
            if (matrixArray.Any(row => row.Length != columnCount))
            {
                throw new InvalidOperationException("All rows in the matrix must have the same number of columns.");
            }

            return Enumerable.Range(0, columnCount)
                             .Select(col => _boyerMooreSearcher.Search(GetColumnAsString(matrixArray, col), word))
                             .Sum();
        }

        /// <summary>
        /// Converts a column of the matrix into a string, where each character represents a value in a row.
        /// </summary>
        /// <param name="matrix">The matrix from which the column will be extracted.</param>
        /// <param name="col">The index of the column to convert into a string.</param>
        /// <returns>A string containing the characters from the specified column.</returns>
        private string GetColumnAsString(string[] matrix, int col)
        {
            // Ensure that the column index is within the bounds of the matrix rows
            if (matrix.Any(row => col >= row.Length))
            {
                throw new IndexOutOfRangeException("Column index is out of bounds.");
            }

            return new string(matrix.Select(row => row[col]).ToArray());
        }
    }
}
