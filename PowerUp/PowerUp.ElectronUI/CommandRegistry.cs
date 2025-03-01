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
      var interfaceType = GetCommandInterfaceType(commandType);
      var requestType = GetRequestType(interfaceType);
      var commandInfo = new CommandInfo(
        commandType,
        interfaceType,
        requestType
      );
      _commands.Add(commandName, commandInfo);
    }

    public async Task<object> ExecuteCommand(CommandRequest commandRequest)
    {
      if(commandRequest == null)
        throw new ArgumentNullException(nameof(commandRequest));

      var commandName = commandRequest.CommandName;
      if (commandName == null)
        throw new ArgumentNullException(nameof(commandRequest.CommandName));

      if (commandRequest.Request is null)
        throw new ArgumentNullException(nameof(commandRequest.Request));
      var request = commandRequest.Request;

      _commands.TryGetValue(commandName, out var commandInfo);
      if (commandInfo == null)
        throw new ArgumentException($"No command found with the name: {commandName}", nameof(commandName));

      var command = _commandFactory.Invoke(commandInfo.CommandType);
      if (command == null)
        throw new Exception("Command must have a parameterless constructor");

      var deserializedRequest = JsonSerializer.Deserialize(request, commandInfo.RequestType, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

      var isFileRequest = commandInfo.CommandInterfaceType.Name == typeof(IFileRequestCommand<,>).Name;
      var result = isFileRequest
        ? commandInfo.CommandType.InvokeMember("Execute", BindingFlags.InvokeMethod, null, command, [deserializedRequest, commandRequest.File])!
        : commandInfo.CommandType.InvokeMember("Execute", BindingFlags.InvokeMethod, null, command, [deserializedRequest])!;
      return await (dynamic)result;
    }

    private Type GetCommandInterfaceType(Type commandType)
    {
      var interfaces = commandType.GetInterfaces().Where(i => i != typeof(ICommand));
      if (interfaces.Count() != 1)
        throw new Exception($"{commandType.FullName} cannot must implement exactly one ICommand interface");
      return interfaces.Single();
    }

    private Type GetRequestType(Type commandInterfaceType)
    {
      return commandInterfaceType.GenericTypeArguments[0];
    }

    private record CommandInfo(
      Type CommandType,
      Type CommandInterfaceType,
      Type RequestType
    );
  }

  public interface ICommand { }
  public interface ICommand<TRequest, TResponse> : ICommand
  {
    Task<TResponse> Execute(TRequest request);
  }

  public interface IFileRequestCommand<TRequest, TResponse> : ICommand
  {
    Task<TResponse> Execute(TRequest request, IFormFile? file);
  }

  public class CommandRequest
  {
    public string? CommandName { get; set; }
    public string? Request { get; set; }
    public IFormFile? File { get; set; }
  }
}
