# ShiroBot Avalonia Demo Plugin

一个用于演示 ShiroBot Avalonia 渲染能力的示例插件。

插件通过独立的 `.axaml` `UserControl` 渲染图片，适合参考 Avalonia UI、AXAML 预览和复杂控件树的插件开发方式。

## 功能

- 使用 Avalonia `UserControl` 渲染 PNG 图片
- 支持在好友或群聊中通过命令触发截图
- 演示 `ShiroBot.AvaloniaSdk` 的基础用法

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

项目目标框架为 `net10.0`，依赖：

- `ShiroBot.SDK` 0.5.0
- `ShiroBot.AvaloniaSdk` 0.5.0
- `Avalonia` 12.0.4

## 许可证

本项目使用 MIT License。
