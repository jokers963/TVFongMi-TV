using System.Text.Json; using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.Services;
public sealed class HistoryStore
{
    private readonly string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TVFongMi", "history.json");
    public IReadOnlyList<HistoryItem> Load() { if (!File.Exists(_path)) return Array.Empty<HistoryItem>(); try { return JsonSerializer.Deserialize<List<HistoryItem>>(File.ReadAllText(_path)) ?? new List<HistoryItem>(); } catch (JsonException) { return Array.Empty<HistoryItem>(); } }
    public void Upsert(HistoryItem item) { var all = Load().Where(x => x.VideoId != item.VideoId).ToList(); item.LastPlayedAt = DateTime.UtcNow; all.Insert(0, item); Save(all.Take(100)); }
    public void Clear() => Save(Array.Empty<HistoryItem>());
    private void Save(IEnumerable<HistoryItem> items) { Directory.CreateDirectory(Path.GetDirectoryName(_path)!); File.WriteAllText(_path, JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true })); }
}
