using p4g64.configFramework.UI.Common;
using static p4g64.configFramework.UI.Common.Spr;
using static p4g64.configFramework.Native.Inputs;

namespace p4g64.configFramework.UI.Menu.Options;
public unsafe class BoolOption : BaseMenuOption
{
    public override string Name { get; }

    public override string Description { get; }

    public override bool Readonly { get; }

    public override bool StaySelected => false;
    
    public bool Value { get; set; }
    

    public BoolOption(string name, string description, bool value, bool isReadonly = false)
    {
        Name = name;
        Description = description;
        Value = value;
        Readonly = isReadonly;
    }

    public override void Draw(float xPos, float yPos, byte alpha, SpriteFile* cMainSpr, bool isSelected)
    {
        DrawOptionBg(cMainSpr, alpha, xPos, yPos, isSelected);
        DrawOptionName(alpha, xPos, yPos, isSelected);


        if (isSelected)
        {
            // Draw the two buttons around the option
            Spr.DrawWrapped(cMainSpr, 0xa4, xPos + 338.0f - 12.0f, yPos + 7, 0.0f, 0xd7, 0xff, 0xb, alpha, 0.85f, 0.85f, -90.0f);
            Spr.DrawWrapped(cMainSpr, 0xa4, xPos + 338.0f + 104.0f, yPos + 7, 0.0f, 0xd7, 0xff, 0xb, alpha, 0.85f, 0.85f, 90.0f);

            // Draw the background of the value
            Spr.Draw(cMainSpr, 0x203, xPos + 338.0f, yPos + 2, 0.0f, 0xd7, 0xff, 0xb, alpha, 1.0f, 0.85f, 0.0f, 0.0f, 0.0f);
            Spr.Draw(cMainSpr, 0x202, xPos + 338.0f + 104.0f, yPos + 2, 0.0f, 0xd7, 0xff, 0xb, alpha, 1.0f, 0.85f, 0.0f, 0.0f, 0.0f);
        }

        // Draw ON or OFF
        var text = Value ? "ON" : "OFF";
        RevColour textColour = new RevColour { R = 0x2D, G = 0x2D, B = 0x2D, A = alpha };
        Text.Draw(xPos + 393.0f, yPos + 6, 0, textColour, 0, 1, text, Text.TextPositioning.Center);
    }
    
    public override void Process()
    {
        if (IsPressed(Input.Left) || IsPressed(Input.Right))
            Value = !Value;
    }
}
