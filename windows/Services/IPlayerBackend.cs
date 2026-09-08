using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.Services;
public interface IPlayerBackend
{
    bool IsAvailable { get; }
    Task PlayAsync(PlaybackRequest request, CancellationToken cancellationToken = default);
}
