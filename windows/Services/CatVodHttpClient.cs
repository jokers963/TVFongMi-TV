using System.Net.Http;
using System.Net.Http.Json; using System.Text.Json; using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.Services;
public sealed class CatVodHttpClient
{
    private readonly HttpClient _client;
    public CatVodHttpClient(HttpClient? client = null) { _client = client ?? new HttpClient { Timeout = TimeSpan.FromSeconds(15) }; _client.DefaultRequestHeaders.UserAgent.ParseAdd("TVFongMi-Windows/0.1"); }
    public async Task<JsonDocument> GetJsonAsync(string url, CancellationToken cancellationToken = default)
    { if (!SourceUrlValidator.IsValid(url)) throw new ArgumentException("仅支持 HTTP/HTTPS 地址。", nameof(url)); using var response = await _client.GetAsync(url, cancellationToken); response.EnsureSuccessStatusCode(); await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken); return await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken); }
 public static IReadOnlyList<VideoItem> ReadVideoItems(JsonElement root)
    { var array = root.ValueKind == JsonValueKind.Array ? root : FindArray(root, "list", "data", "results", "items"); if (array.ValueKind != JsonValueKind.Array) return Array.Empty<VideoItem>(); var result = new List<VideoItem>(); foreach (var item in array.EnumerateArray()) { if (item.ValueKind != JsonValueKind.Object) continue; var name = Read(item, "name", "title", "vod_name"); if (string.IsNullOrWhiteSpace(name)) continue; result.Add(new VideoItem { Id = Read(item, "id", "vod_id") ?? name, Name = name, Poster = Read(item, "pic", "poster", "vod_pic"), Year = Read(item, "year", "vod_year"), Remark = Read(item, "remark", "vod_remarks"), Category = Read(item, "type_name", "type", "category", "vod_class") }); } return result; }
    private static JsonElement FindArray(JsonElement root, params string[] keys) { if (root.ValueKind != JsonValueKind.Object) return default; foreach (var key in keys) if (root.TryGetProperty(key, out var value) && value.ValueKind == JsonValueKind.Array) return value; return default; }
    private static string? Read(JsonElement item, params string[] keys) { foreach (var key in keys) if (item.TryGetProperty(key, out var value) && value.ValueKind == JsonValueKind.String) return value.GetString(); return null; }
}
