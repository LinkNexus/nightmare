using System.Collections.ObjectModel;
using System.Data;
using Terminal.Gui;

namespace Nightmare.UI;

public class SelectProfileDialog : Dialog
{

  Button confirmButton;
  Button cancelButton;
  Dictionary<string, Profile> profiles;
  View buttonsView;
  string selectedProfile;
  FrameView profilesListFrame;
  ListView profilesList;
  FrameView dataTableFrame;
  TableView dataTableView;
  DataTable dataTable = new();

  public event Action<string> OnProfileSelected;

  public SelectProfileDialog(Dictionary<string, Profile> profiles, string selectedProfile)
  {
    this.selectedProfile = selectedProfile;
    this.profiles = profiles;
    Title = "Select Profile";
    Width = Dim.Percent(50);
    Height = Dim.Percent(60);

    SetProfileslist();
    SetDataTable();
    SetButtons();
    SetKeyBindings();
  }

  void OnConfirm()
  {
    selectedProfile = (string)profilesList.Source.ToList()[profilesList.SelectedItem];
    OnProfileSelected?.Invoke(selectedProfile);
    Application.RequestStop();
  }
  void OnCancel() => Application.RequestStop();

  void SetKeyBindings()
  {
    KeyDown += (_, args) =>
    {
      if (args.KeyCode == Key.Enter) OnConfirm();
      if (args.KeyCode == Key.Esc) OnCancel();
    };
  }

  void SetProfileslist()
  {
    profilesListFrame = new()
    {

      Title = "Available Profiles",
      Height = Dim.Percent(35),
      Width = Dim.Fill() - 2,
      X = 1,
      Y = 1
    };

    profilesList = new()
    {
      Height = Dim.Fill() - 2,
      Width = Dim.Fill() - 2,
    };
    profilesList.SetSource(new ObservableCollection<string>(profiles.Keys));
    profilesList.SelectedItem = profilesList.Source.ToList().IndexOf(selectedProfile);
    profilesList.HasFocus = true;

    // Add selection change handler
    profilesList.SelectedItemChanged += OnProfileSelectionChanged;

    profilesListFrame.Add(profilesList);
    Add(profilesListFrame);
  }

  void OnProfileSelectionChanged(object? sender, ListViewItemEventArgs args)
  {
    if (args.Item < 0 || args.Item >= profiles.Count) return;

    var profileName = profiles.Keys.ToArray()[args.Item];
    selectedProfile = profileName;

    // Update the data table
    UpdateDataTable();
  }

  void UpdateDataTable()
  {
    dataTable.Clear();

    foreach (var kvp in profiles[selectedProfile].Data)
    {
      dataTable.Rows.Add([kvp.Key, kvp.Value]);
    }
  }

  void SetDataTable()
  {
    dataTableFrame = new()
    {
      Height = Dim.Percent(50),
      Title = "Profile Data",
      Width = Dim.Fill() - 2,
      Y = Pos.Bottom(profilesListFrame) + 1,
      X = 1
    };

    dataTableView = new()
    {
      Height = Dim.Fill(),
      // Height = Dim.Fill() - 2,
      Width = Dim.Fill() - 2,
    };

    dataTable.Columns.Add("Keys");
    dataTable.Columns.Add("Values");

    foreach (var kvp in profiles[selectedProfile].Data)
    {
      dataTable.Rows.Add([kvp.Key, kvp.Value]);
    }

    // Set the data source for the TableView
    dataTableView.Table = new DataTableSource(dataTable);

    dataTableFrame.Add(dataTableView);
    Add(dataTableFrame);
  }

  void SetButtons()
  {
    buttonsView = new()
    {
      Width = Dim.Fill() - 2,
      Height = 3,
      Y = Pos.AnchorEnd(1),
      X = Pos.Center()
    };

    confirmButton = new()
    {
      Y = 0,
      X = 0,
      Text = "Confirm",
      IsDefault = true
    };
    confirmButton.Accepting += (_, args) => OnConfirm();

    cancelButton = new()
    {
      Text = "Cancel",
      Y = 0,
      X = Pos.Right(confirmButton) + 2
    };
    cancelButton.Accepting += (_, args) => OnCancel();

    buttonsView.Add(confirmButton);
    buttonsView.Add(cancelButton);
    Add(buttonsView);
  }
}
