namespace TVFongMi.Windows.Models;
public sealed class VideoDetail
{
    public string Id { get; init; } = "";
    public string Name { get; init; } = "";
    public string? Poster { get; init; }
    public string? Description { get; init; }
    public string? Director { get; init; }
    public string? Cast { get; init; }
    public string? Year { get; init; }
    public List<EpisodeItem> Episodes { get; init; } = new();
}
