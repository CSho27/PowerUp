using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.GameSaves;
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

    public void Write(GSFreeAgentList freeAgents) => _writer.Write(FreeAgentListOffsetUtils.GetFreeAgentListOffset(GameSaveFormat.Wii), freeAgents);
    public void Dispose() => _writer.Dispose();
  }
}
