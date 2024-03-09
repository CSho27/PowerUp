using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace PowerUp.CSV
{
  public interface IPlayerCsvWriter
  {
    public Task WriteAllPlayers(Stream stream, IEnumerable<CsvPlayer> players);
  }

  public class PlayerCsvWriter : IPlayerCsvWriter
  {
    public async Task WriteAllPlayers(Stream stream, IEnumerable<CsvPlayer> players)
    {
      var writer = new StreamWriter(stream);
      var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
      await csv.WriteRecordsAsync(players);
      await writer.FlushAsync();
      stream.Seek(0, SeekOrigin.Begin);
    }
  }
}
