using System.Runtime.InteropServices;

namespace p4g64.configFramework.Native;

using static Utils;

public unsafe class Inputs
{
    private static InputStruct* _inputs;
    private static readonly Dictionary<Input, DateTime> _timePressed = new();

    private static readonly TimeSpan _cooldown = TimeSpan.FromMilliseconds(100);
    private static readonly TimeSpan _initialDelay = TimeSpan.FromMilliseconds(230);

    public static void Initialise()
    {
        SigScan("F7 05 ?? ?? ?? ?? 00 40 00 00 74 ?? 44 8B EB", "InputsPtr",
            address => { _inputs = (InputStruct*)GetGlobalAddress(address + 2); });
    }

    /// <summary>
    /// Checks whether an input is currently being held
    /// </summary>
    /// <param name="input">The input to check for</param>
    /// <returns>True if the button is currently being held, false otherwise</returns>
    public static bool IsHeld(Input input)
    {
        InputFlag flag = (InputFlag)(1 << (int)input);
        if (_inputs->Pressed.HasFlag(flag) || _inputs->ThumbstickPressed.HasFlag(flag))
        {
            if (_timePressed.ContainsKey(input))
                _timePressed[input] = DateTime.Now + _initialDelay;
            else
                _timePressed.Add(input, DateTime.Now + _initialDelay);
            return true;
        }

        if (_inputs->Held.HasFlag(flag) || _inputs->ThumbstickHeld.HasFlag(flag))
        {
            if (!_timePressed.TryGetValue(input, out var timePressed))
            {
                _timePressed.Add(input, DateTime.Now + _initialDelay);
                timePressed = DateTime.Now + _initialDelay;
            }

            if (DateTime.Now >= timePressed + _cooldown)
            {
                _timePressed[input] = DateTime.Now;
                return true;
            }

            return false;
        }

        _timePressed.Remove(input);
        return false;
    }

    /// <summary>
    /// Checks whether an input has been pressed.
    /// This is only true the instant it is pressed, not when held.
    /// </summary>
    /// <param name="input">The input to check for</param>
    /// <returns>True if the input was just pressed, false otherwise</returns>
    public static bool IsPressed(Input input)
    {
        return input switch
        {
            Input.Up => _inputs->Pressed.HasFlag(InputFlag.Up) ||
                        _inputs->ThumbstickPressed.HasFlag(InputFlag.Up),
            Input.Down => _inputs->Pressed.HasFlag(InputFlag.Down) ||
                          _inputs->ThumbstickPressed.HasFlag(InputFlag.Down),
            Input.Left => _inputs->Pressed.HasFlag(InputFlag.Left) ||
                          _inputs->ThumbstickPressed.HasFlag(InputFlag.Left),
            Input.Right => _inputs->Pressed.HasFlag(InputFlag.Right) ||
                           _inputs->ThumbstickPressed.HasFlag(InputFlag.Right),
            _ => _inputs->Pressed.HasFlag(input)
        };
    }

    [Flags]
    public enum InputFlag
    {
        Start = 1 << Input.Start,
        Up = 1 << Input.Up,
        Right = 1 << Input.Right,
        Down = 1 << Input.Down,
        Left = 1 << Input.Left,
        Confirm = 1 << Input.Confirm,
        Escape = 1 << Input.Escape,
        SubMenu = 1 << Input.SubMenu,
    }

    public enum Input
    {
        Start = 3,
        Up = 4,
        Right = 5,
        Down = 6,
        Left = 7,
        Confirm = 13,
        Escape = 14,
        SubMenu = 15,
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputStruct
    {
        [FieldOffset(4)] public InputFlag Pressed;

        [FieldOffset(12)] public InputFlag Held;

        [FieldOffset(20)] public InputFlag ThumbstickHeld;

        [FieldOffset(24)] public InputFlag ThumbstickPressed;
    }
}