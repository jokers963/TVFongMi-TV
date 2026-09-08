using System.Net.Http;
using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.Services;
public sealed record SourceHealthResult(string Name, string Url, bool IsReachable, string? Error, TimeSpan? Elapsed);
public sealed class SourceHealthChecker
{
    private readonly HttpClient _client;
    public SourceHealthChecker(HttpClient? client = null) { _client = client ?? new HttpClient { Timeout = TimeSpan.FromSeconds(8) }; }
    public async Task<SourceHealthResult> CheckAsync(SourceConfig source, CancellationToken cancellationToken = default)
    {
        if (!SourceUrlValidator.IsValid(source.Url)) return new(source.Name, source.Url, false, "地址格式无效", null);
        var start = DateTime.UtcNow;
        try { using var response = await _client.GetAsync(source.Url, HttpCompletionOption.ResponseHeadersRead, cancellationToken); return new(source.Name, source.Url, response.IsSuccessStatusCode, response.IsSuccessStatusCode ? null : $"HTTP {(int)response.StatusCode}", DateTime.UtcNow - start); }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException) { return new(source.Name, source.Url, false, ex.Message, DateTime.UtcNow - start); }
    }
}
