namespace OBDIIToolKit
{
    public class Command : ICommand
    {
        private static int commandCount = 0;

        public string id { get; }  // this is the unique ID
        public string rawId { get; }  // this is the original PID
        private readonly Func<string, bool> validator;
        private readonly int timeout;

        public Command(string pid, Func<string, bool> validator, int timeout)
        {
            rawId = pid;
            id = pid + "_" + commandCount++;
            this.validator = validator;
            this.timeout = timeout;
        }

        public async Task<string> Execute(ICommunicator communication)
        {
            return await SendCommand(communication, id, validator);
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
