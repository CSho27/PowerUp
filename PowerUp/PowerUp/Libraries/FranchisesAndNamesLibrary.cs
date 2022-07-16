using PowerUp.Fetchers.MLBLookupService;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp.Libraries
{
  public interface IFranchisesAndNamesLibrary
  {
    IEnumerable<FranchiseDetails> Franchises { get; }
    IEnumerable<FranchiseDetails> Search(string searchText);
  }

  public class FranchiseDetails
  {
    public long LSTeamId { get; }
    public int BeginYear { get; }
    public int? EndYear { get; }
    public string Name { get; }

    public FranchiseDetails(
      long lsTeamId,
      int beginYear,
      int? endYear,
      string name
    )
    {
      LSTeamId = lsTeamId;
      BeginYear = beginYear;
      EndYear = endYear;
      Name = name;
    }
  }

  public class FranchisesAndNamesLibrary : IFranchisesAndNamesLibrary
  {
    public IEnumerable<FranchiseDetails> Franchises { get; }

    public FranchisesAndNamesLibrary(string libraryFilePath)
    {
      Franchises = File.ReadAllLines(libraryFilePath)
        .Select(l => l.Split(','))
        .Select(l => new FranchiseDetails(
          lsTeamId: long.Parse(l[0]), 
          beginYear: int.Parse(l[1]),
          endYear: l[2].TryParseInt(),
          name: l[3]
        ));
    }

    public IEnumerable<FranchiseDetails> Search(string searchText)
    {
      var lowercasedSearchText = searchText.ToLower();
      return Franchises.Where(f => f.Name.ToLower().Contains(lowercasedSearchText));
    }
  }
}
