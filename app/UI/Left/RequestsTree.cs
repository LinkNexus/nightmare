using Terminal.Gui;

namespace Nightmare.UI.Left;

public class RequestsTree : TreeBuilder<RequestOrFolder>
{

  public RequestsTree() : base(supportsCanExpand: true)
  {
  }

  public override IEnumerable<RequestOrFolder> GetChildren(RequestOrFolder requestOrFolder)
  {
    if (requestOrFolder.Requests is not null)
      return requestOrFolder.Requests.Values;
    else
      return [];
  }
}
