using TVFongMi.Windows.Models;

namespace TVFongMi.Windows.Services;

public sealed class SourceCatalog
{
    private readonly ConfigStore _store;
    private readonly List<SourceConfig> _sources;

    public SourceCatalog(ConfigStore? store = null)
    {
        _store = store ?? new ConfigStore();
        _sources = _store.LoadSources().ToList();
    }

    public IReadOnlyList<SourceConfig> Sources => _sources;

    public ImportResult Merge(IEnumerable<SourceConfig> imported)
    {
        var warnings = new List<string>();
        var count = 0;
        foreach (var source in imported)
        {
            var existing = _sources.FirstOrDefault(x => string.Equals(x.Url, source.Url, StringComparison.OrdinalIgnoreCase));
            if (existing is not null) { warnings.Add($"已跳过重复源：{source.Name}"); continue; }
            _sources.Add(source);
            count++;
        }
        _store.SaveSources(_sources);
        return new ImportResult(count, warnings);
    }
}
