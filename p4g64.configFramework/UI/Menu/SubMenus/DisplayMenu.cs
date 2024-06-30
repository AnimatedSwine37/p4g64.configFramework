using p4g64.configFramework.UI.Menu.Options;

namespace p4g64.configFramework.UI.Menu.ConfigMenus;
internal unsafe class DisplayMenu : IMenu
{
    private MenuOptionList _options;
    
    public string Description => _options.SelectedOption.Description;

    public DisplayMenu()
    {
        List<IMenuOption> options = new()
        {
            new ConfirmButton(),
            new SelectionOption("Resolution", "Change the game's resolution", new[] {"1920x1440", "2560x1440"}, "2560x1440"),
            new SelectionOption("Monitor", "Change the display monitor", new[] {"1", "2"}, "1"),
            new SelectionOption("Screen Mode", "Change the screen size", new[] {"Window", "Fullscreen", "Borderless"}, "Fullscreen"),
            new BoolOption("V Sync", "Change V Sync settings\nTurning V Sync ON may reduce tearing", true),
            new SelectionOption("FPS Limit", "Set the maximum FPS limit", new[] { "30", "60", "90", "120" }, "60"),
        };

        _options = new MenuOptionList(options, 7);
    }
    
    public void Draw(ConfigMenu.ConfigMenuInfo* info, byte alpha)
    {
        float xStart = info->XStart;
        float yStart = info->YStart + 11 + 30;

        _options.DrawOptions(xStart, yStart, info->Alpha, info->CMainSpr);
    }

    public void Process(ConfigMenu.ConfigMenuInfo* info)
    {
        _options.Process();
    }
}
