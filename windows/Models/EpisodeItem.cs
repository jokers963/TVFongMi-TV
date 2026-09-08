namespace TVFongMi.Windows.Models;
public sealed class EpisodeItem
{
    public int Number { get; init; }
    public string Name { get; init; } = "";
    public string Url { get; init; } = "";
    public string? SourceName { get; init; }
}
