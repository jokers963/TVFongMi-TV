# TVFongMi Windows

Windows 客户端移植入口，基于 WPF/.NET 8。

## 已实现

- 首页和分类导航骨架
- JSON 影视源导入（本地文件或 URL）
- CatVod 配置和播放地址解析
- JSON、M3U/M3U8、TXT 播放列表解析
- 影视源去重、本地持久化和连通性检查
- 搜索过滤、历史记录和收藏数据层
- mpv 播放器后端与播放器自动发现
- win-x64 自包含单文件发布配置

## 数据位置

- 影视源：%APPDATA%\\TVFongMi\\sources.json
- 设置：%APPDATA%\\TVFongMi\\settings.json
- 历史：%APPDATA%\\TVFongMi\\history.json
- 收藏：%APPDATA%\\TVFongMi\\favorites.json

## 构建

```powershell
dotnet restore windows/TVFongMi.Windows.csproj
dotnet build windows/TVFongMi.Windows.csproj --configuration Release
dotnet publish windows/TVFongMi.Windows.csproj -p:PublishProfile=win-x64
```

播放视频需要安装 mpv，并将 mpv.exe 放入 PATH 或程序目录。

影视内容来源由用户自行配置；客户端不内置或提供未经授权的内容来源。项目保留 GPLv3 许可证和原作者声明。
