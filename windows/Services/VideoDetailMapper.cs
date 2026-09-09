using System.Text.Json; using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.Services;
public static class VideoDetailMapper
{
    public static VideoDetail FromJson(JsonElement item)
    {
        var detail = new VideoDetail { Id = Read(item, "id", "vod_id") ?? "", Name = Read(item, "name", "title", "vod_name") ?? "未命名", Poster = Read(item, "pic", "poster", "vod_pic"), Description = Read(item, "desc", "description", "vod_content"), Director = Read(item, "director", "vod_director"), Cast = Read(item, "actor", "cast", "vod_actor"), Year = Read(item, "year", "vod_year") };
        if (item.TryGetProperty("episodes", out var episodes) && episodes.ValueKind == JsonValueKind.Array) { var number = 1; foreach (var episode in episodes.EnumerateArray()) { if (episode.ValueKind == JsonValueKind.String) detail.Episodes.Add(new EpisodeItem { Number = number++, Name = $"第{number - 1}集", Url = episode.GetString() ?? "" }); else if (episode.ValueKind == JsonValueKind.Object) detail.Episodes.Add(new EpisodeItem { Number = number++, Name = Read(episode, "name", "title") ?? $"第{number - 1}集", Url = Read(episode, "url", "link", "play_url") ?? "", SourceName = Read(episode, "source", "source_name") }); } }
        if (detail.Episodes.Count == 0)
        {
            var playUrl = Read(item, "vod_play_url", "play_url");
            if (!string.IsNullOrWhiteSpace(playUrl))
            {
                var number = 1;
                foreach (var entry in playUrl.Split('#', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                {
                    var parts = entry.Split('$', 2, StringSplitOptions.TrimEntries);
                    var url = parts.Length == 2 ? parts[1] : parts[0];
                    if (Uri.TryCreate(url, UriKind.Absolute, out _)) detail.Episodes.Add(new EpisodeItem { Number = number++, Name = parts.Length == 2 ? parts[0] : $"第{number - 1}集", Url = url });
                }
            }
        }
        return detail;
    }
    private static string? Read(JsonElement item, params string[] names) { foreach (var name in names) if (item.TryGetProperty(name, out var value) && value.ValueKind == JsonValueKind.String) return value.GetString(); return null; }
}
