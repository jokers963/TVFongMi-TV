using System.Windows; using Microsoft.Win32; using TVFongMi.Windows.Services;
namespace TVFongMi.Windows;
public partial class MainWindow : Window
{
    private readonly SourceCatalog _catalog = new();
    private readonly SourceImporter _importer = new();
    public MainWindow() { InitializeComponent(); RefreshSources(); }
    private void RefreshSources() { SourceList.ItemsSource = null; SourceList.ItemsSource = _catalog.Sources; StatusText.Text = _catalog.Sources.Count == 0 ? "尚未导入影视源" : $"已加载 {_catalog.Sources.Count} 个影视源"; }
    private async void ImportUrl_Click(object sender, RoutedEventArgs e) { var dialog = new UrlDialog(); if (dialog.ShowDialog() != true) return; try { var result = _catalog.Merge(await _importer.ImportFromUrlAsync(dialog.Url)); RefreshSources(); MessageBox.Show($"导入 {result.ImportedCount} 个影视源。", "导入完成"); } catch (Exception ex) { MessageBox.Show(ex.Message, "导入失败"); } }
    private async void ImportFile_Click(object sender, RoutedEventArgs e) { var dialog = new OpenFileDialog { Filter = "JSON 文件|*.json|所有文件|*.*" }; if (dialog.ShowDialog() != true) return; try { var result = _catalog.Merge(await _importer.ImportFromFileAsync(dialog.FileName)); RefreshSources(); MessageBox.Show($"导入 {result.ImportedCount} 个影视源。", "导入完成"); } catch (Exception ex) { MessageBox.Show(ex.Message, "导入失败"); } }
}
