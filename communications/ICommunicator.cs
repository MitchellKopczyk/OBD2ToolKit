namespace OBDIIToolKit
{
    public interface ICommunicator : IDisposable
    {
        Task ConnectAsync();
        void Disconnect();
        bool IsConnected { get; }
        void Write(string command);
        string ReadString();
    }
}
