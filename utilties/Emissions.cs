namespace OBDIIToolKit
{
    public static class Emissions
    {
        public static Dictionary<string, string> RetrieveEmissionReadinessData(string response)
        {
            string[] parts = response.Split(' ');

            byte thirdByte = Convert.ToByte(parts[3], 16);
            byte fourthByte = Convert.ToByte(parts[4], 16);
            byte fifthByte = Convert.ToByte(parts[5], 16);

            int bitValue = (thirdByte >> 2) & 1;

            var engineType = bitValue == 1 ? "Spark-ignited" : "Compression-ignited";

            var readinessDict = new Dictionary<string, string>();
            string[] emissionTestsSpark = new string[] { "Catalyst", "Heated Catalyst", "Evaporative System", "Secondary Air System",
                "Gasoline Particulate Filter", "Oxygen Sensor", "Oxygen Sensor Heater", "EGR and/or VVT System" };
            string[] emissionTestsDiesel = new string[] { "NMHC Catalyst", "NOx/SCR Monitor", "Reserved", "Boost Pressure", "Reserved",
                "Exhaust Gas Sensor", "PM filter monitoring", "EGR and/or VVT System" };

            if (engineType == "Spark-ignited")
            {
                for (int i = 0; i < 8; i++)
                {
                    var testAbility = ((fourthByte >> i) & 1) == 1 ? "Supported" : "Not supported";
                    var testCompleteness = ((fifthByte >> i) & 1) == 1 ? "Complete" : "Not complete";
                    readinessDict.Add(emissionTestsSpark[i], $"{testAbility}, {testCompleteness}");
                }
            }
            else if (engineType == "Compression-ignited")
            {
                for (int i = 0; i < 8; i++)
                {
                    var testAbility = ((fourthByte >> i) & 1) == 1 ? "Supported" : "Not supported";
                    var testCompleteness = ((fifthByte >> i) & 1) == 1 ? "Complete" : "Not complete";
                    readinessDict.Add(emissionTestsDiesel[i], $"{testAbility}, {testCompleteness}");
                }
            }
            return readinessDict;
        }
    }
}
