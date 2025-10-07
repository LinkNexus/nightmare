using Nightmare;
using Terminal.Gui;

namespace NIghtmare.UI.Right;

public class RecipeSection : FrameView
{
  public View RouteInfo;
  public Label MethodLabel;
  public Label UrlLabel;
  RequestOrFolder selectedRequest;

  public RecipeSection(RequestOrFolder selectedRequest)
  {
    this.selectedRequest = selectedRequest;
    Title = "Recipe (c)";
    Height = Dim.Percent(50);
    Width = Dim.Fill();

    CreateRouteInfo();
  }

  void CreateRouteInfo()
  {
    RouteInfo = new()
    {
      Width = Dim.Fill(),
      Height = Dim.Fill()
    };

    MethodLabel = new()
    {
      ColorScheme = new ColorScheme
      {
        Normal = new(Color.BrightYellow, Color.Black)
      }
    };

    UrlLabel = new()
    {
      Width = Dim.Percent(30)
    };

    if (selectedRequest is not null)
    {
      MethodLabel.Title = selectedRequest.Method;
      UrlLabel.Title = selectedRequest.Url;
    }

    RouteInfo.Add(MethodLabel, UrlLabel);
    Add(RouteInfo);
  }

  public void SelectRequestChanged(RequestOrFolder request)
  {
    MethodLabel.Title = request.Method;
  }

}
