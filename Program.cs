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

    }
}


/*
 *         using (var serial = new SerialCommunicator("/dev/tty.usbserial-113010763101", 115200, Parity.None, 8))
        {
            await serial.ConnectAsync();

            ELM327Controller.DebugMode = true;

            ICommand setProtocol = CommonComands.CreateSetProtocolToAutoCommand();
            await setProtocol.Execute(serial);


            ICommand faultsAndImReadinessCommand = CommonComands.CreateFaultsAndImReadinessCommand();
            string data = await faultsAndImReadinessCommand.Execute(serial);


            int numberOfFaults = Fault.RetrieveNumberOfFaultCodes(data);
            Console.WriteLine("Number of faults: " + numberOfFaults);

                
            if (numberOfFaults > 0)
            {
                Fault fault = new Fault();

                ICommand getFaultMemoryCommand = CommonComands.CreateGetFaultMemoryCommand();
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
                
            var readinessData = Emissions.RetrieveEmissionReadinessData(data);
            foreach (var pair in readinessData)
            {
                Console.WriteLine($"Monitor: {pair.Key}, Ready: {pair.Value}");
            }
               
                
            serial.Disconnect();
        }
*/