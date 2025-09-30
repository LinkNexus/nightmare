using Nightmare.UI;
using Terminal.Gui;

namespace Nightmare;

public class Program
{
  static void Main()
  {
    var config = CollectionLoader.Instance.GetConfig();
    config.SetRequestsIdentifiers();
    Application.Init();

    try
    {
      Application.Run(new MainWindow(config));
    }
    finally
    {
      Application.Shutdown();
    }
  }
}
