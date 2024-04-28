
using p4g64.configFramework.UI.Menu.Options;
using static p4g64.configFramework.UI.Menu.ConfigMenu;

namespace p4g64.configFramework.UI.Menu.ConfigMenus;
internal unsafe class AudioMenu : IMenu
{
    private List<IMenuOption> _options = new()
    {
        new ConfirmButton(),
        new SliderOption("BGM", "Change BGM volume settings", 0, 10, 5),
        new SliderOption("SE", "Change SE volume settings", 0, 10, 5),
        new SliderOption("Voice", "Change Voice volume settings", 0, 10, 5),
        new BoolOption("Voiced Line", "Play Voiced Lines in time with the text", true),
        new SelectionOption("Audio Language", "Change settings for audio language", new string[] {"English", "Japanese"}, 0),
        new SelectionOption("Sound Output Device", "Change sound output device", new string[] {"Default Device", "Realtek(R) Audio"}, 0)
    };

    private int _selectedOption = 0;

    public string Description => _options[_selectedOption].Description;

    public void Draw(ConfigMenuInfo* info, byte alpha)
    {
        // TODO implement

        // Draw option background and name (put this in a shared function, every option does this the same)
        float xStart = info->XStart;
        float yStart = info->YStart + 11 + 30;

        for(int i = 0; i < _options.Count; i++)
        {
            _options[i].Draw(xStart, yStart, alpha, info->CMainSpr, info->SelectedOption == i);
            yStart += 23;
        }
    }

    public void Process(ConfigMenu.ConfigMenuInfo* info)
    {
        _selectedOption = info->SelectedOption;
        // TODO implement
    }
}
