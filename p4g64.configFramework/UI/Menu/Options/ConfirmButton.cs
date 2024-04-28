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

        if (isSelected)
        {
            // Draw the background of the button
            Spr.Draw(cMainSpr, 0x203, xPos + 338.0f, yPos + 2, 0.0f, 0xd7, 0xff, 0xb, alpha, 1.0f, 0.85f, 0.0f, 0.0f, 0.0f);
            Spr.Draw(cMainSpr, 0x202, xPos + 338.0f + 104.0f, yPos + 2, 0.0f, 0xd7, 0xff, 0xb, alpha, 1.0f, 0.85f, 0.0f, 0.0f, 0.0f);
        }

        // Draw OK
        RevColour textColour = new RevColour { R = 0x2D, G = 0x2D, B = 0x2D, A = alpha };
        Text.Draw(xPos + 393.0f, yPos + 6, 0, textColour, 0, 1, "OK", Text.TextPositioning.Center);
    }

    public override void KeyPressed(int key)
    {
        // TODO implement
    }
}
