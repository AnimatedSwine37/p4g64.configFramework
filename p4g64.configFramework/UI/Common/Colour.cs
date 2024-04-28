using System.Drawing;
using System.Runtime.InteropServices;

namespace p4g64.configFramework.UI.Common;

/// <summary>
/// A colour, used in structs in memory
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct Colour
{
    internal byte R;
    internal byte G;
    internal byte B;
    internal byte A;
}

/// <summary>
/// A colour with the fields in reverse, used in arguments
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct RevColour
{
    internal byte A;
    internal byte B;
    internal byte G;
    internal byte R;
}