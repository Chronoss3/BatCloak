namespace BatCloak
{
    internal enum LogType
    {
        Normal,
        Info,
        Warning,
        Error,
        Success
    }

    internal class Logger
    {
        public static void Write(string message, LogType type = LogType.Normal)
        {
            switch (type)
            {
                case LogType.Normal:
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("[-] ");
                        break;
                    }
                case LogType.Info:
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("[@] ");
                        break;
                    }
                case LogType.Warning:
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("[!] ");
                        break;
                    }
                case LogType.Error:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[!] ");
                        break;
                    }
                case LogType.Success:
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[+] ");
                        break;
                    }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(message + Environment.NewLine);
            Console.ResetColor();
        }
    }
}
