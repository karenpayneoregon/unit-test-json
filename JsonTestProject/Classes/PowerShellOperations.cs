using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContainerLibrary.Classes;
using Json.Library;
using Json.Library.Classes;

namespace JsonTestProject.Classes
{
    public class PowerShellOperations
    {
        public static async Task<DateContainer> GetPartialComputerInformationTask(string fileName)
        {

            var argument = 
                "Get-ComputerInfo | " + 
                "select BiosReleaseDate,OsLocalDateTime,OsLastBootUpTime,OsUptime | " +
                "ConvertTo-Json";


            var start = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                RedirectStandardOutput = true,
                Arguments = argument,
                CreateNoWindow = true
            };

            using var process = Process.Start(start);
            using var reader = process.StandardOutput;

            process.EnableRaisingEvents = true;

            var fileContents = await reader.ReadToEndAsync();

            await File.WriteAllTextAsync(fileName, fileContents);
            await process.WaitForExitAsync();

            var json = await File.ReadAllTextAsync(fileName);

            return JSonHelper.DeserializeObjectUnixEpochDateTime<DateContainer>(json);

        }
    }
}
