using IronPython.Hosting;
using System;
using System.Diagnostics;
using System.Formats.Tar;
using System.IO.Compression;

namespace MLB_StatsAPI.NET
{
  public class MLBStatsApi
  {
    public const string PACKAGE_NAME = "MLB-StatsAPI";
    public const string SUPPORTED_VERSION = "1.7";

    public static async Task Main()
    {
      var engine = Python.CreateEngine();
      var scope = engine.CreateScope();
      var paths = engine.GetSearchPaths();
      var libDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib");
      if (!paths.Contains(libDirectory))
        throw new Exception("lib directory does not exist on Python ScriptEngine seach paths");

      if(!Directory.Exists(libDirectory))
        Directory.CreateDirectory(libDirectory);

      /*
      var pyPiApiClient = new PyPiApiClient();
      var packageInfo = await pyPiApiClient.GetPackageInfo(PACKAGE_NAME);
      /*
      if (packageInfo.Info.Version != SUPPORTED_PACKAGE_VERSION)
        Console.WriteLine("WARNING: MLBStats-API version is out of date. MLB-StatsAPI.NET may need to be updated to support the latest version");

      using var packageStream = await pyPiApiClient.FetchPackageAsync(PACKAGE_NAME, SUPPORTED_VERSION);
      var packagePath = Path.Combine(libDirectory, $"{PACKAGE_NAME}-{SUPPORTED_VERSION}.whl");
      using var fileStream = new FileStream(packagePath, FileMode.Create);
      packageStream.CopyTo(fileStream);
      */

      string arguments = $"-m pip install {PACKAGE_NAME}";

      ExecuteShellCommand("python", arguments);

      engine.Execute("import statspi");
    }
    private static void ExecuteShellCommand(string command, string arguments)
    {
      ProcessStartInfo processStartInfo = new ProcessStartInfo
      {
        FileName = command,
        Arguments = arguments,
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = false
      };

      using (Process process = new Process { StartInfo = processStartInfo })
      {
        process.Start();
        process.WaitForExit();
        var output = process.StandardOutput.ReadToEnd();
        var error = process.StandardError.ReadToEnd();
        Console.WriteLine(output);
        Console.WriteLine(error);
      }
    }
  }

}
