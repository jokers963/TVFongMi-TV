using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.Services;
public sealed class PlaybackService
{
    private readonly IPlayerBackend _backend;
    public PlaybackService(IPlayerBackend backend) => _backend = backend;
    public Task PlayAsync(PlaybackRequest request, CancellationToken cancellationToken = default)
    { if (!_backend.IsAvailable) throw new InvalidOperationException("未找到播放器。请安装 mpv 并将 mpv.exe 加入 PATH，或在设置中指定路径。"); return _backend.PlayAsync(request, cancellationToken); }
}
