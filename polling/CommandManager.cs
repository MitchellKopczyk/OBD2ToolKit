namespace OBDIIToolKit
{
    public class CommandManager
    {
        private Dictionary<string, (ICommand, Func<string, Task>)> commandHandlers;

        public CommandManager()
        {
            commandHandlers = new Dictionary<string, (ICommand, Func<string, Task>)>();
        }

        public void RegisterCommandHandler(ICommand command, Func<string, Task> handler)
        {
            commandHandlers[command.id] = (command, handler);
        }

        public async Task ExecuteCommandHandler(string commandID, string response)
        {
            if (commandHandlers.TryGetValue(commandID, out var handler))
            {
                await handler.Item2(response);
            }
        }

        public List<ICommand> GetRegisteredCommands()
        {
            return commandHandlers.Values.Select(item => item.Item1).ToList();
        }
    }
}