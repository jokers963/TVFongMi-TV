using System.Text.Json;
using TVFongMi.Windows.Models;

namespace TVFongMi.Windows.Services;

public sealed class ConfigStore
{
    private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TVFongMi", "sources.json");

    public IReadOnlyList<SourceConfig> LoadSources()
    {
        if (!File.Exists(_filePath)) return Array.Empty<SourceConfig>();
        try
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<SourceConfig>>(json) ?? new List<SourceConfig>();
        }
        catch (JsonException) { return Array.Empty<SourceConfig>(); }
    }

    public void SaveSources(IEnumerable<SourceConfig> sources)
    {
        var directory = Path.GetDirectoryName(_filePath)!;
        Directory.CreateDirectory(directory);
        var json = JsonSerializer.Serialize(sources, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}
