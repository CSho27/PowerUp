using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Text.Json;

namespace PowerUp.ElectronUI
{
  public class CommandRegistry
  {
    private readonly IDictionary<string, CommandInfo> _commands = new Dictionary<string, CommandInfo>();
    private readonly Func<Type, object> _commandFactory;

    public CommandRegistry(Func<Type, object> commandFactory)
    {
      _commandFactory = commandFactory;
    }

    public void RegisterCommand(Type commandType, string commandName)
    {
      var commandInterfaceType = commandType.GetInterface("ICommand`2");
      if (commandInterfaceType == null)
        throw new Exception($"{commandType.FullName} does not implement the ICommand interface");

      Type requestType = commandInterfaceType.GetGenericArguments()[0];
      _commands.Add(commandName, new CommandInfo(commandType, requestType));
    }

    public object ExecuteCommand(CommandRequest commandRequest)
    {
      if(commandRequest == null)
        throw new ArgumentNullException(nameof(commandRequest));

      var commandName = commandRequest.CommandName;
      if (commandName == null)
        throw new ArgumentNullException(nameof(commandRequest.CommandName));

      if (!commandRequest.Request.HasValue)
        throw new ArgumentNullException(nameof(commandRequest.Request));
      var request = commandRequest.Request.Value;

      _commands.TryGetValue(commandName, out var commandInfo);
      if (commandInfo == null)
        throw new ArgumentException($"No command found with the name: {commandName}", nameof(commandName));

      var command = _commandFactory.Invoke(commandInfo.CommandType);
      if (command == null)
        throw new Exception("Command must have a parameterless constructor");

      var deserializedRequest = JsonConvert.DeserializeObject(request.GetRawText(), commandInfo.RequestType);
      return commandInfo.CommandType.InvokeMember("Execute", BindingFlags.InvokeMethod, null, command, new[] { deserializedRequest })!;
    }

    private class CommandInfo
    {
      public Type CommandType { get; }
      public Type RequestType { get; }

      public CommandInfo(Type commandType, Type requestType)
      {
        CommandType = commandType;
        RequestType = requestType;
      }
    }
  }

  public interface ICommand<TRequest, TResponse>
  {
    TResponse Execute(TRequest request);
  }

  public class CommandRequest
  {
    public string? CommandName { get; set; }
    public JsonElement? Request { get; set; }
  }
}
