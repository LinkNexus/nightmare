using Terminal.Gui;

namespace NIghtmare.UI.Right;

public class RecipeSection : FrameView
{

  public TextView RouteInfo;

  public RecipeSection()
  {
    Title = "Recipe (c)";
    Height = Dim.Percent(50);
    Width = Dim.Fill();

    RouteInfo = new()
    {
      X = 0,
      Y = 0,
      Width = Dim.Fill(),
      Height = Dim.Fill(),
      Text = "Hello"
    };

    // RouteInfo.SetAttribute(  )

    Add(RouteInfo);
  }

}
