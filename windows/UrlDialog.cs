using System.Windows; using System.Windows.Controls;
namespace TVFongMi.Windows;
public sealed class UrlDialog : Window
{
    private readonly TextBox _input = new() { MinWidth = 420, Margin = new Thickness(0, 0, 0, 12) };
    public string Url => _input.Text.Trim();
    public UrlDialog() { Title = "导入影视源"; WindowStartupLocation = WindowStartupLocation.CenterOwner; SizeToContent = SizeToContent.WidthAndHeight; var panel = new StackPanel { Margin = new Thickness(20) }; panel.Children.Add(new TextBlock { Text = "请输入 JSON 源地址：" }); panel.Children.Add(_input); var buttons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right }; var ok = new Button { Content = "确定", Width = 80, IsDefault = true, Margin = new Thickness(0, 0, 8, 0) }; ok.Click += (_, _) => { DialogResult = true; Close(); }; var cancel = new Button { Content = "取消", Width = 80, IsCancel = true }; buttons.Children.Add(ok); buttons.Children.Add(cancel); panel.Children.Add(buttons); Content = panel; }
}
