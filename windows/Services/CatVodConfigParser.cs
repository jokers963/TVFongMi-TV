using System.Text.Json;
using TVFongMi.Windows.Models;

namespace TVFongMi.Windows.Services;

public static class CatVodConfigParser
{
    public static IReadOnlyList<CatVodSite> ParseSites(string json)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        var array = root.ValueKind == JsonValueKind.Array ? root : FindArray(root, "sites", "source", "urls");
        if (array.ValueKind != JsonValueKind.Array) return Array.Empty<CatVodSite>();
        var result = new List<CatVodSite>();
        foreach (var item in array.EnumerateArray())
        {
            if (item.ValueKind != JsonValueKind.Object) continue;
            var api = ReadString(item, "api", "url", "ext");
            var name = ReadString(item, "name", "title") ?? api;
            if (string.IsNullOrWhiteSpace(api)) continue;
            result.Add(new CatVodSite { Key = ReadString(item, "key", "id") ?? name, Name = name ?? "未命名源", Api = api, Type = ReadString(item, "type", "type_name") ?? "", Searchable = ReadBool(item, "searchable", "search", true) });
        }
        return result;
    }

    private static JsonElement FindArray(JsonElement root, params string[] names)
    {
        if (root.ValueKind != JsonValueKind.Object) return default;
        foreach (var name in names) if (root.TryGetProperty(name, out var value) && value.ValueKind == JsonValueKind.Array) return value;
        return default;
    }
    private static string? ReadString(JsonElement item, params string[] names) { foreach (var name in names) if (item.TryGetProperty(name, out var value) && value.ValueKind == JsonValueKind.String) return value.GetString(); return null; }
    private static bool ReadBool(JsonElement item, string a, string b, bool fallback) { foreach (var name in new[] { a, b }) if (item.TryGetProperty(name, out var value)) { if (value.ValueKind == JsonValueKind.True) return true; if (value.ValueKind == JsonValueKind.False) return false; } return fallback; }
}
