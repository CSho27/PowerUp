namespace PowerUp.ElectronUI
{
  public static class ProjectPath
  {
    public static string Root => Path.Combine(Environment.CurrentDirectory, "../../../");
    public static string Relative(string relativePath) => Path.GetFullPath(Path.Combine(Root, relativePath));
  }
}
