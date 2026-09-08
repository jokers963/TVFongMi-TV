namespace TVFongMi.Windows.Services;
public static class PlayerLocator
{
    public static string? FindMpv()
    {
        var candidates = new[] { "mpv.exe", Path.Combine(AppContext.BaseDirectory, "mpv.exe"), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "mpv", "mpv.exe"), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", "mpv", "mpv.exe") };
        return candidates.FirstOrDefault(path => path.Equals("mpv.exe", StringComparison.OrdinalIgnoreCase) || File.Exists(path));
    }
}
