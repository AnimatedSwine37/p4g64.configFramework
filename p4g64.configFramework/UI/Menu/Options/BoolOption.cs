using static p4g64.configFramework.UI.Common.Spr;

namespace p4g64.configFramework.UI.Menu.Options;
public unsafe class BoolOption : BaseMenuOption
{
    public override string Name { get; }

    public override string Description { get; }

    // TODO decide whether value should be publicly available and if so how to do that.
    // We can't do a list of IMenuOption if it is a generic it seems
    public bool Value { get; set; }

    public BoolOption(string name, string description, bool value)
    {
        Name = name;
        Description = description;
        Value = value;
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
