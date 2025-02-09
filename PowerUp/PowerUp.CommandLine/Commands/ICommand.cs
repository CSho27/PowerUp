using System.CommandLine;

namespace PowerUp.CommandLine.Commands
{
  public interface ICommand
  {
    Command Build();
  }
}
