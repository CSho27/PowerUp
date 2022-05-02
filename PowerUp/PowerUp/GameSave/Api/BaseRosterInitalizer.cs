using PowerUp.Databases;
using PowerUp.Entities;
using PowerUp.Entities.Rosters;
using PowerUp.Libraries;
using System.IO;
using System.Linq;

namespace PowerUp.GameSave.Api
{
  public interface IBaseRosterInitializer
  {
    void Initialize();
  }

  public class BaseRosterInitalizer : IBaseRosterInitializer
  {
    private readonly IBaseGameSavePathProvider _baseGameSavePathProvider;
    private readonly IRosterImportApi _rosterImportApi;

    public BaseRosterInitalizer(
      IBaseGameSavePathProvider baseGameSavePathProvider,
      IRosterImportApi rosterImportApi
    )
    {
      _baseGameSavePathProvider = baseGameSavePathProvider;
      _rosterImportApi = rosterImportApi;
    }

    public void Initialize()
    {
      var baseRoster = DatabaseConfig.Database
        .LoadAll<Roster>()
        .SingleOrDefault(r => r.SourceType == EntitySourceType.Base);

      if (baseRoster == null)
      {
        using var stream = new FileStream(_baseGameSavePathProvider.GetPath(), FileMode.Open, FileAccess.Read);
        var parameters = new RosterImportParameters
        {
          Stream = stream,
          IsBase = true
        };
        _rosterImportApi.ImportRoster(parameters);
      }
    }
  }
}
