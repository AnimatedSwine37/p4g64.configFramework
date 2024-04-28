using Reloaded.Hooks.Definitions;
using static p4g64.configFramework.Utils;

namespace p4g64.configFramework.UI.Common;
internal static unsafe class Text
{
    internal static DrawTextDelegate Draw;
    internal static GetWidthDelegate GetWidth;

    internal static void Initialise(IReloadedHooks hooks)
    {
        SigScan("E8 ?? ?? ?? ?? 45 33 FF 4C 8D 2D ?? ?? ?? ?? EB ??", "Text::Draw Ptr", address =>
        {
            var funcAddr = GetGlobalAddress(address + 1);
            Draw = hooks.CreateWrapper<DrawTextDelegate>((long)funcAddr, out _);
        });

        SigScan("E8 ?? ?? ?? ?? 8B C8 3D 68 01 00 00", "Text::GetWidth Ptr", address =>
        {
            var funcAddr = GetGlobalAddress(address + 1);
            GetWidth = hooks.CreateWrapper<GetWidthDelegate>((long)funcAddr, out _);
        });
    }

    internal delegate void DrawTextDelegate(float xPos, float yPos, nuint param_3, RevColour colour, byte param_5, byte textSize, string text, TextPositioning position);

    internal delegate int GetWidthDelegate(float xPos, float yPos, float param_3, RevColour colour, byte param_5, byte textSize, string text, int position);

    internal enum TextPositioning : int
    {
        Right = 0,
        Left = 2,
        Center = 8
    }
}
