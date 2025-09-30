using Terminal.Gui;

namespace Nightmare.UI.Left;

public class RequestsSection : FrameView
{

  Collection collection;
  string selectedProfile;
  TreeView treeView;

  public RequestsSection(View profilesSection, Collection collection, string selectedProfile)
  {
    this.collection = collection;
    this.selectedProfile = selectedProfile;
    X = 0;
    Y = Pos.Bottom(profilesSection) + 1;
    Height = Dim.Fill();
    Width = Dim.Fill();
    Title = "Requests (r)";
  }

  void ImplementTreeView()
  {

    treeView = new()
    {
      X = 0,
      Y = 0,
      Width = Dim.Fill(),
      Height = Dim.Fill(),
      TreeBuilder = new TreeNodeBuilder()
      {

      }
    };
  }



}
