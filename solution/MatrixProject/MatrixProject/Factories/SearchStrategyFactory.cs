namespace MatrixProject.Factories
{
    using MatrixProject.Strategies;

    /// <summary>
    /// Static factory responsible for creating search strategies to find words in a matrix.
    /// </summary>
    public static class SearchStrategyFactory
    {
        /// <summary>
        /// Creates the default search strategies used to find words in a matrix.
        /// The default strategies include horizontal and vertical search.
        /// </summary>
        /// <returns>
        /// An enumerator of <see cref="IWordSearchStrategy"/> containing the default strategies:
        /// <see cref="HorizontalSearchStrategy"/> and <see cref="VerticalSearchStrategy"/>.
        /// </returns>
        public static IEnumerable<IWordSearchStrategy> CreateDefaultStrategies()
        {
            return new List<IWordSearchStrategy>
            {
                new HorizontalSearchStrategy(),
                new VerticalSearchStrategy(),
            };
        }
    }
}
