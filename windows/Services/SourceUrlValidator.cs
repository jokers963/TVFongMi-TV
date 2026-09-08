namespace TVFongMi.Windows.Services;

public static class SourceUrlValidator
{
    public static bool IsValid(string? value) => Uri.TryCreate(value, UriKind.Absolute, out var uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
}
