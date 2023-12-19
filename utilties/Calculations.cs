namespace OBDIIToolKit.features
{
	public static class Calculations
	{
        private static double PerformEngineLoadCalculation(string response)
        {
            string[] parts = response.Split(' ');

            if (parts.Length < 3)
            {
                throw new ArgumentException("Response does not contain enough data.");
            }

            byte loadByte = Convert.ToByte(parts[2], 16);
            double loadPercent = (loadByte / 255.0) * 100.0;

            return loadPercent;
        }

        private static double PerformCoolantTempCalculation(string response)
        {
            string[] parts = response.Split(' ');

            if (parts.Length < 3)
            {
                throw new ArgumentException("Response does not contain enough data.");
            }

            int temp = Convert.ToInt32(parts[2], 16) - 40;

            return temp;
        }
    }
}

