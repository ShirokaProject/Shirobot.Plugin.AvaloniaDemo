using ShiroBot.SDK.Config;

namespace ShiroBot.AvaloniaDemoPlugin;

public sealed class AvaloniaDemoPluginConfig
{
    [ConfigField(
        "控制截图中是否显示触发命令的用户 ID。",
        Label = "显示用户 ID",
        Type = "boolean",
        Options = new string[] { "false", "true" },
        Min = 0,
        Max = 1,
        Placeholder = "true")]
    public bool ShowId { get; set; } = true;
}
