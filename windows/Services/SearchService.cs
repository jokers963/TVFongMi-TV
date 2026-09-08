using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.Services;
public sealed class SearchService
{
    public IReadOnlyList<VideoItem> Filter(IEnumerable<VideoItem> videos, string? query)
    { if (string.IsNullOrWhiteSpace(query)) return videos.ToList(); return videos.Where(x => x.Name.Contains(query.Trim(), StringComparison.OrdinalIgnoreCase) || (x.Remark?.Contains(query.Trim(), StringComparison.OrdinalIgnoreCase) ?? false)).ToList(); }
}
