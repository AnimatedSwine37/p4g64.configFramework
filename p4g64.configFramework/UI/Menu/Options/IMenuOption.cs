using p4g64.configFramework.Native;
using static p4g64.configFramework.UI.Common.Spr;

namespace p4g64.configFramework.UI.Menu.Options;

/// <summary>
/// An option that you can change inside of a config menu
/// </summary>
public unsafe interface IMenuOption
{
    /// <summary>
    /// The name of the menu option
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// A description of the option's purpose
    /// </summary>
    public string Description { get; }
    
    /// <summary>
    /// If true the option cannot be edited by the user
    /// </summary>
    public bool Readonly { get; }

    /// <summary>
    /// If true the user will not be able to switch from this option by pressing up, down, or hovering over another one.
    /// Most options should just return false, if you are using this make sure there is some condition where it will return
    /// false otherwise you will softlock the user.
    /// </summary>
    public bool StaySelected { get; }

    /// <summary>
    /// Draws the menu option
    /// </summary>
    /// <param name="xPos">The x position to draw at</param>
    /// <param name="yPos">The y position to draw at</param>
    /// <param name="alpha">The alpha of the option</param>
    /// <param name="cMainSpr">A pointer to c_main01x2.spr</param>
    /// <param name="isSelected">Whether this is the selected option</param>
    public void Draw(float xPos, float yPos, byte alpha, SpriteFile* cMainSpr, bool isSelected);

    // TODO Add something for drawing button prompts like how the audio ones have the extra Play option
    // Maybe a list of prompt objects with draw methods?

    
    /// <summary>
    /// Processes inputs made by the user while the option is selected
    /// </summary>
    public void Process();
}
