namespace PowerUp.ElectronUI.Api.Shared
{
  public class KeyedCode
  {
    public string Key { get; set; }
    public string Name { get; set; }

    public KeyedCode(string key, string name)
    {
      Key = key;
      Name = name;
    }
  }
}
