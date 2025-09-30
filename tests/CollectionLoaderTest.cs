namespace Nightmare.Tests;

using Nightmare;

public class CollectionLoaderTest
{

  static void ResetInstance()
  {
    var instanceField = typeof(CollectionLoader).GetField("instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
    instanceField?.SetValue(null, null);
  }

  static void UseCollectionLoaderInstance(Action<CollectionLoader> callback)
  {
    ResetInstance();

    try
    {
      callback(CollectionLoader.Instance);
    }
    finally
    {
      ResetInstance();
    }
  }

  static void ExecInsideTempDir(Action<string, CollectionLoader> callback)
  {
    UseCollectionLoaderInstance(
        (loader) =>
        {
          var tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
          Directory.CreateDirectory(tempDir);
          try
          {
            callback(tempDir, loader);
          }
          finally
          {
            Directory.Delete(tempDir, true);
          }
        }
    );
  }

  [Fact]
  public void GetConfigPath_ShouldThrowException_WhenFileNotFound()
  {
    UseCollectionLoaderInstance(
        (loader) =>
        {
          Assert.Throws<CollectionFileNotFoundException>(
              () => loader.GetConfigPath()
          );
        }
    );
  }

  [Fact]
  public void GetConfigPath_ShouldFindConfigFileInDirectory()
  {
    ExecInsideTempDir(
        (tempDir, loader) =>
        {
          var configFilePath = Path.Combine(tempDir, CollectionLoader.CONFIG_FILE);
          File.WriteAllText(configFilePath, "{}");

          var foundPath = loader.GetConfigPath(tempDir);
          Assert.Equal(configFilePath, foundPath);
        }
    );
  }

  [Fact]
  public void GetConfigPath_ShouldFindConfigFileInParentDirectory()
  {
    ExecInsideTempDir(
        (tempDir, loader) =>
        {
          var configFilePath = Path.Combine(tempDir, CollectionLoader.CONFIG_FILE);
          File.WriteAllText(configFilePath, "{}");

          var subDir = Path.Combine(tempDir, "subdir");
          Directory.CreateDirectory(subDir);

          var foundPath = loader.GetConfigPath(subDir);
          Assert.Equal(configFilePath, foundPath);
        }
    );
  }

  [Fact]
  public void GetConfig_ShouldCreateDefaultConfig_WhenFileNotFound()
  {
    ExecInsideTempDir(
        (tempDir, loader) =>
        {
          var config = loader.GetConfig(tempDir);
          Assert.True(File.Exists(Path.Combine(tempDir, CollectionLoader.CONFIG_FILE)));
          Assert.NotNull(config);
          Assert.Equal("Nightmare Collection", config.Name);
        }
    );
  }
}
