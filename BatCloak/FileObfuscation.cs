using System.Text;

namespace BatCloak
{
    internal class FileObfuscation
    {
        public string Process(string path, int level)
        {
            if (!File.Exists(path)) throw new FileNotFoundException();

            Logger.Write("Reading file...");
            string contents = File.ReadAllText(path);

            Logger.Write("Parsing file...");
            List<string> lines = new(contents.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
            lines.Insert(0, "rem https://github.com/ch2sh/BatCloak");

            Logger.Write("Obfuscating...");
            StringBuilder builder = new();
            LineObfuscation obfuscator = new(level);
            builder.AppendLine("@echo off");
            builder.Append(obfuscator.Boilerplate);
            for (int i = 0; i < lines.Count; i++)
            {
                if ((lines[i].StartsWith("rem") || lines[i].StartsWith("::")) && !lines[i].Contains("BatCloak"))
                {
                    Logger.Write($"Comment skipped on line {i + 1}.", LogType.Warning);
                    continue;
                }
                LineObfResult result = obfuscator.Process(lines[i]);
                builder.AppendLine(string.Join(Environment.NewLine, result.Sets));
                builder.AppendLine(result.Result);
            }

            return builder.ToString().TrimEnd('\r', '\n');
        }
    }
}
