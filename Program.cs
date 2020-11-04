using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;

namespace CSN
{
    public class Utility
    {
        public static StreamReader ExecuteCommandLine(String file, String arguments = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = file;
            startInfo.Arguments = arguments;

            Process process = Process.Start(startInfo);

            return process.StandardOutput;
        }
        public static void GetIPAddress(string MacAddress)
        {
            var arpStream = ExecuteCommandLine("arp", "-a");

            for (int i = 0; i < 3; i++)
            {
                arpStream.ReadLine();
            } // Read entries 

            while (!arpStream.EndOfStream)
            {
                var line = arpStream.ReadLine().Trim();
                while (line.Contains("  "))
                {
                    line = line.Replace("  ", " ");
                }
                var parts = line.Split(' ');
                // Match device's MAC address 
                if (parts.Length > 1 &&  parts[1] == MacAddress)
                {
                    Console.WriteLine($"\nMac-Address:\t{MacAddress}");
                    Console.WriteLine($"IP-Address:\t{parts[0]}");
                    Console.WriteLine($"Address type:\t{parts[2]}");
                    return;
                }
            }
            Console.WriteLine($"There is no IP-Address that matches {MacAddress} Mac-Address");
            return;
        }
    class Program
    {
        
        }
        static void Main(string[] args)
        {
            //1a-99-5b-13-4b-a2
            Console.WriteLine("Enter MAC-Address ");
            string MacAddress = Console.ReadLine();
            Utility.GetIPAddress(MacAddress);
        }
    }
}
