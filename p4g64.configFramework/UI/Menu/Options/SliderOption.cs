using p4g64.configFramework.UI.Common;
using static p4g64.configFramework.UI.Common.Spr;
using static p4g64.configFramework.Native.Inputs;
using static p4g64.configFramework.Native.Sound;

namespace p4g64.configFramework.UI.Menu.Options;

public unsafe class SliderOption : BaseMenuOption
{
    public override string Name { get; }

    public override string Description { get; }

    public override bool Readonly { get; }

    public override bool StaySelected => false;

    public int Value { get; set; }

    public int MinValue { get; set; }

    public int MaxValue { get; set; }

    public int StepSize { get; set; }

    /// <summary>
    /// Creates a new slider option
    /// </summary>
    /// <param name="name">The name of the option</param>
    /// <param name="description">The description of the option</param>
    /// <param name="minValue">The minimum value of the slider</param>
    /// <param name="maxValue">The maximum value of the slider</param>
    /// <param name="initialValue">The starting value of the slider</param>
    /// <param name="stepSize">The number to add or subtract from the current value when the user presses left or right</param>
    /// <param name="isReadonly">True if the user cannot change the value, false otherwise</param>
    public SliderOption(string name, string description, int minValue, int maxValue, int initialValue, int stepSize,
        bool isReadonly = false)
    {
        Name = name;
        Description = description;
        Readonly = isReadonly;
        Value = initialValue;
        MinValue = minValue;
        MaxValue = maxValue;
        StepSize = stepSize;
    }

    // Note that I could just use the existing function that does this but it doesn't support a minimum value as far as I can tell
    // Also where's the fun in using something existing, it would be cool to do it myself
    public override void Draw(float xPos, float yPos, byte alpha, SpriteFile* cMainSpr, bool isSelected)
    {
        DrawOptionBg(cMainSpr, alpha, xPos, yPos, isSelected);
        DrawOptionName(alpha, xPos, yPos, isSelected);

        // TODO Draw the actual slider
        Rectangle rect = new Rectangle { X1 = xPos + 4, X2 = 84, Y1 = yPos + 4, Y2 = 5 };
        Colour colour = new Colour { R = 0x2D, G = 0x2D, B = 0x2D, A = alpha };
        //Spr.DrawRectangle(&colour, &rect, 0);

        // Draw the value
        RevColour textColour;
        if (isSelected)
            textColour = new RevColour { R = 0xFF, G = 0xFF, B = 0xFF, A = alpha };
        else
            textColour = new RevColour { R = 0x2D, G = 0x2D, B = 0x2D, A = alpha };

        Text.Draw(xPos + 443, yPos + 7, 0, textColour, 0, 2, Value.ToString(), Text.TextPositioning.Right);
    }

    public override void Process()
    {
        if (Value < MaxValue && IsHeld(Input.Right))
        {
            Value += StepSize;
            PlaySoundEffect(SoundEffect.SelectionMoved);
        }
        
        if (Value > MinValue && IsHeld(Input.Left))
        {
            Value -= StepSize;
            PlaySoundEffect(SoundEffect.SelectionMoved);
        }
    }
}
