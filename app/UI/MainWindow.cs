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

    selectedProfile = collection.Profiles
      .FirstOrDefault(p => p.Value.Default).Key
      ?? collection.Profiles.Keys.First();

    SetLeftSide();
    rightSide = new(leftSide);

    SetProfilesDialog();
    SetKeyBindings();

    Add(leftSide, rightSide);
  }

  void SetLeftSide()
  {
    leftSide = new(collection, selectedProfile);
    leftSide.RequestsSection.OnRequestSelected += (req) =>
    {
      MessageBox.Query(50, 7, "Request Selected", req.Name, "Ok");
    };
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
