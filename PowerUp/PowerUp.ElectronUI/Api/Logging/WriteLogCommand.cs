using PowerUp.ElectronUI.Api.Shared;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowerUp.ElectronUI.Api.Logging
{
  public class WriteLogCommand : ICommand<WriteLogRequest, ResultResponse>
  {
    private readonly ILogger _logger;

    public WriteLogCommand(ILogger logger)
    {
      _logger = logger;
    }

    public ResultResponse Execute(WriteLogRequest request)
    {
      _logger.Log(request.LogLevel, $"(UI): {request.Message}");
      return ResultResponse.Succeeded();
    }
  }

  public class WriteLogRequest
  {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LogLevel LogLevel { get; set; }
    public JsonElement? Message { get; set; }
  }
}
