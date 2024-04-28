using Reloaded.Hooks.Definitions;
using System.Runtime.InteropServices;

using static p4g64.configFramework.Utils;

namespace p4g64.configFramework.UI.Common;

/// <summary>
/// A class for dealing with sprites in Spr files
/// </summary>
public unsafe class Spr
{
    public static DrawDelegate Draw;
    // TODO work out the atual difference and give this a proper name
    public static DrawWrappedDelegate DrawWrapped;

    public static void Initialise(IReloadedHooks hooks)
    {
        SigScan("48 8B C4 55 53 57 48 8D A8 ?? ?? ?? ?? 48 81 EC 40 03 00 00", "Spr::Draw", address =>
        {
            Draw = hooks.CreateWrapper<DrawDelegate>(address, out _);
        });

        SigScan("48 8B C4 48 89 58 ?? 57 48 81 EC B0 00 00 00 0F 29 70 ?? 48 8B F9 0F 29 78 ?? 48 8D 0D ?? ?? ?? ?? 44 0F 29 40 ?? 8B DA", "Spr::DrawWrapped", address =>
        {
            DrawWrapped = hooks.CreateWrapper<DrawWrappedDelegate>(address, out _);
        });
    }

    /// <summary>
    /// Gets the length of a sprite inside of <see cref="SpriteFile"/>
    /// </summary>
    /// <param name="spr">A pointer to the spr file</param>
    /// <param name="spriteIndex">The index of the sprite inside of the spr</param>
    /// <returns>The length of the sprite</returns>
    /// <remarks>This length is half of what the sprite file actually says as the game always does this scaling.</remarks>
    public float GetSpriteLength(SpriteFile* spr, int spriteIndex)
    {
        var sprite = spr->Entries[spriteIndex];
        int length = sprite.Length;
        if (length == 0)
            length = sprite.X2 - sprite.X1;

        return length / 2.0f;
    }

    public delegate void DrawDelegate(SpriteFile* sprFile, int spriteIndex, float param_3, float param_4, float param_5,
                byte colourR, byte colourG, byte colourB, byte colourA, float param_10,
                float param_11, float param_12, float param_13, float param_14);

    public delegate void DrawWrappedDelegate(SpriteFile* spr, int spriteIndex, float xPos, float yPos, float param_5, 
                byte colourR, byte colourG, byte colourB, byte colourA, float param_10, float param_11, float rotation);

    [StructLayout(LayoutKind.Explicit)]
    public struct SpriteFile
    {
        [FieldOffset(0x308)]
        public SpriteEntry* Entries;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct SpriteEntry
    {
        [FieldOffset(0x14)]
        public int TextureIndex;

        [FieldOffset(0x54)]
        public int X1;

        [FieldOffset(0x58)]
        public int Y1;

        [FieldOffset(0x5C)]
        public int X2;

        [FieldOffset(0x60)]
        public int Y2;

        [FieldOffset(0x74)]
        public short Length;

        [FieldOffset(0x76)]
        public short Height;
    }
}
