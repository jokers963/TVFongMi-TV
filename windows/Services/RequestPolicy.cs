namespace TVFongMi.Windows.Services;
public sealed class RequestPolicy
{
    public TimeSpan Timeout { get; init; } = TimeSpan.FromSeconds(15);
    public bool AllowInsecureHttp { get; init; }
    public bool IsAllowed(Uri uri) => uri.Scheme == Uri.UriSchemeHttps || (AllowInsecureHttp && uri.Scheme == Uri.UriSchemeHttp);
}
