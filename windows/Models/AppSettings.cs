namespace TVFongMi.Windows.Models;
public sealed class AppSettings
{
    public string? MpvPath { get; set; }
    public bool PreferHttps { get; set; } = true;
    public bool RememberHistory { get; set; } = true;
}
