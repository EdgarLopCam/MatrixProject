namespace MatrixProject.Services
{
    /// <summary>
    /// Class that implements the Boyer-Moore search algorithm to find all occurrences of a pattern in a text.
    /// Uses imperative logic to optimize the search process.
    /// </summary>
    public class BoyerMooreSearcher
    {
        /// <summary>
        /// Searches for occurrences using the Boyer-Moore algorithm.
        /// </summary>
        /// <param name="text">The text in which the pattern will be searched for.</param>
        /// <param name="pattern">The pattern to search for in the text.</param>
        /// <returns>The total number of occurrences of the pattern in the text.</returns>
        public int Search(string text, string pattern)
        {
            int[] badCharTable = BuildBadCharTable(pattern);
            int textLength = text.Length;
            int patternLength = pattern.Length;
            int totalOccurrences = 0;
            int shift = 0;

            while (shift <= textLength - patternLength)
            {
                int j = patternLength - 1;

                // Compare the pattern from right to left
                while (j >= 0 && pattern[j] == text[shift + j])
                {
                    j--;
                }

                // If a match is found
                if (j < 0)
                {
                    totalOccurrences++;
                    shift += (shift + patternLength < textLength) ? patternLength - badCharTable[text[shift + patternLength]] : 1;
                }
                else
                {
                    // Jump based on the "bad character" table
                    shift += Math.Max(1, j - badCharTable[text[shift + j]]);
                }
            }

            return totalOccurrences;
        }

        /// <summary>
        /// Builds the "bad character" table used in the Boyer-Moore algorithm.
        /// </summary>
        /// <param name="pattern">The pattern for which the "bad character" table will be built.</param>
        /// <returns>An integer array representing the "bad character" table.</returns>
        private int[] BuildBadCharTable(string pattern)
        {
            int[] table = new int[256]; // Assume 256 ASCII characters
            for (int i = 0; i < 256; i++)
            {
                table[i] = -1; // Initialize with -1
            }

            // Fill the table with the positions of the characters in the pattern
            for (int i = 0; i < pattern.Length - 1; i++)
            {
                table[pattern[i]] = i;
            }

            return table;
        }
    }
}
