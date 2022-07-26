using PowerUp.GameSave.IO;
using System;

namespace PowerUp.GameSave.Objects.FreeAgents
{
  internal class FreeAgentListWriter : IDisposable
  {
    private readonly GameSaveObjectWriter _writer;

    public FreeAgentListWriter(GameSaveObjectWriter writer)
    {
      _writer = writer;
    }

    public void Write(GSFreeAgentList freeAgents) => _writer.Write(FreeAgentListOffset.START_OFFSET, freeAgents);
    public void Dispose() => _writer.Dispose();
  }
}
