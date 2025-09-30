using Nightmare.UI;
using Terminal.Gui;

namespace Nightmare;

public class Program
{
  static void Main()
  {
    var loader = CollectionLoader.Instance;
    Application.Init();

    try
    {
      Application.Run(new MainWindow(loader.GetConfig()));
    }
    finally
    {
      Application.Shutdown();
    }
  }
}
