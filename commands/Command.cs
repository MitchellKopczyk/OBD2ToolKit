namespace OBDIIToolKit
{
    public class Command : ICommand
    {

        public string pid { get; }
        private readonly Func<string, bool> validator;
        private readonly int timeout;

        public Command(string pid, Func<string, bool> validator, int timeout)
        {
            this.pid = pid;
            this.validator = validator;
            this.timeout = timeout;
        }

        public async Task<string> Execute(ICommunicator communication)
        {
            return await SendCommand(communication, pid, validator);
        }

        private async Task<string> SendCommand(ICommunicator communication, string pid, Func<string, bool> responseValidator)
        {
            ELM327Controller.Write(communication, pid);
            string response = "";
            try
            {
                response = await ELM327Controller.ReadDesiredStringResponse(communication, responseValidator, timeout);

                if (!responseValidator(response))
                {
                    throw new Exception("Failed to retrieve data.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                throw;
            }

            return response;
        }
    }
}
