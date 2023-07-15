using System.Text.RegularExpressions;

namespace OBDIIToolKit
{
    public static class ELM327Controller
    {
        public static bool DebugMode { get; set; } = false;

        public static void Write(ICommunicator communication, string message)
        {

            if (DebugMode)
                Console.WriteLine("Sent: " + message);

            try
            {
                communication.Write(message + "\r");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred during write: " + e.ToString());
            }
        }

        public static string ReadString(ICommunicator communication)
        {
            string response = communication.ReadString().Trim();
            response = Regex.Replace(response, ">", "");
            response = Regex.Replace(response, "\r", "");

            if (DebugMode && !string.IsNullOrEmpty(response))
                Console.WriteLine("Received: " + response);

            return response;
        }

        public static async Task<string> ReadDesiredStringResponse(ICommunicator communication, Func<string, bool> desiredResultFunc, int timeoutDuration)
        {
            string response = "";
            int timeout = timeoutDuration;
            while (!desiredResultFunc(response) && timeout > 0)
            {
                response = ELM327Controller.ReadString(communication);
                timeout -= 10;
                await Task.Delay(10);
            }

            if (DebugMode && timeout <= 0)
                Console.WriteLine("Timeout reached before desired response was received.");

            if (timeout <= 0)
                throw new TimeoutException("Desired response not received within the specified timeout.");

            return response;
        }
    }
}
