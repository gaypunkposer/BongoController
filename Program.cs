using System;
using System.IO;
using System.Linq;
using System.Threading;

using HidLibrary;
using Newtonsoft.Json;

namespace Bongos
{
    public class Program
    {
        private static int pollRate;
        private static HidDevice bongos;
        private static BongoManager manager;
        private static OutputHandler handler;
        private static Thread polling;

        static void Main(string[] args)
        {
            if (args.Length < 1) WriteErrorToConsoleAndExit("Please specify a config file");

            BongoConfig config = null;

            try
            {
                String json = "";
                using (StreamReader sr = new StreamReader(args[0]))
                {
                    json += sr.ReadToEnd();
                }
                config = JsonConvert.DeserializeObject<BongoConfig>(json);
            }
            catch (IOException)
            {
                WriteErrorToConsoleAndExit("File not found");
            }

            if (config == null) WriteErrorToConsoleAndExit("Failed to read config file");

            manager = new BongoManager(config.micThreshold);
            switch (config.output.ToLower().Trim())
            {
                case ("keyboard"):
                    handler = new KeyboardHandler(manager, config.keyboardMapping);
                    break;
                case ("vjoy"):
                    handler = new VJoyHandler(manager, config.vJoyMapping, config.vJoyId, config.micSensitivity);
                    break;
                default:
                    WriteErrorToConsoleAndExit("Unsupported output method");
                    break;
            }

            bongos = HidDevices.Enumerate(config.vendorID, config.productID).FirstOrDefault();
            if (bongos == null) WriteErrorToConsoleAndExit("No device found");

            bongos.OpenDevice();
            bongos.Removed += () => WriteErrorToConsoleAndExit("Device disconnected");

            pollRate = config.pollRate;
            polling = new Thread(PollDevice);
            polling.Start();

            Console.WriteLine($"Beginning bongo translation with method '{config.output}'");
            while (polling.IsAlive)
            {
                Console.ReadKey();
            }
        }

        private static void PollDevice()
        {
            while (true)
            {
                manager?.UpdateState(bongos?.ReadReport(1).Data);
                Thread.Sleep(1000 / pollRate);
            }
        }

        public static void WriteErrorToConsoleAndExit(string message)
        {
            Console.WriteLine($"Error: {message}, press any key to exit.");
            Console.ReadKey();

            bongos?.CloseDevice();
            polling?.Abort();

            Environment.Exit(1);
        }
    }
}
