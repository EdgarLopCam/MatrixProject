namespace MatrixProject
{
    using MatrixProject.Factories;
    using MatrixProject.Strategies;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class responsible for finding words in a matrix using different search strategies.
    /// </summary>
    public class WordFinder
    {
        private readonly IEnumerable<string> _matrix;
        private readonly List<IWordSearchStrategy> _searchStrategies;

        /// <summary>
        /// Initializes a new instance of the <see cref="WordFinder"/> class with a given matrix.
        /// </summary>
        /// <param name="matrix">The character matrix where the words will be searched for.</param>
        public WordFinder(IEnumerable<string> matrix)
        {
            _matrix = matrix;
            // Check if matrix is empty
            if (_matrix == null || !_matrix.Any())
            {
                throw new ArgumentException("The matrix cannot be null or empty.");
            }
            _searchStrategies = SearchStrategyFactory.CreateDefaultStrategies().ToList();
        }

        /// <summary>
        /// Finds the words from the input stream that appear in the matrix, sorted by frequency.
        /// Returns the 10 most repeated words.
        /// </summary>
        /// <param name="wordStream">The stream of words to be searched for in the matrix.</param>
        /// <returns>
        /// An enumeration of strings describing the found words along with the number of times they appeared, 
        /// in the format "word (found X times)". If no words are found, returns an empty enumeration.
        /// </returns>
        public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            // We use a HashSet to ensure that the words are unique
            var uniqueWords = new HashSet<string>(wordStream);

            // Count the occurrences of each word using LINQ for clarity
            var wordCount = uniqueWords
                .Select(word => new
                {
                    Word = word,
                    Occurrences = GetTotalOccurrences(word)
                })
                .Where(result => result.Occurrences > 0) // Only include words that appeared at least once
                .ToDictionary(result => result.Word, result => result.Occurrences);

            if (!wordCount.Any())
            {
                return Enumerable.Empty<string>();
            }

            // Sort and take the 10 most repeated words
            return wordCount
                .OrderByDescending(w => w.Value)
                .Take(10)
                .Select(w => $"{w.Key} (found {w.Value} times)");
        }

        /// <summary>
        /// Calculates the total number of occurrences of a word in the matrix using all available search strategies.
        /// </summary>
        /// <param name="word">The word to search for in the matrix.</param>
        /// <returns>The total number of times the word appears in the matrix.</returns>
        private int GetTotalOccurrences(string word)
        {
            return _searchStrategies.Sum(strategy => strategy.SearchWord(_matrix, word));
        }
    }
}
