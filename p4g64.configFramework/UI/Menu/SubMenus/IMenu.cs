
using static p4g64.configFramework.UI.Menu.ConfigMenu;

namespace p4g64.configFramework.UI.Menu.ConfigMenus;

/// <summary>
/// A sub menu of the config menu
/// </summary>
internal unsafe interface IMenu
{
    public string Description { get; }

    public void Draw(ConfigMenuInfo* info, byte alpha);

    public void Process(ConfigMenuInfo* info);
}
