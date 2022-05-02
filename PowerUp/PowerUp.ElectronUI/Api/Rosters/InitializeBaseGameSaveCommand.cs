using PowerUp.GameSave.Api;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class InitializeBaseGameSaveCommand : ICommand<InitializeBaseRequest, InitializeBaseResponse>
  {
    private readonly IBaseRosterInitializer _baseRosterInitializer;

    public InitializeBaseGameSaveCommand(IBaseRosterInitializer baseRosterInitializer)
    {
      _baseRosterInitializer = baseRosterInitializer;
    }

    public InitializeBaseResponse Execute(InitializeBaseRequest request)
    {
      _baseRosterInitializer.Initialize();
      return new InitializeBaseResponse();
    }
  }

  public class InitializeBaseRequest { }
  public class InitializeBaseResponse { }
}
