using NUnit.Framework;
using PowerUp.Libraries;
using PowerUp.Providers;
using Shouldly;

namespace PowerUp.Tests.Providers
{
  public class PowerProsIdAssignerTests
  {
    [Test]
    public void AssignIds_AssignsIdsForPostArbPlayersOrderedByAbility()
    {
      var players = new[]
      { new PowerProsIdParameters
        { PlayerId = 1
        , Overall = 79
        , YearsInMajors = 7
        }
      , new PowerProsIdParameters
        { PlayerId = 2
        , Overall = 85
        , YearsInMajors = 6
        }
      , new PowerProsIdParameters
        { PlayerId = 3
        , Overall = 65
        , YearsInMajors = 10
        }
      };

      var contracts = new[]
      { new PlayerSalaryDetails
        ( playerId: 101
        , powerProsPointsPerYear: 700
        , yearsUntilFreeAgency: 1
        )
      , new PlayerSalaryDetails
        ( playerId: 102
        , powerProsPointsPerYear: 400
        , yearsUntilFreeAgency: 2
        )
      , new PlayerSalaryDetails
        ( playerId: 103
        , powerProsPointsPerYear: 1000
        , yearsUntilFreeAgency: 4
        )
      };

      var results = new PowerProsIdAssigner().AssignIds(players, contracts);
      results.ShouldContain(p => p.Key == 1 && p.Value == 102);
      results.ShouldContain(p => p.Key == 2 && p.Value == 103);
      results.ShouldContain(p => p.Key == 3 && p.Value == 101);
    }

    [Test]
    public void AssignIds_AssignsIdsForRookieContractPlayersWhereYearsRemainingMatch()
    {
      var players = new[]
      { new PowerProsIdParameters
        { PlayerId = 1
        , Overall = 79
        , YearsInMajors = 1
        }
      , new PowerProsIdParameters
        { PlayerId = 2
        , Overall = 85
        , YearsInMajors = 2
        }
      , new PowerProsIdParameters
        { PlayerId = 3
        , Overall = 65
        , YearsInMajors = 3
        }
      };

      var contracts = new[]
      { new PlayerSalaryDetails
        ( playerId: 101
        , powerProsPointsPerYear: 380
        , yearsUntilFreeAgency: 5
        )
      , new PlayerSalaryDetails
        ( playerId: 102
        , powerProsPointsPerYear: 380
        , yearsUntilFreeAgency: 4
        )
      , new PlayerSalaryDetails
        ( playerId: 103
        , powerProsPointsPerYear: 380
        , yearsUntilFreeAgency: 3
        )
      };

      var results = new PowerProsIdAssigner().AssignIds(players, contracts);
      results.ShouldContain(p => p.Key == 1 && p.Value == 101);
      results.ShouldContain(p => p.Key == 2 && p.Value == 102);
      results.ShouldContain(p => p.Key == 3 && p.Value == 103);
    }

    [Test]
    public void AssignIds_AssignsIdsForRookieContractPlayersWhereYearsRemainingDoNotMatch()
    {
      var players = new[]
      { new PowerProsIdParameters
        { PlayerId = 1
        , Overall = 79
        , YearsInMajors = 1
        }
      , new PowerProsIdParameters
        { PlayerId = 2
        , Overall = 85
        , YearsInMajors = 2
        }
      , new PowerProsIdParameters
        { PlayerId = 3
        , Overall = 65
        , YearsInMajors = 3
        }
      };

      var contracts = new[]
      { new PlayerSalaryDetails
        ( playerId: 101
        , powerProsPointsPerYear: 3000
        , yearsUntilFreeAgency: 5
        )
      , new PlayerSalaryDetails
        ( playerId: 102
        , powerProsPointsPerYear: 4000
        , yearsUntilFreeAgency: 2
        )
      , new PlayerSalaryDetails
        ( playerId: 103
        , powerProsPointsPerYear: 5000
        , yearsUntilFreeAgency: 1
        )
      };

      var results = new PowerProsIdAssigner().AssignIds(players, contracts);
      results.ShouldContain(p => p.Key == 1 && p.Value == 102);
      results.ShouldContain(p => p.Key == 2 && p.Value == 101);
      results.ShouldContain(p => p.Key == 3 && p.Value == 103);
    }

    [Test]
    public void AssignIds_AssignsIdsWhenNoContractsMatch()
    {
      var players = new[]
      { new PowerProsIdParameters
        { PlayerId = 1
        , Overall = 79
        , YearsInMajors = 1
        }
      , new PowerProsIdParameters
        { PlayerId = 2
        , Overall = 85
        , YearsInMajors = 2
        }
      , new PowerProsIdParameters
        { PlayerId = 3
        , Overall = 65
        , YearsInMajors = 8
        }
      };

      var contracts = new[]
      { new PlayerSalaryDetails
        ( playerId: 101
        , powerProsPointsPerYear: 3000
        , yearsUntilFreeAgency: 5
        )
      };

      var results = new PowerProsIdAssigner().AssignIds(players, contracts);
      results.ShouldContain(p => p.Key == 1 && p.Value == 103);
      results.ShouldContain(p => p.Key == 2 && p.Value == 102);
      results.ShouldContain(p => p.Key == 3 && p.Value == 101);
    }
  }
}
