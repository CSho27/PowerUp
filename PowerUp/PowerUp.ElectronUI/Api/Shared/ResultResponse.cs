namespace PowerUp.ElectronUI.Api.Shared
{
  public class ResultResponse
  {
    public bool Success { get; set; }

    public static ResultResponse Succeeded() => new ResultResponse { Success = true };
  }
}