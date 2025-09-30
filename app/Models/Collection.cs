namespace Nightmare.Models;

public class Collection
{
  public string Name { get; set; } = "Nightmare Requests";

  public Dictionary<string, Profile> Profiles { get; set; } = new()
  {
    ["default"] = new Profile { Default = true }
  };
}

public class Profile
{
  public Dictionary<string, object> Data { get; set; } = [];

  public bool Default { get; set; } = false;
}
