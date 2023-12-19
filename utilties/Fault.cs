using Microsoft.Data.Sqlite;
using System.Text.RegularExpressions;

namespace OBDIIToolKit
{
    public class Fault
    {
        private SqliteConnection _dbConnection;
        private SqliteCommand _dbCommand;

        public static int RetrieveNumberOfFaultCodes(string response)
        {
            string[] parts = response.Split(' ');

            int numberFaultCodes = Convert.ToInt32(parts[2], 16);

            if (numberFaultCodes > 128)
            {
                numberFaultCodes -= 128;
            }

            return numberFaultCodes;
        }

        public async Task<List<(string FaultCode, string)>> GetFaultMemoryAsync(string faultCodeDump)
        {
            List<(string FaultCode, string)> faultList = new List<(string FaultCode, string)>();

            faultCodeDump = faultCodeDump[6..];
            faultCodeDump = Regex.Replace(faultCodeDump, " ", "");

            _dbConnection = new SqliteConnection("Data Source=Diagnostic Trouble Codes;Version=3;");
            _dbConnection.Open();

            while (faultCodeDump.Length >= 4)
            {
                string faultTempBuffer = faultCodeDump.Substring(0, 4);
                string faultCode = DetermineFaultCategory(faultTempBuffer[0]) + faultTempBuffer[1..];

                string tableName = faultCode[0] switch
                {
                    'P' => "Powertrain",
                    'C' => "Chassis",
                    'U' => "Undefined",
                    _ => ""
                };

                if (!string.IsNullOrEmpty(tableName))
                {
                    _dbCommand = new SqliteCommand($"SELECT Description FROM '{tableName}' WHERE Code= '{faultCode}'", _dbConnection);
                    var description = (await _dbCommand.ExecuteScalarAsync())?.ToString();
                    if (description != null)
                        faultList.Add((faultCode, description));
                }
                else
                {
                    faultList.Add((faultCode, "No Description Found"));
                }
                faultCodeDump = faultCodeDump[4..];
            }

            _dbConnection.Close();

            return faultList;
        }

        private string DetermineFaultCategory(char hexValue)
        {
            return hexValue switch
            {
                '0' => "P0",
                '1' => "P1",
                '2' => "P2",
                '3' => "P3",
                '4' => "C0",
                '5' => "C1",
                '6' => "C2",
                '7' => "C3",
                '8' => "B0",
                '9' => "B1",
                'A' => "B2",
                'B' => "B3",
                'C' => "U0",
                'D' => "U1",
                'E' => "U2",
                'F' => "U3",
                _ => ""
            };
        }
    }
}
