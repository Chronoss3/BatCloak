using System.Text;
using Commander.NET;
using Commander.NET.Attributes;

namespace BatCloak
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(asciibanner);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Evasive Batch File Obfuscation ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("@ch2sh\n\n");
            Console.ResetColor();

            CommanderParser<CmdOptions> parser = new();
            CmdOptions? options = null;
            try { options = parser.Parse(args); }
            catch
            {
                Console.WriteLine(parser.Usage());
                Environment.Exit(1);
            }

            if (options.Level < 1 || options.Level > 5) Logger.Write("Error: Obfuscation level out of bounds (1-5).", LogType.Error);
            if (!File.Exists(options.Input)) Logger.Write("Error: Input file path not valid.", LogType.Error);

            Logger.Write("Starting...");
            FileObfuscation obfuscator = new();
            string output = obfuscator.Process(options.Input, options.Level);

            Logger.Write("Writing output to: " + options.Output);
            File.WriteAllText(options.Output, output, Encoding.ASCII);

            Logger.Write("Done!", LogType.Success);
        }

        class CmdOptions
        {
#pragma warning disable CS8618, CS0649
            [Parameter("-i", "--input", Description = "Path to input file.", Required = Required.Yes)]
            public string Input;

            [Parameter("-o", "--output", Description = "Output file path.", Required = Required.Yes)]
            public string Output;

            [Parameter("-l", "--level", Description = "Obfuscation level (1-5).", Required = Required.No)]
            public int Level = 3;
#pragma warning restore CS8618, CS0649
        }

        private const string asciibanner = @"
  ____       _______ _____ _      ____          _  __
 |  _ \   /\|__   __/ ____| |    / __ \   /\   | |/ /
 | |_) | /  \  | | | |    | |   | |  | | /  \  | ' / 
 |  _ < / /\ \ | | | |    | |   | |  | |/ /\ \ |  <  
 | |_) / ____ \| | | |____| |___| |__| / ____ \| . \ 
 |____/_/    \_\_|  \_____|______\____/_/    \_\_|\_\
";
    }
}