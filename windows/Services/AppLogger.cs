namespace TVFongMi.Windows.Services;
public sealed class AppLogger
{
    private readonly string _directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TVFongMi", "logs");
    public void Info(string message) => Write("INFO", message);
    public void Error(string message, Exception? exception = null) => Write("ERROR", exception is null ? message : message + Environment.NewLine + exception);
    private void Write(string level, string message)
    {
        try { Directory.CreateDirectory(_directory); var file = Path.Combine(_directory, DateTime.UtcNow.ToString("yyyy-MM-dd") + ".log"); File.AppendAllText(file, $"[{DateTime.UtcNow:O}] [{level}] {message}{Environment.NewLine}"); } catch { }
    }
}
