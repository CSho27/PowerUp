using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.GameSaves;
using System;

namespace PowerUp.GameSave.Objects.FreeAgents
{
  internal class FreeAgentListWriter : IDisposable
  {
    private readonly GameSaveObjectWriter _writer;
    private readonly GameSaveFormat _format;

    public FreeAgentListWriter(GameSaveObjectWriter writer, GameSaveFormat format)
    {
      _writer = writer;
      _format = format;
    }

    public void Write(GSFreeAgentList freeAgents) => _writer.Write(FreeAgentListOffsetUtils.GetFreeAgentListOffset(_format), freeAgents);
    public void Dispose() => _writer.Dispose();
  }
}
