using System.Windows;
using System.Windows.Controls;
using TVFongMi.Windows.Models;
using TVFongMi.Windows.Services;

namespace TVFongMi.Windows;

public sealed class EpisodeWindow : Window
{
    public EpisodeWindow(VideoDetail detail, PlaybackService playback)
    {
        Title = $"{detail.Name} - 选集"; Width = 420; Height = 520; WindowStartupLocation = WindowStartupLocation.CenterOwner;
        var list = new ListBox { Margin = new Thickness(16), ItemsSource = detail.Episodes, DisplayMemberPath = "Name" };
        list.MouseDoubleClick += async (_, _) => { if (list.SelectedItem is EpisodeItem episode) try { await playback.PlayAsync(new PlaybackRequest { Url = episode.Url, Title = $"{detail.Name} - {episode.Name}" }); } catch (Exception ex) { MessageBox.Show(ex.Message, "播放失败"); } };
        Content = list;
    }
}
