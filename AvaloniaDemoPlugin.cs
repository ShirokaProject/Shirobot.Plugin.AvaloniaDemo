using ShiroBot.AvaloniaDemoPlugin.Views;
using ShiroBot.AvaloniaSdk;
using ShiroBot.Model.Common;
using ShiroBot.SDK.Abstractions;
using ShiroBot.SDK.Core;
using ShiroBot.SDK.Plugin;

namespace ShiroBot.AvaloniaDemoPlugin;

/// <summary>
/// 演示如何用独立 .axaml + UserControl 渲染图片。
/// 适合需要 IDE 智能提示、axaml 调试预览、复杂控件树场景。
///
/// 命令：
/// - #render：好友/群聊里发起一次截图
/// 
/// </summary>

[BotPlugin(id: "AvaloniaDemoPlugin",
    Name = "AvaloniaDemoPlugin",
    Version = "0.7.0",
    Author = "ShirokaProject",
    Category = PluginCategory.Development,
    Description = "演示通过独立 .axaml UserControl 使用宿主内置 Avalonia 渲染图片。",
    GithubRepo = "ShirokaProject/Shirobot.Plugin.AvaloniaDemo",
    IsPluginSingleFile = true)
]
public sealed class AvaloniaDemoPlugin : PluginBase
{
    public override string Name => "AvaloniaDemoPlugin";
    private AvaloniaDemoPluginConfig? _config;
    protected override Task LoadAsync()
    {
        _config = Context.Config.Load<AvaloniaDemoPluginConfig>();
        AllCommands.MapExact("#render", HandleRenderAsync);
        BotLog.Info("[AvaloniaDemoPlugin] 已加载，使用 #render 触发截图。");
        return Task.CompletedTask;
    }

    private async Task HandleRenderAsync(IncomingMessage message)
    {
        var id = message switch
        {
            GroupIncomingMessage groupMsg => groupMsg.SenderId,
            FriendIncomingMessage privateMsg => privateMsg.SenderId,
            _ => 0L
        };
        
        var segment = await RenderAsync(id.ToString(), _config?.ShowId ?? true).ConfigureAwait(false);
        if (segment is null)
        {
            await Context.Message.ReplyAsync(message, "宿主未提供 Avalonia 渲染服务，无法渲染图片。");
            return;
        }

        await Context.Message.ReplyAsync(message, segment);
    }

    private async Task<ImageOutgoingSegment?> RenderAsync(string requestedBy, bool showRequestedBy)
    {
        var vm = new ScreenshotViewModel
        {
            Title = "ShiroBot Screenshot",
            RequestedBy = requestedBy,
            ShowRequestedBy = showRequestedBy,
            Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            Footer = $"AvaloniaDemoPlugin v{GetType().Assembly.GetName().Version}"
        };
        
        if (Context.Render is null) { return null; }
        var image = await Context.RenderControlPngAsync<ScreenshotView>(
            vm,
            new ControlRenderOptions(RenderTheme.Auto));

        return new ImageOutgoingSegment("base64://" + Convert.ToBase64String(image));
    }
}
