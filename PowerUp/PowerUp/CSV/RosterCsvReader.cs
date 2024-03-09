using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.CSV
{
  public interface IPlayerCsvReader
  {
    Task<IEnumerable<CsvRosterEntry>> ReadAllPlayers(Stream stream);
  }

  public class RosterCsvReader : IPlayerCsvReader
  {
    public async Task<IEnumerable<CsvRosterEntry>> ReadAllPlayers(Stream stream)
    {
      using var reader = new StreamReader(stream);
      using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
      return await csv.GetRecordsAsync<CsvRosterEntry>().ToListAsync();
    }
  }
}
