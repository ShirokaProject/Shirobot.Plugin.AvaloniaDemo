# ShiroBot Avalonia Demo Plugin

一个用于演示 ShiroBot Avalonia 渲染能力的示例插件。

插件通过独立的 `.axaml` `UserControl` 渲染图片，适合参考 Avalonia UI、AXAML 预览和复杂控件树的插件开发方式。

## 功能

- 使用 Avalonia `UserControl` 渲染 PNG 图片
- 支持在好友或群聊中通过命令触发截图
- 只引用统一的 `ShiroBot.SDK`，演示其中的 Avalonia 契约与控件渲染 API

## 命令

```text
#render
```

发送 `#render` 后，插件会渲染一张演示截图并回复图片。

## 构建

```bash
dotnet build
```

发布 Release 版本：

```bash
dotnet publish -c Release
```

同级存在核心仓库时，默认使用本地 `ProjectReference` 做编译联调。验证最终 NuGet `buildTransitive` 发布行为时使用：

```bash
dotnet publish -c Release -p:UseLocalShiroBotSdk=false
```

项目目标框架为 `net10.0`，只依赖：

- `ShiroBot.SDK` 0.7.0-rc3

如果仓库旁存在 `../ShiroBot/ShiroBot.SDK/ShiroBot.SDK.csproj`，项目会自动使用本地 `ProjectReference` 便于联调；其他环境使用 NuGet 包。

`ShiroBot.SDK` 的 `buildTransitive` targets 会自动生成单 DLL 插件，并从发布目录移除由宿主提供的 SDK、Avalonia 共享程序集与 runtime 文件。项目不需要自行引用 ILRepack 或编写重打包 target。

ShiroBot Host 默认内置并启用 Avalonia，不再提供或区分 lite 版本。安装时只需把发布出的 `ShiroBot.AvaloniaDemoPlugin.dll` 放入宿主插件目录。

## 许可证

本项目使用 MIT License。
