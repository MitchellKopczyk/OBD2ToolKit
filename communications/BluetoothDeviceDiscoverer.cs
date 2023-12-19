using InTheHand.Net.Sockets;

//Install-Package System.Configuration.ConfigurationManager

namespace OBDIIToolKit
{
    public class BluetoothDeviceDiscoverer
    {
        public IEnumerable<BluetoothDeviceInfo> DiscoverDevices()
        {
            var discoveredDevices = new List<BluetoothDeviceInfo>();

            try
            {
                var client = new BluetoothClient();
                var devices = client.DiscoverDevices();

                discoveredDevices.AddRange(devices);
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


/*
 *      BluetoothDeviceDiscoverer dis = new BluetoothDeviceDiscoverer();
        var discoveredDevice = dis.DiscoverDevices();
        foreach(var device in discoveredDevice)
        {
            Console.WriteLine(device.DeviceName);
            Console.WriteLine(device.address);
        }
*/