using ShiroBot.SDK.Config;

namespace ShiroBot.AvaloniaDemoPlugin;

public class AvaloniaDemoPluginConfig
{
    [ConfigField("是否渲染Id。")]
    public bool ShowId { get; set; } = true;
}