using System.Text.Json;
using Nightmare.Models;

namespace Nightmare;

public class CollectionLoader
{
  public const string CONFIG_FILE = "nightmare.json";
  const int SEARCH_DEPTH = 5;

  static string? configPath = null;
  static Collection config = null;

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

  public static string GetConfigPath(string? cwd = null)
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

  public static Collection GetConfig(string? cwd = null)
  {
    if (config is null)
    {
      string? path = null;

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
        var options = new JsonSerializerOptions
        {
          PropertyNameCaseInsensitive = true
        };
        config = JsonSerializer.Deserialize<Collection>(openStream, options);
      }
    }

    return config;
  }

}
