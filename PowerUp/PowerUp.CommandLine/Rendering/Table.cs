using System.Text;

namespace PowerUp.CommandLine.Rendering
{
  public class Table<T>(IEnumerable<Column<T>> columns)
  {
    public string Render(IEnumerable<T> rows)
    {
      var tableRows = rows.Select(GetRow).ToList();
      var columnVaules = tableRows
        .SelectMany(r => r.ColumnValues.Select((c, i) => (Value: c, Index: i))).ToList();
      var tableColumns = columns
        .Select((c, i) =>
        {
          var maxWidth = columnVaules
            .Where(v => v.Index == i)
            .Select(v => v.Value)
            .Append(c.Name)
            .Max(c => c.Length);
          return new Column(c.Name, maxWidth);
        }).ToList();
      var formatString = tableColumns
        .Select((c, i) => $"{{{i},-{c.Width}}}")
        .StringJoin(new string(' ', 2));
      var headerNames = columns.Select(c => c.Name).ToArray();

      var sb = new StringBuilder();
      sb.AppendLine(string.Format(formatString, headerNames));
      foreach(var row in tableRows)
        sb.AppendLine(string.Format(formatString, row.ColumnValues));

      return sb.ToString();
    }

    private Row GetRow(T row)
    {
      var rowColumns = columns
        .Select(c => c.GetValue(row)?.ToString() ?? "")
        .ToArray();
      return new Row(rowColumns);
    }

    private record Row(string[] ColumnValues);
    private record Column(string Name, int Width);
  }

  public record Column<T>(string Name, Func<T, object?> GetValue);
}
