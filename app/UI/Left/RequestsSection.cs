using Terminal.Gui;

namespace Nightmare.UI.Left;

public class RequestsSection : FrameView
{
  public TreeView<RequestOrFolder> tree;
  Collection collection;

  public event Action<RequestOrFolder> OnRequestSelected;

  public RequestsSection(FrameView profilesSection, Collection collection)
  {
    Y = Pos.Bottom(profilesSection);
    Height = Dim.Fill();
    Width = Dim.Fill();
    Title = "Requests";
    this.collection = collection;

    ImplementTree();
  }

  void ImplementTree()
  {
    tree = new()
    {
      X = 0,
      Y = 0,
      Width = Dim.Fill(),
      Height = Dim.Fill(),
      TreeBuilder = new RequestsTree()
    };

    tree.AddObjects(collection.Requests.Values);

    tree.SelectionChanged += (_, args) =>
    {
      var selected = tree.SelectedObject;

      if (selected is not null && selected.Requests is null)
      {
        if (selected.Url is null)
        {
          MessageBox.ErrorQuery("Error", "Request URL is null", "Ok");
          tree.SelectedObject = ;
        }
        else
          OnRequestSelected?.Invoke(selected);
      }
    };

    Add(tree);
  }

}
