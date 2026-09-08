using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.Services;
public static class PlayUrlParser
{
    public static IReadOnlyList<EpisodeItem> Parse(string? value, string? sourceName = null)
    {
        if (string.IsNullOrWhiteSpace(value)) return Array.Empty<EpisodeItem>();
        var result = new List<EpisodeItem>(); var number = 1;
        foreach (var part in value.Split('#', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var separator = part.IndexOf('$');
            var name = separator > 0 ? part[..separator] : $"第{number}集";
            var url = separator > 0 ? part[(separator + 1)..] : part;
            if (Uri.TryCreate(url, UriKind.Absolute, out _)) result.Add(new EpisodeItem { Number = number++, Name = name, Url = url, SourceName = sourceName });
        }
        return result;
    }
}
