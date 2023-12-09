using IronPython.Hosting;
using static IronPython.Modules._ast;

namespace MLB_StatsAPI.NET
{
  public class MLBStatsApi
  {
    public static void Main()
    {
      var engine = Python.CreateEngine();
      var scope = engine.CreateScope();
      var paths = engine.GetSearchPaths();
      paths.Add(Environment.CurrentDirectory);
      engine.SetSearchPaths(paths);
      /*
      engine.Execute("import requests");
      engine.Execute("response = requests.get('https://jsonplaceholder.typicode.com/posts/1')");
      var content = engine.Execute<string>("response.content");
      */
    }
  }
}
