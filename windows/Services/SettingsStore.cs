using System.Text.Json; using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.Services;
public sealed class SettingsStore
{
    private readonly string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TVFongMi", "settings.json");
    public AppSettings Load() { if (!File.Exists(_path)) return new AppSettings(); try { return JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(_path)) ?? new AppSettings(); } catch (JsonException) { return new AppSettings(); } }
    public void Save(AppSettings settings) { Directory.CreateDirectory(Path.GetDirectoryName(_path)!); File.WriteAllText(_path, JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true })); }
}
