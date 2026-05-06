namespace MdRenderer;

class Pager(List<string> lines, string filePath)
{
    private readonly List<string> _lines = lines;
    private readonly string _filePath = filePath;
    private int _scrollOffset = 0;

    public void Run()
    {
        try
        {
            Writer.OpenAlternateBuffer();

            while (true)
            {
                int viewportHeight = Console.WindowHeight - 1;
                Draw(viewportHeight);

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.Q:
                        return;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.J:
                        if (_scrollOffset < _lines.Count - viewportHeight)
                            _scrollOffset++;
                        break;

                    case ConsoleKey.UpArrow:
                    case ConsoleKey.K:
                        if (_scrollOffset > 0)
                            _scrollOffset--;
                        break;

                    case ConsoleKey.PageDown:
                    case ConsoleKey.Spacebar:
                        _scrollOffset = Math.Min(
                            _scrollOffset + viewportHeight,
                            Math.Max(0, _lines.Count - viewportHeight));
                        break;

                    case ConsoleKey.PageUp:
                        _scrollOffset = Math.Max(0, _scrollOffset - viewportHeight);
                        break;

                    case ConsoleKey.Home:
                        _scrollOffset = 0;
                        break;

                    case ConsoleKey.End:
                        _scrollOffset = Math.Max(0, _lines.Count - viewportHeight);
                        break;
                }
            }
        }
        finally
        {
            Writer.CloseAlternateBuffer();
        }
    }

    private void Draw(int viewportHeight)
    {
        Console.SetCursorPosition(0, 0);

        for (int i = 0; i < viewportHeight; i++)
        {
            int lineIdx = i + _scrollOffset;
            if (lineIdx < _lines.Count)
                Console.Write(_lines[lineIdx].PadRight(Console.WindowWidth) + "\n");
            else
                Console.Write("~".PadRight(Console.WindowWidth) + "\n");
        }

        Writer.WriteStatusBar(_scrollOffset, _lines.Count, viewportHeight, _filePath);
    }
}
