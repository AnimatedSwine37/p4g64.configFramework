using p4g64.configFramework.UI.Common;
using p4g64.configFramework.UI.Menu.Options;

namespace p4g64.configFramework.UI.Menu.ConfigMenus;

/// <summary>
/// An abstract menu that should generally be used as a base for any menu.
/// This implements
/// </summary>
public abstract class BaseMenu : IMenu
{
    public abstract string Description { get; }

    public abstract unsafe void Draw(ConfigMenu.ConfigMenuInfo* info, byte alpha);

    public abstract unsafe void Process(ConfigMenu.ConfigMenuInfo* info);
}