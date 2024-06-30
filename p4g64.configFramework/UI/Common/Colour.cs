using System.Drawing;
using System.Runtime.InteropServices;

namespace p4g64.configFramework.UI.Common;

/// <summary>
/// A colour, used in structs in memory
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Colour
{
    public byte R;
    public byte G;
    public byte B;
    public byte A;
}

/// <summary>
/// A colour with the fields in reverse, used in arguments
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct RevColour
{
    public byte A;
    public byte B;
    public byte G;
    public byte R;
}