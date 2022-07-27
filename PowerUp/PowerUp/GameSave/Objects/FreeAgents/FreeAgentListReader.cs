using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.GameSaves;
using System;

namespace PowerUp.GameSave.Objects.FreeAgents
{
  internal class FreeAgentListReader : IDisposable
  {
    private readonly GameSaveObjectReader _reader;
    private readonly GameSaveFormat _format;

    public FreeAgentListReader(GameSaveObjectReader reader, GameSaveFormat format)
    {
      _reader = reader;
      _format = format;
    }

    public GSFreeAgentList Read() => _reader.Read<GSFreeAgentList>(FreeAgentListOffsetUtils.GetFreeAgentListOffset(_format));
    public void Dispose() => _reader.Dispose();
  }
}
