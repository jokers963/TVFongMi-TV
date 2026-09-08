using System.Windows; using TVFongMi.Windows.Services;
namespace TVFongMi.Windows;
public partial class App : Application
{
    private readonly AppLogger _logger = new();
    protected override void OnStartup(StartupEventArgs e) { DispatcherUnhandledException += OnDispatcherUnhandledException; AppDomain.CurrentDomain.UnhandledException += OnUnhandledException; base.OnStartup(e); }
    private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) { _logger.Error("未处理的 UI 异常", e.Exception); MessageBox.Show("程序遇到未预期的错误，详细信息已写入日志。", "影視TV"); e.Handled = true; }
    private void OnUnhandledException(object? sender, UnhandledExceptionEventArgs e) { if (e.ExceptionObject is Exception ex) _logger.Error("未处理的后台异常", ex); }
}
