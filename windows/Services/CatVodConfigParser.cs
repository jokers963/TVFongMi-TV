using System.Text.Json;
using System.Text.RegularExpressions;
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

    public static IReadOnlyList<CatVodSite> ParseSitesLenient(string text)
    {
        var result = new List<CatVodSite>();
        // Fallback for common TVBox files containing comments, single quotes,
        // or one malformed field. We only accept quoted HTTP(S) endpoints.
        var objectPattern = new Regex("\\{[^{}]*?(?:[\\\"'](?:api|url|ext)[\\\"']\\s*[:=]\\s*[\\\"'](?<api>https?://[^\\\"']+)[\\\"'])[^{}]*\\}", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        foreach (Match match in objectPattern.Matches(text))
        {
            var block = match.Value;
            var api = match.Groups["api"].Value.Trim();
            var name = ReadQuoted(block, "name", "title") ?? api;
            var key = ReadQuoted(block, "key", "id") ?? name;
            if (SourceUrlValidator.IsValid(api) && !result.Any(x => x.Api.Equals(api, StringComparison.OrdinalIgnoreCase)))
                result.Add(new CatVodSite { Key = key, Name = name, Api = api, Searchable = true });
        }
        return result;
    }

    private static string? ReadQuoted(string block, params string[] names)
    {
        foreach (var name in names)
        {
            var match = Regex.Match(block, $"[\\\"']{Regex.Escape(name)}[\\\"']\\s*[:=]\\s*[\\\"'](?<value>[^\\\"']+)[\\\"']", RegexOptions.IgnoreCase);
            if (match.Success) return match.Groups["value"].Value.Trim();
        }
        return null;
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
