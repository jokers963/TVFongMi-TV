using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.Services;
public static class SourceHealthCheckerExtensions
{
    public static async Task<IReadOnlyList<SourceHealthResult>> CheckAllAsync(this SourceHealthChecker checker, IEnumerable<SourceConfig> sources, CancellationToken cancellationToken = default)
    {
        var tasks = sources.Where(x => x.Enabled).Select(source => checker.CheckAsync(source, cancellationToken));
        return await Task.WhenAll(tasks);
    }
}
