namespace ShiroBot.AvaloniaDemoPlugin.Views;

/// <summary>
/// 演示用 ViewModel：纯 POCO，AXAML 通过 {Binding ...} 反射读属性。
/// 无需 INotifyPropertyChanged，因为这是一次性渲染场景。
/// </summary>
public sealed class ScreenshotViewModel
{
    public string Title { get; init; } = string.Empty;
    public string RequestedBy { get; init; } = string.Empty;
    public bool ShowRequestedBy { get; init; } = true;
    public string Timestamp { get; init; } = string.Empty;
    public string Footer { get; init; } = string.Empty;
}
