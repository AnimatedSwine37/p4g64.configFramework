using static p4g64.configFramework.Native.Enums;
using static p4g64.configFramework.Utils;

namespace p4g64.configFramework.UI.Common;
internal unsafe class Globals
{
    private static Language* _language;
    internal static Language Language => *_language;

    internal static void Initialise()
    {
        SigScan("8B 05 ?? ?? ?? ?? 83 C0 FB 83 F8 03", "LanguagePtr", address =>
        {
            _language = (Language*)GetGlobalAddress(address + 2);
        });
    }
}
