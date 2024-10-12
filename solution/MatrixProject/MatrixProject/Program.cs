using MatrixProject.Configurations;
using MatrixProject;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            string configPath = GetConfigPath(); // Get the configuration file path
            var matrix = LoadMatrixFromConfig(configPath); // Load the matrix from the configuration file

            var wordStream = GetUserInput(); // Get the user input

            var wordFinder = new WordFinder(matrix);
            var foundWords = wordFinder.Find(wordStream);

            DisplayResults(foundWords); // Display results
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Gets the path of the JSON configuration file.
    /// Uses the project's base path and adds "config.json".
    /// </summary>
    /// <returns>The full path of the configuration file.</returns>
    static string GetConfigPath()
    {
        string basePath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        return Path.Combine(basePath, "config.json");
    }

    /// <summary>
    /// Loads the character matrix from the specified configuration file.
    /// </summary>
    /// <param name="configPath">The path to the JSON configuration file.</param>
    /// <returns>An enumeration of strings representing the matrix loaded from the configuration file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the configuration file does not exist at the specified path.</exception>
    static IEnumerable<string> LoadMatrixFromConfig(string configPath)
    {
        if (!File.Exists(configPath))
        {
            throw new FileNotFoundException($"The configuration file was not found at path: {configPath}");
        }

        return ConfigurationLoader.LoadMatrixFromFile(configPath);
    }

    /// <summary>
    /// Gets the user input from the console.
    /// Prompts the user to enter words separated by commas.
    /// </summary>
    /// <returns>An enumeration of words entered by the user. If no input is provided, returns an empty enumeration.</returns>
    static IEnumerable<string> GetUserInput()
    {
        Console.WriteLine("Enter the words to search (comma-separated):");
        var input = Console.ReadLine();
        return input?.Split(',').Select(word => word.Trim()) ?? Enumerable.Empty<string>();
    }

    /// <summary>
    /// Displays the results of the found words in the matrix on the console.
    /// </summary>
    /// <param name="foundWords">An enumeration of words found in the matrix.</param>
    static void DisplayResults(IEnumerable<string> foundWords)
    {
        Console.WriteLine("Top 10 most repeated words found in the matrix:");
        foreach (var word in foundWords)
        {
            Console.WriteLine(word);
        }
    }
}
