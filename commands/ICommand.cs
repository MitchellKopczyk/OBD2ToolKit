namespace OBDIIToolKit
{
    public interface ICommand
    {
        string pid { get; }
        Task<string> Execute(ICommunicator communication);
    }
}
