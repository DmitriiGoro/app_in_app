using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("== Fake SingBox Started ==");

        if (args.Length < 2 || args[0] != "-c")
        {
            Console.WriteLine("Usage: FakeSingBox.exe -c <config-file>");
            return;
        }

        string configPath = args[1];

        if (!File.Exists(configPath))
        {
            Console.WriteLine($"Config file not found: {configPath}");
            return;
        }

        Console.WriteLine($"Reading config from: {configPath}");
        Console.WriteLine("--------- BEGIN ---------");

        try
        {
            using StreamReader reader = new StreamReader(configPath);
            int ch;
            while ((ch = reader.Read()) != -1)
            {
                Console.Write((char)ch);
                System.Threading.Thread.Sleep(50); // для эффекта построчной "анимации"
            }

            Console.WriteLine("\n---------- END ----------");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}