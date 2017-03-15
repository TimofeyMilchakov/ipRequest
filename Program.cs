using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            do
            {
                String ipaddress;
                String track = "";
                Console.WriteLine("Enter IP-address for tracing. If IP-address is empty, will tracing 173.194.122.192");
                ipaddress = Console.ReadLine();
                if (ipaddress.Length == 0)
                {
                    ipaddress = "173.194.122.192";
                }

                var timeout = 120;
                var buffer = new byte[] { 0, 0, 0, 0 };
                var ping = new Ping();
                int i = 0;
                while (i < 30)
                {
                    i++;
                    try
                    {
                        var reply = ping.Send(ipaddress, timeout, buffer, new PingOptions { Ttl = i });

                        if (reply.Status == IPStatus.Success)
                        {
                            track = track + i + " ip: " + reply.Address + "\t ms: " + reply.RoundtripTime + "\n";
                            break;
                        }
                        else
                        {
                            track = track + i + " ip: " + reply.Address + "\t ms: " + reply.RoundtripTime + "\n";
                        }
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error.ToString());
                        break;
                    }
                }
                Console.WriteLine(track);

                Console.WriteLine("For tracing this IP-address by standart command press 'y' \nFor exit press 'Esc' \nFor continuing working press any key");

                key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }

                if (key.Key == ConsoleKey.Y)
                {
                    ProcessStartInfo cmd = new ProcessStartInfo();
                    cmd.FileName = "cmd";
                    cmd.Arguments = @"/k tracert " + ipaddress;
                    Process.Start(cmd);
                }
                Console.WriteLine("\nFor exit press 'Esc' or any key for continuing");
                key = Console.ReadKey();
            } while (key.Key != ConsoleKey.Escape);

        }
    }
}
