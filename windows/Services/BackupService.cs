using System.Text.Json;
namespace TVFongMi.Windows.Services;
public sealed class BackupService
{
    private readonly string _root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TVFongMi");
    public async Task ExportAsync(string destination, CancellationToken cancellationToken = default)
    {
        var files = Directory.Exists(_root) ? Directory.GetFiles(_root, "*.json") : Array.Empty<string>();
        var data = new Dictionary<string, string>();
        foreach (var file in files) data[Path.GetFileName(file)] = await File.ReadAllTextAsync(file, cancellationToken);
        await File.WriteAllTextAsync(destination, JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }), cancellationToken);
    }
    public async Task ImportAsync(string backupFile, CancellationToken cancellationToken = default)
    {
        await using var stream = File.OpenRead(backupFile);
        var data = await JsonSerializer.DeserializeAsync<Dictionary<string, string>>(stream, cancellationToken: cancellationToken) ?? new();
        Directory.CreateDirectory(_root);
        foreach (var entry in data)
        {
            if (!entry.Key.EndsWith(".json", StringComparison.OrdinalIgnoreCase) || entry.Key.Contains(Path.DirectorySeparatorChar) || entry.Key.Contains(Path.AltDirectorySeparatorChar)) continue;
            await File.WriteAllTextAsync(Path.Combine(_root, entry.Key), entry.Value, cancellationToken);
        }
    }
}
