using Terminal.Gui;

namespace Nightmare.UI.Left;

public class LeftSide : View
{

  FrameView profilesSection;
  string selectedProfile;
  RequestsSection requestsSection;
  Collection collection;

  public LeftSide(Collection collection, string selectedProfile)
  {
    X = 0;
    Height = Dim.Fill();
    Width = Dim.Percent(25);

    this.selectedProfile = selectedProfile;
    this.collection = collection;
    CreateProfilesSection();
    CreateRequestsSection();
  }

  void CreateProfilesSection()
  {
    profilesSection = new()
    {
      X = 0,
      Width = Dim.Fill(),
      Height = Dim.Auto(),
      Title = "Profiles (p)",
      Text = selectedProfile
    };

    Add(profilesSection);
  }

  void CreateRequestsSection()
  {
    requestsSection = new(profilesSection, collection, selectedProfile);
    Add(requestsSection);
  }

  public void SelectedProfileChanged(string profileName)
  {
    selectedProfile = profileName;
    profilesSection.Text = selectedProfile;
  }

}
