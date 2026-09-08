using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using TVFongMi.Windows.Models;

namespace TVFongMi.Windows.Services;

public sealed class SourceImporter
{
    private readonly HttpClient _httpClient;

    public SourceImporter(HttpClient? httpClient = null) => _httpClient = httpClient ?? new HttpClient();

    public async Task<IReadOnlyList<SourceConfig>> ImportFromUrlAsync(string url, CancellationToken cancellationToken = default)
    {
        if (!SourceUrlValidator.IsValid(url)) throw new ArgumentException("仅支持有效的 HTTP/HTTPS 地址。", nameof(url));
        using var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return Parse(json);
    }

    public async Task<IReadOnlyList<SourceConfig>> ImportFromFileAsync(string path, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(path)) throw new FileNotFoundException("找不到影视源文件。", path);
        var json = await File.ReadAllTextAsync(path, cancellationToken);
        return Parse(json);
    }

    private static IReadOnlyList<SourceConfig> Parse(string json)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        try
        {
            var result = JsonSerializer.Deserialize<List<SourceConfig>>(json, options);
            if (result is not null && result.Count > 0)
                return result.Where(IsUsable).ToList();
        }
        catch (JsonException)
        {
            // CatVod/TVBox configs in the wild are not always strict JSON.
        }

        var sites = CatVodConfigParser.ParseSitesLenient(json);
        if (sites.Count > 0)
            return sites.Select(x => new SourceConfig { Name = x.Name, Url = x.Api }).Where(IsUsable).ToList();

        throw new JsonException("无法识别影视源格式。请导入源列表 JSON，或包含 api/url 字段的 CatVod 配置。");
    }

    private static bool IsUsable(SourceConfig x) =>
        !string.IsNullOrWhiteSpace(x.Name) && SourceUrlValidator.IsValid(x.Url);
}
