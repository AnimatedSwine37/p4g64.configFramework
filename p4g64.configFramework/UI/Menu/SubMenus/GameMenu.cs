using p4g64.configFramework.UI.Menu.Options;

namespace p4g64.configFramework.UI.Menu.ConfigMenus;
internal unsafe class GameMenu : IMenu
{
    private MenuOptionList _options;
    
    public string Description => _options.SelectedOption.Description;

    public GameMenu()
    {
        List<IMenuOption> options = new()
        {
            new ConfirmButton(),
            new BoolOption("Auto Text", "Automatically progress the text in time with the voice audio", true),
            new BoolOption("Cursor Position Memory", "Remember the cursor's last position during battle", true),
            new BoolOption("Battle Order Memory", "Remember battle orders of previously selected during battle", true),
            new BoolOption("Equipped Persona Memory", "Remember the Persona that was equipped at the end of battle", true),
            new BoolOption("Program Guide Notification", "Automatically notify when the Program Guide is updated", true),
            new BoolOption("Inverted Camera", "Invert camera controls", true),
            new SliderOption("Camera Speed", "Change the camera rotation speed while moving", 1, 5, 3, 1),
            new BoolOption("Network Function", "Activates functions which use a network connection", true),
            new BoolOption("Anime Subtitles", "Change settings for Subtitles during anime cutscenes", true)
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
