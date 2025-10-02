using Nightmare.UI.Left;
using NIghtmare.UI.Right;
using Terminal.Gui;

namespace Nightmare.UI.Right;

public class RightSide : View
{

  RecipeSection recipeSection;

  public RightSide(LeftSide leftSide)
  {
    X = Pos.Right(leftSide);
    Height = Dim.Fill();
    Width = Dim.Percent(75);

    CreateRecipeSection();
  }

  void CreateRecipeSection()
  {
    recipeSection = new();
    Add(recipeSection);
  }
}
