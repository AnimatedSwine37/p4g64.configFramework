using p4g64.configFramework.UI.Menu.Options;

namespace p4g64.configFramework.UI.Menu.ConfigMenus;
internal unsafe class GraphicsMenu : IMenu
{
    private MenuOptionList _options;
    
    public string Description => _options.SelectedOption.Description;

    public GraphicsMenu()
    {
        List<IMenuOption> options = new()
        {
            new ConfirmButton(),
            new SelectionOption("Presets", "Change the preset configuration for the graphics", 
                new[] {"Low", "Middle", "High"}, "Middle"),
            new SelectionOption("Rendering Scale", "Changes the quality of graphics in-game\nSetting to 100% or lower may improve performance", 
                new[] {"50%", "75%", "100%", "125%", "150%", "175%", "200%"}, "100%"),
            new SelectionOption("Shadow Quality", "Changes the graphical quality of shadows\nLowering the value may improve performance",
                new[] {"Low", "Middle", "High"}, "Middle"),
            new BoolOption("Shadow", "Change settings for displaying shadows", true),
            new BoolOption("Anisotropic Filter", "Change settings for anisotropic filter", true),
            new BoolOption("Anti Aliasing", "Improves graphical fidelity\nChanging the setting to \"OFF\" may improve performance", true),
            new SliderOption("Contrast", "Change the contrast level", 1, 5, 3, 1), // TODO add the image to this option
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
