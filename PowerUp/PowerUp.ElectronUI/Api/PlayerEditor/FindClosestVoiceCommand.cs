using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI.Api.PlayerEditor
{
  public class FindClosestVoiceCommand : ICommand<FindClosestVoiceRequest, SimpleCode>
  {
    private readonly IVoiceLibrary _voiceLibrary;

    public FindClosestVoiceCommand(IVoiceLibrary voiceLibrary)
    {
      _voiceLibrary = voiceLibrary;
    }

    public SimpleCode Execute(FindClosestVoiceRequest request)
    {
      var closest = _voiceLibrary.FindClosestTo(request.FirstName!, request.LastName!);
      return new SimpleCode(closest.Key, closest.Value);
    }
  }

  public class FindClosestVoiceRequest
  {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
  }
}
