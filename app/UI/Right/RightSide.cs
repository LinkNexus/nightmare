using Nightmare.UI.Left;
using NIghtmare.UI.Right;
using Terminal.Gui;

namespace Nightmare.UI.Right;

public class RightSide : View
{
  public RecipeSection RecipeSection;

  public RightSide(LeftSide leftSide, RequestOrFolder selectedRequest)
  {
    X = Pos.Right(leftSide);
    Height = Dim.Fill();
    Width = Dim.Percent(75);

    RecipeSection = new(selectedRequest);
    Add(RecipeSection);
  }
}
