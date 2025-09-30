namespace Nightmare.Tests;

public class CollectionTest
{
  [Fact]
  public void SetRequestsIdentifiers_ShouldApplyIdentifersCorrectlyAndRecursively()
  {
    var collection = new Collection
    {
      Requests = new()
      {
        ["folder1"] = new()
        {
          Name = "Folder 1",
          Requests = new()
          {
            ["request1"] = new()
            {
              Name = "Request 1",
              Method = "GET",
              Url = "https://example.com/request1"
            },
            ["request2"] = new()
            {
              Name = "Request 2",
              Method = "POST",
              Url = "https://example.com/request2"
            }
          }
        },
        ["request3"] = new()
        {
          Name = "Request 3",
          Method = "PUT",
          Url = "https://example.com/request3"
        }
      }
    };

    collection.SetRequestsIdentifiers();

    Assert.Equal("folder1.request1", collection.Requests["folder1"].Requests["request1"].Identifier);
    Assert.Equal("folder1.request2", collection.Requests["folder1"].Requests["request2"].Identifier);
    Assert.Equal("request3", collection.Requests["request3"].Identifier);
  }
}
