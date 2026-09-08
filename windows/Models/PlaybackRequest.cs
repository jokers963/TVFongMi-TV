namespace TVFongMi.Windows.Models;
public sealed class PlaybackRequest
{
    public required string Url { get; init; }
    public string? Title { get; init; }
    public IReadOnlyDictionary<string, string> Headers { get; init; } = new Dictionary<string, string>();
}
