namespace MdRenderer;

class Writer
{
    public static void OpenAlternateBuffer()
    {
        Console.Write("\x1b[?1049h");
        Console.Clear();
    }

    public static void CloseAlternateBuffer()
    {
        Console.Write("\x1b[?1049l");
    }

    public static void WriteStatusBar(int offset, int total, int viewportHeight, string filePath)
    {
        int percent = total <= viewportHeight ? 100
            : (int)(100.0 * offset / Math.Max(1, total - viewportHeight));

        string left = " [q] Quit  [↑/↓] Scroll  [PgUp/PgDn] Page  [Home/End] Jump ";
        string right = percent switch {100 => $" (END) {filePath} ", _ => $" {percent}% {filePath} "} ;

        int padding = Math.Max(0, Console.WindowWidth - left.Length - right.Length);
        string status = left + new string(' ', padding) + right;

        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(status);
        Console.ResetColor();
    }
}