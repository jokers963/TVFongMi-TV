using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.Services;
public enum VideoCategory { All, Movie, Series, Variety, Anime }
public sealed class CategoryFilter
{
    public IReadOnlyList<VideoItem> Filter(IEnumerable<VideoItem> items, VideoCategory category)
    {
        if (category == VideoCategory.All) return items.ToList();
        var keywords = category switch { VideoCategory.Movie => new[] { "电影", "movie" }, VideoCategory.Series => new[] { "剧", "电视剧", "series" }, VideoCategory.Variety => new[] { "综艺", "variety" }, VideoCategory.Anime => new[] { "动漫", "动画", "anime" }, _ => Array.Empty<string>() };
        return items.Where(item => keywords.Any(keyword => item.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) || (item.Remark?.Contains(keyword, StringComparison.OrdinalIgnoreCase) ?? false))).ToList();
    }
}
