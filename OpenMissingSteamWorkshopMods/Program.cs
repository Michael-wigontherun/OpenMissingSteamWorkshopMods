using System.Text.Json;

namespace OpenMissingSteamWorkshopMods
{
    internal class Program
    {
        static string? OutputFolder;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            StellarisJson stellarisJson = new();

            if (args.Length > 1)
            {
                try
                {
                    stellarisJson = JsonSerializer.Deserialize<StellarisJson>(File.ReadAllText(args[0]))!;
                }
                catch
                {
                    Console.WriteLine("File did not exist or was not a Stellaris playlist json.");
                    Help();
                    return;
                }
                if (args.Length == 3) OutputFolder = args[2];
            }
            else
            {
                Help();
                return;
            }

            Console.WriteLine("Control + click on ID to open the workshop link then press enter to get the next one.");
            Console.WriteLine("To skip console enter s and press enter");
            bool skipConsole = false;

            List<string> outputFile = new List<string>();

            foreach (StellarisJsonMod stellarisJsonMod in stellarisJson.mods)
            {
                if(Directory.Exists(Path.Combine(args[1], stellarisJsonMod.steamId))) continue;

                outputFile.Add(stellarisJsonMod.GetLinkFileLine());

                if (skipConsole)continue;
                Console.WriteLine(stellarisJsonMod.GetLinkConsole());
                string input = Console.ReadLine() ?? "";

                if (input.Equals("s", StringComparison.OrdinalIgnoreCase)) skipConsole = true;

                ClearPreviousLine();//Clear Enter
                ClearPreviousLine();//Clear prev link
            }

            File.WriteAllLines(GetOutputFilePath(Path.GetFileNameWithoutExtension(args[0])), outputFile);

        }

        static string GetOutputFilePath(string fileName)
        {
            if(OutputFolder == null) return fileName + ".txt";

            return Path.Combine(OutputFolder, fileName + ".txt");
        }

        //Google gave me this
        static void ClearPreviousLine()
        {
            // 1. Move the cursor up one row
            Console.CursorTop--;

            // 2. Clear the line by overwriting it with blank spaces
            Console.Write(new string(' ', Console.BufferWidth));

            // 3. Move the cursor back to the start of that cleared line
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        static void Help()
        {
            Console.WriteLine("This Consol Application requires atleast two paramater to run.");
            Console.WriteLine("param 1 : stellaris playlist json path");
            Console.WriteLine("Example : \"C:\\Users\\[user]\\Desktop\\Everything_but_a_salad.json\"");
            Console.WriteLine();
            Console.WriteLine("param 2 : stellaris workshop mods folder path");
            Console.WriteLine("Example : \"H:\\SteamLibrary\\steamapps\\workshop\\content\\281990\"");
            Console.WriteLine();
            Console.WriteLine("param 3 : Output folder for the report.");
            Console.WriteLine("If none will write to the same folder as this program.");
        }
    }
}
