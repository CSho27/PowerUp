namespace PowerUp.ElectronUI.Api.Shared
{
  public class ResultResponse
  {
    public bool Success { get; set; }

    public ResultResponse() { }
    public ResultResponse(bool success) { Success = success; }
    public static ResultResponse Succeeded() => new ResultResponse { Success = true };
    public static ResultResponse Failed() => new ResultResponse { Success = false };
  }
}