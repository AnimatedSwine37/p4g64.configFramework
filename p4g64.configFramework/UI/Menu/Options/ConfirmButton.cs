using p4g64.configFramework.UI.Common;

namespace p4g64.configFramework.UI.Menu.Options;
public class ConfirmButton : BaseMenuOption
{
    public override string Name => "Confirm";

    public override string Description => "Apply changed settings";

    public override unsafe void Draw(float xPos, float yPos, byte alpha, Spr.SpriteFile* cMainSpr, bool isSelected)
    {
        DrawOptionBg(cMainSpr, alpha, xPos, yPos, isSelected);
        DrawOptionName(alpha, xPos, yPos, isSelected);

        // TODO implement
    }

    public override void KeyPressed(int key)
    {
        // TODO implement
    }
}
