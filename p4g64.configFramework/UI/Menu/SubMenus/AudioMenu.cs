
using p4g64.configFramework.UI.Menu.Options;
using static p4g64.configFramework.UI.Menu.ConfigMenu;

namespace p4g64.configFramework.UI.Menu.ConfigMenus;
internal unsafe class AudioMenu : IMenu
{
    private MenuOptionList _options;
    
    public string Description => _options.SelectedOption.Description;

    public AudioMenu()
    {
        List<IMenuOption> options = new()
        {
            new ConfirmButton(),
            new SliderOption("BGM", "Change BGM volume settings", 0, 10, 5, 1),
            new SliderOption("SE", "Change SE volume settings", 0, 10, 5, 1),
            new SliderOption("Voice", "Change Voice volume settings", 0, 10, 5, 1),
            new BoolOption("Voiced Line", "Play Voiced Lines in time with the text", true),
            new SelectionOption("Audio Language", "Change settings for audio language", new string[] {"English", "Japanese"}, "English"),
            new SelectionOption("Sound Output Device", "Change sound output device", new string[] {"Default Device", "Realtek(R) Audio"}, "Default Device")
        };

        _options = new MenuOptionList(options, 7);
    }

    public void Draw(ConfigMenuInfo* info, byte alpha)
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
