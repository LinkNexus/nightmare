using Nightmare.UI.Left;
using Terminal.Gui;

namespace Nightmare.UI;

public class MainWindow : Window
{

  Collection collection;
  string selectedProfile;
  SelectProfileDialog selectProfileDialog;
  LeftSide leftSide;
  string selectedRequestId;

  public MainWindow(Collection collection)
  {
    this.collection = collection;
    Title = collection.Name;
    BorderStyle = LineStyle.Double;

    selectedProfile = collection.Profiles
      .FirstOrDefault(p => p.Value.Default).Key
      ?? collection.Profiles.Keys.First();

    leftSide = new(collection, selectedProfile);

    SetProfilesDialog();
    SetKeyBindings();

    Add(leftSide);
  }

  void SetProfilesDialog()
  {
    selectProfileDialog = new(collection.Profiles, selectedProfile);
    selectProfileDialog.OnProfileSelected += profileName =>
    {
      selectedProfile = profileName;
      leftSide.SelectedProfileChanged(selectedProfile);
    };
  }

  void SetKeyBindings()
  {
    KeyDown += (_, args) =>
    {
      if (args.KeyCode == Key.P) Application.Run(selectProfileDialog);
    };
  }

}
