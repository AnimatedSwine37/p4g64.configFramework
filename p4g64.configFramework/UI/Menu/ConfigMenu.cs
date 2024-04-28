using p4g64.configFramework.UI.Common;
using p4g64.configFramework.UI.Menu.ConfigMenus;
using Reloaded.Hooks.Definitions;
using Reloaded.Memory;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static p4g64.configFramework.Native.Enums;
using static p4g64.configFramework.UI.Common.Spr;
using static p4g64.configFramework.Utils;

namespace p4g64.configFramework.UI.Menu;
internal unsafe class ConfigMenu
{
    private IReloadedHooks _hooks;

    private IHook<DrawDelegate> _drawHook;
    private IHook<ProcessDelegate> _processHook;
    private GetComponentFadeStateDelegate _getComponentFadeState;
    private CalculateComponentOpacityDelegate _calculateComponentOpacity;

    private readonly List<IMenu> _menus = new()
    {
        new AudioMenu(),
        new GameMenu(),
        new GraphicsMenu(),
        new DisplayMenu(),
        new KeyboardMenu(),
        new ControllerMenu()
    };

    internal ConfigMenu(IReloadedHooks hooks)
    {
        _hooks = hooks;
        
        Spr.Initialise(hooks);
        SigScan("4C 8B DC 55 56 57 41 57", "ConfigMenu::Draw", address =>
        {
            _drawHook = hooks.CreateHook<DrawDelegate>(Draw, address).Activate();
        });

        SigScan("48 8B C4 48 89 48 ?? 55 53 56 57 41 54 41 55 41 56 41 57 48 8D 68 ?? 48 81 EC D8 00 00 00", "ConfigMenu::Process", address =>
        {
            _processHook = hooks.CreateHook<ProcessDelegate>(ProcessFunc, address).Activate();
        });

        SigScan("40 53 48 83 EC 30 48 8B D9 0F 29 74 24 ?? 48 8D 0D ?? ?? ?? ?? 0F 28 F1 E8 ?? ?? ?? ?? 8B 03 A8 04 75 ?? 33 C0 0F 28 74 24 ?? 48 83 C4 30 5B C3 A8 01", "GetComponentFadeState", address =>
        {
            _getComponentFadeState = hooks.CreateWrapper<GetComponentFadeStateDelegate>(address, out _);
        });

        SigScan("48 89 5C 24 ?? 57 48 83 EC 40 0F 29 74 24 ?? 48 89 CB", "CalculateComponentOpacity", address =>
        {
            _calculateComponentOpacity = hooks.CreateWrapper<CalculateComponentOpacityDelegate>(address, out _);
        });
    }

    private nuint ProcessFunc(ConfigMenuState* state, int* param_2, ConfigMenuInfo* info)
    {
        // TODO implement
        _menus[(int)info->CurrentTab].Process(info);

        return _processHook.OriginalFunction(state, param_2, info);
    }

    private void Draw(ConfigMenuInfo* info)
    {
        var optionIndex = info->OptionOffset + info->SelectedOption;

        DrawTitle(info);
        DrawVisualiser(info);
        DrawDescription(info);
        DrawOptions(info);
        DrawButtonHints(info);
        DrawTabHeaders(info);
    }

    // Draw the word Config in the bottom left
    private void DrawTitle(ConfigMenuInfo* info)
    {
        if ((info->TitleComponent.field0 & 4) == 0)
            return;

        // Draw the title
        var alphaPercent = info->Alpha * 255.0f;
        var alpha = GetComponentAlpha(&info->TitleComponent, alphaPercent, 5, 10, 0, 5);
        var colour = new Colour { R = 0x5E, G = 0x37, B = 0xFF, A = 0xFF };

        Spr.Draw(info->CMainSpr, 0x112, info->XStart + 26.0f, info->YStart + 229.0f + 2.0f, 0.0f, colour.R, colour.G, colour.B, alpha, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f);
    }

    private void DrawVisualiser(ConfigMenuInfo* info)
    {
        if ((info->VisualiserComponent.field0 & 4) == 0)
            return;

        var alphaPercent = info->Alpha * 255.0f;
        var alpha = GetComponentAlpha(&info->VisualiserComponent, alphaPercent, 7, 10, 0, 3);

        // TODO draw the thing
    }

    private void DrawDescription(ConfigMenuInfo* info)
    {
        if ((info->DescriptionComponent.field0 & 4) == 0)
            return;

        var alphaPercent = info->Alpha * 255.0f;
        var alpha = GetComponentAlpha(&info->DescriptionComponent, alphaPercent, 5, 10, 0, 5);

        // TODO draw the thing
        var tab = info->CurrentTab;
        var menu = _menus[(int)tab];
        var description = menu.Description;

        var colour = new RevColour { R = 0xEC, G = 0x7C, B = 0, A = alpha };
        // TODO vary based on language like the game does, this code below doesn't seem to work even though I think it's equivalent
        // Search for /* Render the "Apply changed settings" text at the bottom */ in ConfigMenu::Draw
        //byte textSize = (byte)((int)Globals.Language - 5 < 4 ? 2 : 1);
        byte textSize = 1;
        Text.Draw(info->XStart + 467, info->YStart + 216, 0, colour, 0, textSize, description, Text.TextPositioning.Left);
    }

    private void DrawOptions(ConfigMenuInfo* info)
    {
        if ((info->OptionsComponent.field0 & 4) == 0)
            return;

        var alphaPercent = info->Alpha * 255.0f;
        var alpha = GetComponentAlpha(&info->OptionsComponent, alphaPercent, 5, 10, 0, 5);

        // TODO draw the thing
        _menus[(int)info->CurrentTab].Draw(info, alpha);
    }

    private void DrawButtonHints(ConfigMenuInfo* info)
    {
        if ((info->ButtonHintsComponent.field0 & 4) == 0)
            return;

        var alphaPercent = info->Alpha * 255.0f;
        var alpha = GetComponentAlpha(&info->ButtonHintsComponent, alphaPercent, 5, 10, 0, 5);

        // TODO draw the thing
    }

    private void DrawTabHeaders(ConfigMenuInfo* info)
    {
        if ((info->HeaderComponent.field0 & 4) == 0)
            return;

        var alphaPercent = info->Alpha * 255.0f;
        var alpha = GetComponentAlpha(&info->HeaderComponent, alphaPercent, 0, 5, 0, 5);

        // Draw the background of each tab


        // Draw the name of each tab
        float xPos = 29.0f;
        for (int i = 0; i < 6; i++)
        {
            var colour = i == (int)info->CurrentTab ?
                new Colour { R = 0x2D, G = 0x2D, B = 0x2D, A = 0xFF } :
                new Colour { R = 0xAD, G = 0xAD, B = 0xAD, A = 0xFF };

            Spr.Draw(info->CMainSpr, i + 0x2f3, xPos, 3, 0.0f, colour.R, colour.G, colour.B, alpha, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f);
            xPos += 70;
        };
    }


    /// <summary>
    /// Does all of the stuff necessary to get the alpha for a particular component
    /// </summary>
    /// <param name="component">The menu component</param>
    /// <param name="alphaPercent">The alpha percent gotten as info->Alpha / 255.0</param>
    /// <param name="fadeIn1">The second argument to CalculateComponentOpacity when fading in (TODO find out what ths means)</param>
    /// <param name="fadeIn2">The third argument to CalculateComponentOpacity when fading in (TODO find out what ths means)</param>
    /// <param name="fadeOut1">The second argument to CalculateComponentOpacity when fading out (TODO find out what ths means)</param>
    /// <param name="fadeOut2">The third argument to CalculateComponentOpacity when fading out (TODO find out what ths means)</param>
    /// <returns></returns>
    private byte GetComponentAlpha(MenuComponent* component, float alphaPercent, float fadeIn1, float fadeIn2, float fadeOut1, float fadeOut2)
    {
        var fadeState = _getComponentFadeState(component, 0.5f);
        uint alpha = 255;
        if (fadeState == MenuComponentFadeState.FadeIn)
        {
            var opacity = _calculateComponentOpacity(component, fadeIn1, fadeIn2, 1);
            alpha = (uint)(opacity * 255);
        }
        else if (fadeState == MenuComponentFadeState.FadeOut)
        {
            var opacity = _calculateComponentOpacity(component, fadeOut1, fadeOut2, 1);
            alpha = (uint)(1.0 - opacity * 255);
        }

        return (byte)(alphaPercent * alpha);
    }


    private delegate void DrawDelegate(ConfigMenuInfo* info);
    private delegate nuint ProcessDelegate(ConfigMenuState* state, int* param_2, ConfigMenuInfo* info);
    private delegate MenuComponentFadeState GetComponentFadeStateDelegate(MenuComponent* component, float param_2);
    private delegate float CalculateComponentOpacityDelegate(MenuComponent* component, float param_2, float param_3, int param_4);

    /// <summary>
    /// The index of sprites inside of c_main01x2.spr
    /// </summary>
    private enum CampSprite : int
    {
        Common = 0x30e,
        FieldDungeon = 0x30f,
        Battle = 0x310,
        Event = 0x311,
        FiveDash = 0x313,
        FourDashes = 0x311
    }

    private enum ConfigMenuState : int
    {
        Browsing = 4,
        SettingKey = 7,
        KeyAlreadyAssigneed = 0x10,
        SettingsSaved = 0x11,
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct ConfigMenuInfo
    {
        [FieldOffset(0)]
        internal byte Alpha;

        [FieldOffset(4)]
        internal float XStart;

        [FieldOffset(8)]
        internal float YStart;

        [FieldOffset(0x14)]
        internal int field0x14;

        [FieldOffset(0x30)]
        internal int SelectedOption;

        [FieldOffset(0x34)]
        internal int OptionOffset;

        [FieldOffset(0x38)]
        internal ConfigTab CurrentTab;

        [FieldOffset(0x1b8)]
        internal SpriteFile* CMainSpr;

        [FieldOffset(0x1f0)]
        internal MenuComponent TitleComponent;

        [FieldOffset(0x1d8)]
        internal MenuComponent VisualiserComponent;

        [FieldOffset(0x208)]
        internal MenuComponent DescriptionComponent;

        [FieldOffset(0x1c0)]
        internal MenuComponent OptionsComponent;

        [FieldOffset(0x220)]
        internal MenuComponent ButtonHintsComponent;

        [FieldOffset(0x2b0)]
        internal MenuComponent HeaderComponent;
    }

    internal struct MenuComponent
    {
        internal uint field0;
        internal float field1;
        internal float field2;
        internal float field3;
    }

    internal enum MenuComponentFadeState : int
    {
        Hidden,
        FadeIn,
        FadeOut,
        Static
    }

    internal enum ConfigTab : int
    {
        Audio,
        Game,
        Graphics,
        Display,
        Keyboard,
        Controller
    }
}