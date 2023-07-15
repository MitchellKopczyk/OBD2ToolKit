using System.IO.Ports;

namespace OBDIIToolKit
{
    public class SerialCommunicator : ICommunicator
    {
        public SerialPort Port { get; private set; }

        public SerialCommunicator(string portName, int baudRate, Parity parity, int dataBits)
        {
            Port = new SerialPort(portName, baudRate, parity, dataBits);
        }

        public async Task ConnectAsync()
        {
            try
            {
                if (!Port.IsOpen)
                {
                    await Task.Run(() => Port.Open());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Unable to establish a connection with serial port.");
                Console.WriteLine(ex.Message);
            }
        }

        public void Disconnect()
        {
            if (Port.IsOpen)
            {
                Port.Close();
            }
        }

        public void Write(string message)
        {
            Port.DiscardInBuffer();
            Port.DiscardOutBuffer();
            Port.Write(message);

        }

        public string ReadString()
        {
            return Port.ReadExisting();
        }

        public bool IsConnected
        {
            get
            {
                return Port.IsOpen;
            }
        }

        public void Dispose()
        {
            if (Port.IsOpen)
            {
                Port.Close();
                Port.Dispose();
            }
        }

        public Task<string> ReadDesiredStringResponse(Func<string, bool> desiredResultFunc, int timeoutDuration)
        {
            throw new NotImplementedException();
        }
    }
}
