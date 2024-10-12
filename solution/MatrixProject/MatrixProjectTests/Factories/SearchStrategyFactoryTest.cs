namespace MatrixProjectTests.Factories
{
    using MatrixProject.Factories;
    using MatrixProject.Strategies;

    public class SearchStrategyFactoryTest
    {
        [Fact]
        public void CreateDefaultStrategies_ShouldReturnNonEmptyList()
        {
            // Act
            var strategies = SearchStrategyFactory.CreateDefaultStrategies();

            // Assert
            Assert.NotNull(strategies);
            Assert.NotEmpty(strategies);
        }

        [Fact]
        public void CreateDefaultStrategies_ShouldContainHorizontalSearchStrategy()
        {
            // Act
            var strategies = SearchStrategyFactory.CreateDefaultStrategies();

            // Assert
            Assert.Contains(strategies, strategy => strategy is HorizontalSearchStrategy);
        }

        [Fact]
        public void CreateDefaultStrategies_ShouldContainVerticalSearchStrategy()
        {
            // Act
            var strategies = SearchStrategyFactory.CreateDefaultStrategies();

            // Assert
            Assert.Contains(strategies, strategy => strategy is VerticalSearchStrategy);
        }

        [Fact]
        public void CreateDefaultStrategies_ShouldReturnExactlyTwoStrategies()
        {
            // Act
            var strategies = SearchStrategyFactory.CreateDefaultStrategies();

            // Assert
            Assert.Equal(2, strategies.Count());
        }

        [Fact]
        public void CreateDefaultStrategies_ShouldReturnInstancesOfIWordSearchStrategy()
        {
            // Act
            var strategies = SearchStrategyFactory.CreateDefaultStrategies();

            // Assert
            Assert.All(strategies, strategy => Assert.IsAssignableFrom<IWordSearchStrategy>(strategy));
        }
    }
}
