
using System.IO.Ports;
using OBDIIToolKit;

class Program
{
    static void Main()
    {
        var program = new Program();
        program.RunAsync().Wait();
    }

    async Task RunAsync()
    {

        using (var serial = new SerialCommunicator("/dev/tty.usbserial-113010763101", 115200, Parity.None, 8))
        {
            await serial.ConnectAsync();

            ELM327Controller.DebugMode = true;

            ICommand setProtocol = CommandFactory.CreateSetProtocolToAutoCommand();
            await setProtocol.Execute(serial);


            //Use a command to fetch the data
            ICommand faultsAndImReadinessCommand = CommandFactory.CreateFaultsAndImReadinessCommand();
            string data = await faultsAndImReadinessCommand.Execute(serial);

            //Get the Number of faults from the data
            int numberOfFaults = Fault.RetrieveNumberOfFaultCodes(data);
            Console.WriteLine("Number of faults: " + numberOfFaults);

                
            // If there are faults, retrieve fault data and print them
            if (numberOfFaults > 0)
            {
                Fault fault = new Fault();

                ICommand getFaultMemoryCommand = CommandFactory.CreateGetFaultMemoryCommand();
                string faultData = await getFaultMemoryCommand.Execute(serial);

                List<(string FaultCode, string Description)> faults = await fault.GetFaultMemoryAsync(faultData);
                foreach (var faultItem in faults)
                {
                    Console.WriteLine($"Fault Code: {faultItem.FaultCode}, Description: {faultItem.Description}");
                }
            }
            else
            {
                Console.WriteLine("No Fault Codes detected.");
            }
                

            // Retrieve Emission Readiness to see if we will pass Emission testing
            var readinessData = Emissions.RetrieveEmissionReadinessData(data);
            foreach (var pair in readinessData)
            {
                Console.WriteLine($"Monitor: {pair.Key}, Ready: {pair.Value}");
            }
               
                
            serial.Disconnect();
        }
    }
}
