using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace OBDIIToolKit
{
    public class BluetoothCommunicator : ICommunicator
    {
        private BluetoothClient _bluetoothClient;
        private Stream _bluetoothStream;

        public BluetoothCommunicator()
        {
            _bluetoothClient = new BluetoothClient();
        }

        public string DeviceAddress { get; set; }

        public async Task ConnectAsync()
        {
            try
            {
                if (!IsConnected && !string.IsNullOrEmpty(DeviceAddress))
                {
                    var device = new BluetoothDeviceInfo(BluetoothAddress.Parse(DeviceAddress));

                    if (device != null)
                    {
                        await Task.Run(() =>
                        {
                            _bluetoothClient.Connect(device.DeviceAddress, BluetoothService.SerialPort);
                            _bluetoothStream = _bluetoothClient.GetStream();
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Unable to establish a connection with Bluetooth device.");
                Console.WriteLine(ex.Message);
            }
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                _bluetoothStream.Close();
                _bluetoothClient.Close();
            }
        }

        public void Write(string message)
        {
            if (IsConnected)
            {
                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                _bluetoothStream.Write(data, 0, data.Length);
            }
        }

        public string ReadString()
        {
            if (IsConnected)
            {
                byte[] buffer = new byte[1024];
                int bytesRead = _bluetoothStream.Read(buffer, 0, buffer.Length);
                return System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);
            }
            return string.Empty;
        }

        public bool IsConnected => _bluetoothClient.Connected;

        public void Dispose()
        {
            Disconnect();
        }

        public Task<string> ReadDesiredStringResponse(Func<string, bool> desiredResultFunc, int timeoutDuration)
        {
            throw new NotImplementedException();
        }
    }
}
