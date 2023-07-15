namespace OBDIIToolKit
{
    public interface ICommand
    {
        string id { get; }
        string rawId { get; }
        Task<string> Execute(ICommunicator communication);
    }
}
