using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.Services;
public static class M3uParser
{
    public static IReadOnlyList<VideoItem> Parse(string content)
    {
        var lines = content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        var result = new List<VideoItem>();
        string? pendingName = null;
        foreach (var raw in lines)
        {
            var line = raw.Trim();
            if (line.StartsWith("#EXTINF", StringComparison.OrdinalIgnoreCase)) { var comma = line.IndexOf(','); pendingName = comma >= 0 ? line[(comma + 1)..].Trim() : "未命名频道"; continue; }
            if (line.StartsWith("#", StringComparison.Ordinal) || !Uri.TryCreate(line, UriKind.Absolute, out var uri)) continue;
            result.Add(new VideoItem { Id = uri.ToString(), Name = pendingName ?? uri.Host, Remark = uri.ToString() });
            pendingName = null;
        }
        return result;
    }
}
