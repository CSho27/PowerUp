using PowerUp.Databases;
using PowerUp.Generators;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI.Api.Generation
{
  public class TeamGenerationCommand : ICommand<TeamGenerationRequest, TeamGenerationResponse>
  {
    private readonly ITeamGenerator _teamGenerator;
    private readonly IVoiceLibrary _voiceLibrary;
    private readonly ISkinColorGuesser _skinColorGuesser;

    public TeamGenerationCommand(
      ITeamGenerator teamGenerator,
      IVoiceLibrary voiceLibrary,
      ISkinColorGuesser skinColorGuesser
    )
    {
      _teamGenerator = teamGenerator;
      _voiceLibrary = voiceLibrary;
      _skinColorGuesser = skinColorGuesser;
    }

    public TeamGenerationResponse Execute(TeamGenerationRequest request)
    {
      var result = _teamGenerator.GenerateTeam(
        lsTeamId: request.LSTeamId, 
        year: request.Year, 
        name: request.TeamName, 
        playerGenerationAlgorithm: new LSStatistcsPlayerGenerationAlgorithm(_voiceLibrary, _skinColorGuesser)
      );

      DatabaseConfig.Database.Save(result.Team);
      return new TeamGenerationResponse { TeamId = result.Team.Id!.Value };
    }
  }

  public class TeamGenerationRequest
  {
    public long LSTeamId { get; set; }
    public string TeamName { get; set; } = "";
    public int Year { get; set; }
  }

  public class TeamGenerationResponse
  {
    public int TeamId { get; set; }
  }
}
