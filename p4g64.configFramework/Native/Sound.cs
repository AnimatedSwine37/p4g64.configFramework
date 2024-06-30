using Reloaded.Hooks.Definitions;
using static p4g64.configFramework.Utils;

namespace p4g64.configFramework.Native;

public class Sound
{
    private static PlaySoundEffectDelegate _playSoundEffect;

    public static void Initialise(IReloadedHooks hooks)
    {
        SigScan("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 54 41 56 41 57 48 83 EC 20 48 63 E9",
            "PlaySoundEffect",
            address => { _playSoundEffect = hooks.CreateWrapper<PlaySoundEffectDelegate>(address, out _); });
    }

    /// <summary>
    /// Plays a sound effect
    /// </summary>
    /// <param name="soundEffect">The sound effect to play</param>
    public static void PlaySoundEffect(SoundEffect soundEffect)
    {
        // TODO don't do this, every SoundEffect should be mapped
        if (!_soundEffectParams.TryGetValue(soundEffect, out var sfxParams))
            return;

        _playSoundEffect(sfxParams.Category, sfxParams.SoundId);
    }

    public enum SoundEffect
    {
        Confirm,
        Back,
        Error,
        SelectionMoved,
        SelectionJumped,
        MenuOpened,
        MenuClosed,
    }

    private static readonly Dictionary<SoundEffect, SoundEffectParams> _soundEffectParams = new()
    {
        { SoundEffect.Confirm, new SoundEffectParams(SoundCategory.System, 0x2711) },
        { SoundEffect.Back, new SoundEffectParams(SoundCategory.System, 0x2712) },
        { SoundEffect.Error, new SoundEffectParams(SoundCategory.System, 0x2718) }
    };

    private struct SoundEffectParams
    {
        internal SoundCategory Category;

        internal int SoundId;

        internal SoundEffectParams(SoundCategory category, int soundId)
        {
            Category = category;
            SoundId = soundId;
        }
    }

    private delegate void PlaySoundEffectDelegate(SoundCategory category, int sound);

    private enum SoundCategory : int
    {
        System = 4
    }
}