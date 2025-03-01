namespace PowerUp.ElectronUI.Api.Shared
{
  public record FileResponse(
    string Name,
    Stream Stream,
    string ContentType
  );
}