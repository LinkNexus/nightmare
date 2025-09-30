using Nightmare.Models;
using Terminal.Gui;

namespace Nightmare.UI;

public class LeftSide : View
{

  FrameView profilesSection;
  string selectedProfile;

  public LeftSide(Collection collection, string selectedProfile)
  {
    X = 0;
    Height = Dim.Fill();
    Width = Dim.Percent(25);

    this.selectedProfile = selectedProfile;
    CreateProfilesSection();
  }

  void CreateProfilesSection()
  {
    profilesSection = new FrameView()
    {
      X = 0,
      Width = Dim.Fill(),
      Height = Dim.Percent(10),
      Title = "Profiles (p)",
      Text = selectedProfile
    };

    Add(profilesSection);
  }

  public void SelectedProfileChanged(string profileName)
  {
    selectedProfile = profileName;
    profilesSection.Text = selectedProfile;
  }

}
