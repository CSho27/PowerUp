using PowerUp.Libraries;

namespace PowerUp.ElectronUI.Api.Searching
{
  public class FranchiseLookupCommand : ICommand<FranchiseLookupRequest, FranchiseLookupResponse>
  {
    private readonly IFranchisesAndNamesLibrary _library;

    public FranchiseLookupCommand(IFranchisesAndNamesLibrary library)
    {
      _library = library;
    }

    public FranchiseLookupResponse Execute(FranchiseLookupRequest request)
    {
      var results = _library.Search(request.SearchText!);
      return new FranchiseLookupResponse(results); 
    }
  }

  public class FranchiseLookupRequest
  {
    public string? SearchText { get; set; }
  }

  public class FranchiseLookupResponse
  {
    public IEnumerable<FranchiseLookupResult> Results { get; set; }

    public FranchiseLookupResponse(IEnumerable<FranchiseDetails> franchises)
    {
      Results = franchises.Select(f => new FranchiseLookupResult(f));
    }
  }

  public class FranchiseLookupResult
  {
    public long LSTeamId { get; }
    public int BeginYear { get; }
    public int? EndYear { get; }
    public string Name { get; }

    public FranchiseLookupResult(FranchiseDetails details)
    {
      LSTeamId = details.LSTeamId;
      BeginYear = details.BeginYear;
      EndYear = details.EndYear;
      Name = details.Name;
    }
  }
}
