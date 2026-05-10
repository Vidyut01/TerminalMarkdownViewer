using System.Runtime.InteropServices;
using Markdig;

namespace MdRenderer;

public class Main
{
    public static async Task<int> RunAsync(string filePath)
    {
        Console.CancelKeyPress += (_, e) => e.Cancel = true;
        using var _ = PosixSignalRegistration.Create(PosixSignal.SIGTERM, _ =>
        {
            Writer.CloseAlternateBuffer();
            Environment.Exit(0);
        });

        using var reader = new StreamReader(filePath);
        string content = reader.ReadToEnd();

        var pipeline = new MarkdownPipelineBuilder().UseEmphasisExtras().UsePipeTables().Build();
        var doc = Markdown.Parse(content, pipeline);
        var renderer = new Renderer();
        var lines = renderer.Render(doc, content);

        var pager = new Pager(lines, filePath);
        pager.Run();
        
        return 0;
    }
}
