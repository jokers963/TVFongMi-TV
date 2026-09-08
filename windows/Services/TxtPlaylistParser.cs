using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.Services;
public static class TxtPlaylistParser
{
    public static IReadOnlyList<VideoItem> Parse(string content)
    {
        var result = new List<VideoItem>();
        foreach (var raw in content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries))
        {
            var line = raw.Trim();
            if (line.Length == 0 || line.StartsWith("#", StringComparison.Ordinal)) continue;
            var separator = line.IndexOf(',');
            var name = separator > 0 ? line[..separator].Trim() : null;
            var url = separator > 0 ? line[(separator + 1)..].Trim() : line;
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri)) continue;
            result.Add(new VideoItem { Id = uri.ToString(), Name = string.IsNullOrWhiteSpace(name) ? uri.Host : name, Remark = uri.ToString() });
        }
        return result;
    }
}
