namespace OBDIIToolKit
{
    public static class CommonComands
    {
        public static Command CreateFaultsAndImReadinessCommand()
        {
            return new Command("0101", response => response.StartsWith("41 01") && response.Length >= 17, 10000);
        }

        public static Command CreateGetFaultMemoryCommand()
        {
            return new Command("03", response => response.StartsWith("43"), 10000);
        }

        public static Command CreateSetProtocolToAutoCommand()
        {
            return new Command("ATSP0", response => response == "ATSP0OK", 10000);
        }

        /*Create Another Factory for the below methods*/
        public static Command CreateEngineLoadCommand()
        {
            return new Command("0104", response => response.Split(' ').Length == 4, 10000);
        }
    }
}
