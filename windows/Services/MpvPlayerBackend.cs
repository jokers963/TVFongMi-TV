using System.Diagnostics; using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.Services;
public sealed class MpvPlayerBackend : IPlayerBackend
{
    private readonly string _executable;
    public MpvPlayerBackend(string executable = "mpv.exe") => _executable = executable;
    public bool IsAvailable => File.Exists(_executable) || _executable.Equals("mpv.exe", StringComparison.OrdinalIgnoreCase);
    public Task PlayAsync(PlaybackRequest request, CancellationToken cancellationToken = default)
    {
        if (!Uri.TryCreate(request.Url, UriKind.Absolute, out var uri) || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps && uri.Scheme != Uri.UriSchemeFile)) throw new ArgumentException("播放地址无效。", nameof(request));
        var info = new ProcessStartInfo { FileName = _executable, UseShellExecute = false, CreateNoWindow = true };
        info.ArgumentList.Add(uri.ToString());
        if (!string.IsNullOrWhiteSpace(request.Title)) { info.ArgumentList.Add("--force-media-title"); info.ArgumentList.Add(request.Title); }
        foreach (var header in request.Headers) { info.ArgumentList.Add("--http-header-fields=" + header.Key + ": " + header.Value); }
        Process.Start(info) ?? throw new InvalidOperationException("无法启动 mpv。");
        return Task.CompletedTask;
    }
}
