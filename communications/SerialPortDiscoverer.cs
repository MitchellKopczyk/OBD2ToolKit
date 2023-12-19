using System.IO.Ports;

namespace OBDIIToolKit
{
    public class SerialPortDiscoverer
    {
        public IEnumerable<string> DiscoverSerialPorts()
        {
            return SerialPort.GetPortNames();
        }
    }
}
