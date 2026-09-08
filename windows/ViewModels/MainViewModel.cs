using System.Collections.ObjectModel; using System.ComponentModel; using System.Runtime.CompilerServices; using TVFongMi.Windows.Models;
namespace TVFongMi.Windows.ViewModels;
public sealed class MainViewModel : INotifyPropertyChanged
{
    public ObservableCollection<CatVodSite> Sites { get; } = new();
    public ObservableCollection<VideoItem> Videos { get; } = new();
    private string _query = "";
    public string Query { get => _query; set { if (_query == value) return; _query = value; OnPropertyChanged(); } }
    public void SetSites(IEnumerable<CatVodSite> sites) { Sites.Clear(); foreach (var site in sites) Sites.Add(site); }
    public void SetVideos(IEnumerable<VideoItem> videos) { Videos.Clear(); foreach (var video in videos) Videos.Add(video); }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
