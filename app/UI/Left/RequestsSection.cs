using Terminal.Gui;

namespace Nightmare.UI.Left;

public class RequestsSection : FrameView
{

  Collection collection;
  string selectedProfile;
  TreeView<RequestOrFolder> treeView;

  public RequestsSection(View profilesSection, Collection collection, string selectedProfile)
  {
    this.collection = collection;
    this.selectedProfile = selectedProfile;
    Y = Pos.Bottom(profilesSection);
    Height = Dim.Fill();
    Width = Dim.Fill();
    Title = "Requests (r)";

    ImplementTreeView();
  }

  void ImplementTreeView()
  {
    treeView = new()
    {
      X = 0,
      Y = 0,
      Width = Dim.Fill(),
      Height = Dim.Fill(),
      TreeBuilder = new RequestsTree()
    };

    treeView.AddObjects(collection.Requests.Values);

    Add(treeView);
  }

}
