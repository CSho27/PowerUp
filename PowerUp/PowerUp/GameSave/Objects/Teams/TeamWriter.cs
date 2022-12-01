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
    public void Write(int powerProsTeamId, GSTeam team)
    {
      var teamOffset = TeamOffsetUtils.GetTeamOffset(powerProsTeamId);
      _writer.Write(teamOffset, team);
    }

    public void Dispose() => _writer.Dispose();
  }
}
