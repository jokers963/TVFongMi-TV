# Windows 构建与测试

## 环境

- Windows 10/11
- .NET 8 SDK
- mpv（播放视频时需要）

## 构建

```powershell
dotnet restore windows/TVFongMi.Windows.csproj
dotnet build windows/TVFongMi.Windows.csproj --configuration Release
dotnet publish windows/TVFongMi.Windows.csproj --configuration Release --self-contained true -r win-x64 -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

## 配置

程序数据保存在 `%APPDATA%\\TVFongMi`。可以从界面导入 JSON 源配置；示例文件位于 `windows/samples/`。

示例地址使用 `example.com`，不会提供实际内容源，请替换为你有权使用的地址。
