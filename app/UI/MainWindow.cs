using Nightmare.UI.Left;
using Nightmare.UI.Right;
using Terminal.Gui;

namespace Nightmare.UI;

public class MainWindow : Window
{
  Collection collection;
  string selectedProfile;
  SelectProfileDialog selectProfileDialog;
  LeftSide leftSide;
  RightSide rightSide;
  RequestOrFolder selectedRequest;

  public MainWindow(Collection collection)
  {
    this.collection = collection;
    Title = collection.Name;
    BorderStyle = LineStyle.Double;

    ColorScheme = new()
    {
      Normal = new Terminal.Gui.Attribute(Color.White, Color.Black)
    };

    selectedProfile = collection.Profiles
      .FirstOrDefault(p => p.Value.Default).Key
      ?? collection.Profiles.Keys.First();

    SetLeftSide();
    SetRightSide();

    SetProfilesDialog();
    SetKeyBindings();
  }

  void SetLeftSide()
  {
    leftSide = new(collection, selectedProfile);
    leftSide.RequestsSection.OnRequestSelected += (req) =>
    {
      rightSide.RecipeSection.SelectRequestChanged(req);
    };
    Add(leftSide);
  }

  void SetRightSide()
  {
    rightSide = new(leftSide, selectedRequest);
    Add(rightSide);
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
