using static p4g64.configFramework.UI.Common.Spr;

namespace p4g64.configFramework.UI.Menu.Options;
public unsafe class SliderOption : BaseMenuOption
{
    public override string Name { get; }

    public override string Description { get; }

    public int Value { get; set; }

    public int MinValue { get; set; }

    public int MaxValue { get; set; }

    public SliderOption(string name, string description, int minValue, int maxValue, int value)
    {
        Name = name;
        Description = description;
        Value = value;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public override void Draw(float xPos, float yPos, byte alpha, SpriteFile* cMainSpr, bool isSelected)
    {
        // TODO implement
    }

    public override void KeyPressed(int key)
    {
        // TODO implement
    }
}
