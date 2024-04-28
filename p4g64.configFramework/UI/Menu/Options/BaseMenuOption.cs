using p4g64.configFramework.UI.Common;
using static p4g64.configFramework.UI.Common.Spr;
using static p4g64.configFramework.UI.Common.Text;

namespace p4g64.configFramework.UI.Menu.Options;

/// <summary>
/// An abstract <see cref="IMenuOption"/> that other menu options can inherit from.
/// This provides functions for drawing standard parts of an option.
/// </summary>
public abstract unsafe class BaseMenuOption : IMenuOption
{
    public abstract string Name { get; }

    public abstract string Description { get; }

    public abstract void Draw(float xPos, float yPos, byte alpha, SpriteFile* cMainSpr, bool isSelected);
    public abstract void KeyPressed(int key);

    // Draw a standard background for the option
    protected void DrawOptionBg(SpriteFile* cMainSpr, byte alpha, float xPos, float yPos, bool isSelected)
    {
        Colour colour;
        if (!isSelected)
            colour = new Colour { R = 0xFF, G = 0xFF, B = 0x81, A = 0xFF };
        else
            colour = new Colour { R = 0x2D, G = 0x2D, B = 0x2D, A = 0xFF };

        Spr.Draw(cMainSpr, 0x205, xPos + 148.0f, yPos, 0.0f, colour.R, colour.G, colour.B, alpha, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f);
        Spr.Draw(cMainSpr, 0x202, xPos + 459.0f, yPos, 0.0f, colour.R, colour.G, colour.B, alpha, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f);
    }

    // Draw the name of the option
    protected void DrawOptionName(byte alpha, float xPos, float yPos, bool isSelected)
    {
        RevColour colour;
        if(isSelected)
            colour = new RevColour { R = 0xFF, G = 0xFF, B = 0xFF, A = alpha };
        else
            colour = new RevColour { R = 0x2D, G = 0x2D, B = 0x2D, A = alpha };

        var width = Text.GetWidth(1000.0f, 1000.0f, 0, colour, 0, 2, Name, 1 );

        // Scale the text (from 0 to 5, 5 being smallest)
        byte textSize = 4;
        if (width < 0x169)
        {
            textSize = 2;
            if (300 < width)
            {
                textSize = 3;
            }
        }

        Text.Draw(xPos + 309.0f, yPos + 8, 0, colour, 0, textSize, Name, TextPositioning.Left);
    }

}
