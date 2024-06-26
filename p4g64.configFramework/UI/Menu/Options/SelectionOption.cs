﻿using p4g64.configFramework.UI.Common;
using static p4g64.configFramework.UI.Common.Spr;
using static p4g64.configFramework.Native.Inputs;
using static p4g64.configFramework.Native.Sound;

namespace p4g64.configFramework.UI.Menu.Options;

/// <summary>
/// An option where you can select between a number of different strings
/// </summary>
public unsafe class SelectionOption : BaseMenuOption
{
    public override string Name { get; }

    public override string Description { get; }
    
    public override bool Readonly { get; }

    public override bool StaySelected => false;
    
    public string[] Options { get; }

    private int _selectedIndex;

    public SelectionOption(string name, string description, string[] options, string initialValue, bool isReadonly = false)
    {
        Name = name;
        Description = description;
        Readonly = isReadonly;
        Options = options;
        _selectedIndex = Array.IndexOf(options, initialValue);
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

        DrawOptionValue(xPos, yPos, alpha);
    }

    private void DrawOptionValue(float xPos, float yPos, byte alpha)
    {
        RevColour textColour = new RevColour { R = 0x2D, G = 0x2D, B = 0x2D, A = alpha };

        var option = Options[_selectedIndex];

        var optionWidth = Text.GetWidth(1000.0f, 1000.0f, 0, textColour, 0, 1, option, 1);
        byte textSize = 1;
        if (optionWidth < 0xfb)
        {
            if (optionWidth >= 0xc4)
                textSize = 3;
        }
        else
        {
            textSize = 4;
        }

        float yOffset = 0;
        // Small text gets moved down slightly extra
        if (textSize >= 3)
            yOffset = 2.5f;

        Text.Draw(xPos + 393.0f, yPos + 6 + yOffset, 0, textColour, 0, textSize, option, Text.TextPositioning.Center);
    }
    
    public override void Process()
    {
        if (IsHeld(Input.Left) && Options.Length > 1)
        {
            if (_selectedIndex > 0)
            {
                PlaySoundEffect(SoundEffect.SelectionMoved);
                _selectedIndex--;
            }
            else
            {
                // Only wrap around if you press the button, not holding it
                if (IsPressed(Input.Left))
                {
                    PlaySoundEffect(SoundEffect.SelectionMoved);
                    _selectedIndex = Options.Length - 1;
                }
            }
        }

        if (IsHeld(Input.Right) && Options.Length > 1)
        {
            if (_selectedIndex < Options.Length - 1)
            {
                PlaySoundEffect(SoundEffect.SelectionMoved);
                _selectedIndex++;
            }
            else
            {
                // Only wrap around if you press the button, not holding it
                if (IsPressed(Input.Right))
                {
                    PlaySoundEffect(SoundEffect.SelectionMoved);
                    _selectedIndex = 0;
                }
            }
        }

    }
}
