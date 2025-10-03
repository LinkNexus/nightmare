using System.Text.Json;

namespace Nightmare;

public class CollectionLoader
{

  static CollectionLoader? instance = null;

  public const string CONFIG_FILE = "nightmare.json";
  const int SEARCH_DEPTH = 5;

  string? configPath = null;
  Collection? config = null;

  private CollectionLoader() { }

  public static CollectionLoader Instance
  {
    get
    {
      instance ??= new CollectionLoader();
      return instance;
    }
  }

  static string FindConfigFile(string cwd, int currentDepth = 0)
  {
    if (currentDepth > SEARCH_DEPTH)
      throw new CollectionFileNotFoundException();

    var path = Path.Combine(cwd, CONFIG_FILE);

    if (File.Exists(path)) return path;

    currentDepth++;

    var parentDir = Directory.GetParent(cwd);

    if (parentDir is not null)
      return FindConfigFile(parentDir.FullName, currentDepth);
    else
      throw new CollectionFileNotFoundException();
  }

  public string GetConfigPath(string? cwd = null)
  {
    if (configPath is null)
    {
      cwd ??= Directory.GetCurrentDirectory();
      configPath = FindConfigFile(cwd);
    }

    return configPath;
  }

  static string CreateDefaultCollectionFile(string cwd)
  {
    var path = Path.Combine(cwd, CONFIG_FILE);
    File.WriteAllText(
        path,
  @"
{
  ""name"": ""Nightmare Collection"",
  ""profiles"": {
    ""default"": {
      ""data"": {
        ""host"": ""https://test.com""
      }
    }
  }
}");

    return path;
  }

  public Collection GetConfig(string? cwd = null)
  {
    if (config is null)
    {
      string? path;

      try
      {
        path = GetConfigPath(cwd);
      }
      catch (CollectionFileNotFoundException exception)
      {
        Console.WriteLine(exception.Message);
        path = CreateDefaultCollectionFile(cwd ?? Directory.GetCurrentDirectory());
      }

      if (path is not null)
      {
        using FileStream openStream = File.OpenRead(path);
        config = JsonSerializer.Deserialize(openStream, JsonContext.Default.Collection);
      }
    }

    if (config is null)
      throw new Exception("Failed to load configuration.");

    return config;
  }

}
