using System.Text.Json; using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.Services;
public sealed class FavoritesStore
{
    private readonly string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TVFongMi", "favorites.json");
    public IReadOnlyList<VideoItem> Load() { if (!File.Exists(_path)) return Array.Empty<VideoItem>(); try { return JsonSerializer.Deserialize<List<VideoItem>>(File.ReadAllText(_path)) ?? new List<VideoItem>(); } catch (JsonException) { return Array.Empty<VideoItem>(); } }
    public void Toggle(VideoItem item) { var all = Load().ToList(); var existing = all.FirstOrDefault(x => x.Id == item.Id); if (existing is null) all.Add(item); else all.Remove(existing); Directory.CreateDirectory(Path.GetDirectoryName(_path)!); File.WriteAllText(_path, JsonSerializer.Serialize(all, new JsonSerializerOptions { WriteIndented = true })); }
}
