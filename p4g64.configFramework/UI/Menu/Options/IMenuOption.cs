using static p4g64.configFramework.UI.Common.Spr;

namespace p4g64.configFramework.UI.Menu.Options;

/// <summary>
/// An option that you can change inside of a config menu
/// </summary>
public unsafe interface IMenuOption
{
    public string Name { get; }

    public string Description { get; }

    public void Draw(float xPos, float yPos, byte alpha, SpriteFile* cMainSpr, bool isSelected);

    // TODO Add something for drawing button prompts like how the audio ones have the extra Play option
    // Maybe a list of prompt objects with draw methods?

    public void KeyPressed(int key);
}
