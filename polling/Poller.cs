namespace OBDIIToolKit
{
    public class Poller
    {
        private readonly ICommunicator communicator;
        private readonly int pollingInterval;
        private readonly CommandManager commandManager;
        private Dictionary<string, Task> commandStatus;

        public Poller(ICommunicator communicator, int pollingInterval)
        {
            this.communicator = communicator;
            this.pollingInterval = pollingInterval;
            this.commandManager = new CommandManager();
            this.commandStatus = new Dictionary<string, Task>();
        }

        public void AddCommand(Command command, Func<string, Task> handler)
        {
            commandManager.RegisterCommandHandler(command, handler);
            commandStatus[command.id] = Task.CompletedTask; // Initialize all command statuses as completed
        }

        public async Task StartPolling(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                var registeredCommands = commandManager.GetRegisteredCommands();

                foreach (var command in registeredCommands)
                {
                    // Skip the command if it is still being executed
                    if (!commandStatus[command.id].IsCompleted)
                        continue;

                    commandStatus[command.id] = ExecuteCommand(command);
                }

                await Task.Delay(pollingInterval, ct);
            }
        }

        private async Task ExecuteCommand(ICommand command)
        {
            try
            {
                string response = await command.Execute(communicator);
                await commandManager.ExecuteCommandHandler(command.id, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing command: {ex.Message}");
            }
        }
    }
}