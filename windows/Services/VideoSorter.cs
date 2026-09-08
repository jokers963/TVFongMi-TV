using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.Services;
public enum VideoSort { Name, Year, Recent }
public static class VideoSorter
{
    public static IReadOnlyList<VideoItem> Sort(IEnumerable<VideoItem> items, VideoSort sort)
    {
        return sort switch
        {
            VideoSort.Name => items.OrderBy(x => x.Name, StringComparer.OrdinalIgnoreCase).ToList(),
            VideoSort.Year => items.OrderByDescending(x => x.Year).ThenBy(x => x.Name, StringComparer.OrdinalIgnoreCase).ToList(),
            VideoSort.Recent => items.ToList(),
            _ => items.ToList()
        };
    }
}
