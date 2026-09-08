using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.Services;
public sealed class PlaylistImporter
{
    public IReadOnlyList<VideoItem> Parse(string content, string? fileName = null)
    {
        return SourceFormatDetector.Detect(content, fileName) switch
        {
            SourceFormat.M3u => M3uParser.Parse(content),
            SourceFormat.Txt => TxtPlaylistParser.Parse(content),
            SourceFormat.SourceList or SourceFormat.CatVodConfig => ParseJson(content),
            _ => Array.Empty<VideoItem>()
        };
    }
    private static IReadOnlyList<VideoItem> ParseJson(string content)
    {
        using var document = System.Text.Json.JsonDocument.Parse(content);
        return CatVodHttpClient.ReadVideoItems(document.RootElement);
    }
}
