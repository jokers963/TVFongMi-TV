namespace TVFongMi.Windows.Models;
public sealed class HistoryItem
{
    public string VideoId { get; set; } = "";
    public string Name { get; set; } = "";
    public string? Poster { get; set; }
    public string? PlayUrl { get; set; }
    public DateTime LastPlayedAt { get; set; } = DateTime.UtcNow;
    public double PositionSeconds { get; set; }
}
