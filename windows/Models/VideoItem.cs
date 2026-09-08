namespace TVFongMi.Windows.Models;

public sealed class VideoItem
{
    public string Id { get; init; } = "";
    public string Name { get; init; } = "";
    public string? Poster { get; init; }
    public string? Year { get; init; }
    public string? Remark { get; init; }
}
