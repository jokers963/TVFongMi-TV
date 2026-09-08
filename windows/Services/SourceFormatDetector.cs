using System.Text.Json;
namespace TVFongMi.Windows.Services;
public enum SourceFormat { Unknown, SourceList, CatVodConfig, M3u, Txt }
public static class SourceFormatDetector
{
    public static SourceFormat Detect(string content, string? fileName = null)
    {
        var extension = System.IO.Path.GetExtension(fileName ?? string.Empty).ToLowerInvariant();
        if (extension == ".m3u" || extension == ".m3u8") return SourceFormat.M3u;
        if (extension == ".txt") return SourceFormat.Txt;
        try { using var doc = JsonDocument.Parse(content); var root = doc.RootElement; if (root.ValueKind == JsonValueKind.Array) return SourceFormat.SourceList; if (root.ValueKind == JsonValueKind.Object && (root.TryGetProperty("sites", out _) || root.TryGetProperty("urls", out _))) return SourceFormat.CatVodConfig; }
        catch (JsonException) { }
        return SourceFormat.Unknown;
    }
}
