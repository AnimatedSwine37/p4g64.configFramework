using p4g64.configFramework.Native;
using p4g64.configFramework.UI.Common;
using static p4g64.configFramework.Native.Inputs;
using static p4g64.configFramework.Native.Sound;

namespace p4g64.configFramework.UI.Menu.Options;

/// <summary>
/// A class that contains information about a list of menu options that will be displayed in a menu
/// </summary>
public class MenuOptionList
{
    /// <summary>
    /// The list of menu options
    /// </summary>
    public List<IMenuOption> Options { get; }

    /// <summary>
    /// The index of the currently selected option
    /// </summary>
    private int _selectedIndex = 0;

    /// <summary>
    /// The index of the currently selected option in the displayed options
    /// </summary>
    private int _displayIndex = 0;

    /// <summary>
    /// The currently selected option in the list
    /// </summary>
    public IMenuOption SelectedOption
    {
        get => Options[_selectedIndex];
        set => _selectedIndex = Options.IndexOf(value);
    }

    /// <summary>
    /// The number of menu options to display at a time.
    /// If the total number of options is greater than this the list will be scrollable
    /// </summary>
    public int NumDisplayed { get; }

    /// <summary>
    /// Creates a new list of menu options
    /// </summary>
    /// <param name="options">The options to include in the list</param>
    /// <param name="numDisplayed">The maximum number of options to display at a given time.
    ///                            If the number of options is greater than this the list will be scrollable</param>
    public MenuOptionList(List<IMenuOption> options, int numDisplayed)
    {
        Options = options;
        NumDisplayed = numDisplayed;
    }

    /// <summary>
    /// Draws the list of menu options
    /// </summary>
    /// <param name="xStart">The X Position to start drawing at</param>
    /// <param name="yStart">The Y Position to start drawing at</param>
    /// <param name="alpha">The alpha value to draw options with</param>
    /// <param name="cMainSpr">A pointer to c_main01x2.spr</param>
    public unsafe void DrawOptions(float xStart, float yStart, byte alpha, Spr.SpriteFile* cMainSpr)
    {
        int startIndex = _selectedIndex - _displayIndex;

        for (int i = startIndex; i < Math.Min(startIndex + NumDisplayed, Options.Count); i++)
        {
            Options[i].Draw(xStart, yStart, alpha, cMainSpr, _selectedIndex == i);
            yStart += 23;
        }
    }

    /// <summary>
    /// Processes any inputs the user has made.
    /// If applicable these will be passed on to the selected option.
    /// </summary>
    public void Process()
    {
        if(!SelectedOption.StaySelected)
            TryChangeSelection();

        SelectedOption.Process();        
    }

    /// <summary>
    /// Handles the user moving between selected options
    /// </summary>
    private void TryChangeSelection()
    {
        if (IsHeld(Input.Up) && Options.Count > 1)
        {
            if (_selectedIndex > 0)
            {
                PlaySoundEffect(SoundEffect.SelectionMoved);
                // If we can see the first skill or the display is the bottom 2 move it up
                if (_displayIndex != 0 && (_displayIndex > 2 || _selectedIndex - _displayIndex == 0))
                    _displayIndex--;
                _selectedIndex--;
            }
            else
            {
                // Only wrap around if you press the button, not holding it
                if (IsPressed(Input.Up))
                {
                    PlaySoundEffect(SoundEffect.SelectionMoved);
                    _selectedIndex = Options.Count - 1;
                    _displayIndex = NumDisplayed - 1;
                    if (Options.Count < NumDisplayed)
                        _displayIndex = Options.Count - 1;
                }
            }
        }

        if (IsHeld(Input.Down) && Options.Count > 1)
        {
            if (_selectedIndex < Options.Count - 1)
            {
                PlaySoundEffect(SoundEffect.SelectionMoved);
                // If we can see the last skill or the display is the top 2, move it down
                if (_displayIndex != NumDisplayed - 1 && (_displayIndex < 2 || _selectedIndex - _displayIndex >= Options.Count - NumDisplayed))
                    _displayIndex++;
                _selectedIndex++;
            }
            else
            {
                // Only wrap around if you press the button, not holding it
                if (IsPressed(Input.Down))
                {
                    PlaySoundEffect(SoundEffect.SelectionMoved);
                    _selectedIndex = 0;
                    _displayIndex = 0;
                }
            }
        }
    }
}