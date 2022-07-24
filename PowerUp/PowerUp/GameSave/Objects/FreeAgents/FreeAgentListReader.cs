using PowerUp.GameSave.IO;
using System;

namespace PowerUp.GameSave.Objects.FreeAgents
{
  internal class FreeAgentListReader : IDisposable
  {
    private readonly GameSaveObjectReader _reader;

    public FreeAgentListReader(GameSaveObjectReader reader)
    {
      _reader = reader;
    }

    public GSFreeAgentList Read() => _reader.Read<GSFreeAgentList>(FreeAgentListOffset.START_OFFSET);
    public void Dispose() => _reader.Dispose();
  }
}
