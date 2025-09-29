namespace Nightmare.Tests;

using Nightmare;

public class CollectionLoaderTest
{
  [Fact]
  public void ShouldThrowCollectionFileNotFoundException()
  {
    Assert.Throws<CollectionFileNotFoundException>(
        () => CollectionLoader.GetConfigPath()
    );
  }

  static void ExecInsideTempDir(Action<string> callback)
  {
    var tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
    Directory.CreateDirectory(tempDir);

    callback(tempDir);

    Directory.Delete(tempDir, true);
  }

  [Fact]
  public void ShouldFindConfigFileInDirectory()
  {
    ExecInsideTempDir(
        (tempDir) =>
        {
          var configFilePath = Path.Combine(tempDir, CollectionLoader.CONFIG_FILE);
          File.WriteAllText(configFilePath, "{}");

          var foundPath = CollectionLoader.GetConfigPath(tempDir);
          Assert.Equal(configFilePath, foundPath);
        }
    );
  }

  [Fact]
  public void ShouldCreateDefaultCollectionFile()
  {
    ExecInsideTempDir(
        (tempDir) =>
        {
          var config = CollectionLoader.GetConfig(tempDir);
          Assert.True(File.Exists(Path.Combine(tempDir, CollectionLoader.CONFIG_FILE)));
          Assert.NotNull(config);
        }
    );
  }
}
