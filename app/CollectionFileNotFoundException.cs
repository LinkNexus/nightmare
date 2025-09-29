namespace Nightmare;

public class CollectionFileNotFoundException : FileNotFoundException
{
  public CollectionFileNotFoundException()
      : base(
              string.Format(
                  "The collection file {0} could not found in the current directory, creating one..",
                  CollectionLoader.CONFIG_FILE
                  )
              )
  { }
}
