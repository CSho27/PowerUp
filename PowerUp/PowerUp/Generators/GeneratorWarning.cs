namespace PowerUp.Generators
{
  public class GeneratorWarning
  {
    public string Key { get; set; }
    public string Message { get; set; }

    public GeneratorWarning(string key, string message)
    {
      Key = key;
      Message = message;
    }
  }
}
