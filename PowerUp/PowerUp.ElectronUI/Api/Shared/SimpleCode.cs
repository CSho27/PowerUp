namespace PowerUp.ElectronUI.Api.Shared
{
  public class SimpleCode
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public SimpleCode(int id, string name) 
    {
      Id = id;
      Name = name;
    }
  }
}
