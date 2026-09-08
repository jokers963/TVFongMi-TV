using System.Text.Json;
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
        var result = JsonSerializer.Deserialize<List<SourceConfig>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (result is null) throw new JsonException("影视源配置不是有效的 JSON 数组。");
        return result.Where(x => !string.IsNullOrWhiteSpace(x.Name) && SourceUrlValidator.IsValid(x.Url)).ToList();
    }
}
