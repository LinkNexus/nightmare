using Terminal.Gui;

namespace NIghtmare.UI.Right;

public class RecipeSection : FrameView
{

  TextView routeInfo;

  public RecipeSection()
  {
    Title = "Recipe (c)";
    Height = Dim.Percent(50);
    Width = Dim.Fill();
  }

}
