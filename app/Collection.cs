using System.Text.Json.Serialization;

namespace Nightmare;

public class Collection
{
  public string Name { get; set; } = "Nightmare Requests";

  public Dictionary<string, Profile> Profiles { get; set; } = new()
  {
    ["default"] = new Profile { Default = true }
  };

  public Dictionary<string, RequestOrFolder> Requests { get; set; } = [];

  public void SetRequestsIdentifiers()
  {
    foreach (var kvp in Requests)
      kvp.Value.SetIdentifers(kvp.Key);
  }
}

public class Profile
{
  public Dictionary<string, object> Data { get; set; } = [];

  public bool Default { get; set; } = false;
}

public class RequestOrFolder
{
  [JsonIgnore]
  public string Identifier { get; private set; }

  public string Name { get; set; }

  public string Method { get; set; }

  public string Url { get; set; }

  public Dictionary<string, RequestOrFolder>? Requests { get; set; } = null;

  public void SetIdentifers(string baseName)
  {
    if (Requests is not null)
    {
      foreach (var kvp in Requests)
      {
        kvp.Value.SetIdentifers(string.Format("{0}.{1}", baseName, kvp.Key));
      }
    }
    else Identifier = baseName;
  }
}


