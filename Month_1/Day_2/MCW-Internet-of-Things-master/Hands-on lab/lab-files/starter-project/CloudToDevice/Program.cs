using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace CloudToDevice
{
    class Program
    {
        static ServiceClient serviceClient;
        static double temperature = 0;
        static int device = 0;
        static string connectionString = "YOUR-CONNECTION-STRING";

        static void Main(string[] args)
        {
            Console.WriteLine("Send temperature control to device from cloud\n");
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

            Console.WriteLine();
            PromptUser().Wait();
        }

        private async static Task SendCloudToDeviceMessageAsync()
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(temperature.ToString()));

            await serviceClient.SendAsync($"Device{device}", commandMessage);
        }

        private async static Task PromptUser()
        {
            bool continueFlag = true;
            Console.Clear();

            do
            {
                device = GetReadLineInteger("Send command to a device - Enter device number (0-9): ",
                                                     0, 9);
                
                temperature = GetReadLineDouble("Enter a temperature (F) to send (65 - 85): ",
                                                     65, 85);
                
                Console.WriteLine();
                Console.WriteLine($"Sending temperature request of {temperature} to Device{device}");
                await SendCloudToDeviceMessageAsync();
                Console.WriteLine();
                if (GetReadLine("Send another message (Y/N): ", new string[] { "Y", "N" }).ToUpper() == "N")
                    continueFlag = false;
                Console.Clear();
            } while (continueFlag);
        }

        private static string GetReadLine(String msg, string[] validChars)
        {
            string keyPressed;
            bool valid = false;

            Console.WriteLine();
            do
            {
                Console.Write(msg);
                keyPressed = Console.ReadLine();
                Console.WriteLine();
                if (Array.Exists(validChars, ch => ch.Equals(keyPressed.ToUpper())))
                    valid = true;

            } while (!valid);
            return keyPressed;
        }

        private static int GetReadLineInteger(String msg, int min, int max)
        {
            bool valid = false;
            int value = 0;

            Console.WriteLine();
            do
            {
                Console.Write(msg);
                Console.WriteLine();
                int.TryParse(Console.ReadLine(), out value);
                if (value >= min && value <= max)
                    valid = true;

            } while (!valid);
            return value;
        }

        private static double GetReadLineDouble(String msg, double min, double max)
        {
            bool valid = false;
            double value = 0;

            Console.WriteLine();
            do
            {
                Console.Write(msg);
                Console.WriteLine();
                double.TryParse(Console.ReadLine(), out value);
                if (value >= min && value <= max)
                    valid = true;

            } while (!valid);
            return value;
        }
    }
}
