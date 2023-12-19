using InTheHand.Net.Sockets;

namespace OBDIIToolKit
{
    public class BluetoothDeviceDiscoverer
    {
        public IEnumerable<string> DiscoverDevices()
        {
            var discoveredDevices = new List<string>();

            try
            {
                var client = new BluetoothClient();
                var devices = client.DiscoverDevices();

                discoveredDevices.AddRange(devices.Select(device => device.DeviceAddress.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Unable to discover Bluetooth devices.");
                Console.WriteLine(ex.Message);
            }

            return discoveredDevices;
        }
    }
}
