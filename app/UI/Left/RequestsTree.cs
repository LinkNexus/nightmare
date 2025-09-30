using Terminal.Gui;

namespace Nightmare.UI.Left;

public class RequestsTree : TreeBuilder<RequestOrFolder>
{

  public RequestsTree() : base(supportsCanExpand: true)
  {
  }

  public override IEnumerable<RequestOrFolder> GetChildren(RequestOrFolder forObject)
  {
    throw new NotImplementedException();
  }
}
