using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Teams
{
  public class TeamWriter : IDisposable
  {
    private readonly GameSaveObjectWriter _writer;
    private readonly GameSaveFormat _format;

    public TeamWriter(ICharacterLibrary characterLibrary, string fileName, GameSaveFormat format)
    {
      _writer = new GameSaveObjectWriter
        ( characterLibrary
        , fileName
        , format == GameSaveFormat.Wii_2007
          ? ByteOrder.BigEndian
          : ByteOrder.LittleEndian
        );
      _format = format;
    }

    public TeamWriter(GameSaveObjectWriter writer)
    {
      _writer = writer;
    }
    public void Write(int powerProsTeamId, IGSTeam team)
    {
      var teamOffset = TeamOffsetUtils.GetTeamOffset(powerProsTeamId, _format);
      switch (_format)
      {
        case GameSaveFormat.Wii_2007:
          _writer.Write(teamOffset, (GSTeam)team);
          break;
        case GameSaveFormat.Ps2_2007:
          _writer.Write(teamOffset, (Ps2GSTeam)team);
          break;
        default:
          throw new NotImplementedException();
      }
    }

    public void Dispose() => _writer.Dispose();
  }
}
