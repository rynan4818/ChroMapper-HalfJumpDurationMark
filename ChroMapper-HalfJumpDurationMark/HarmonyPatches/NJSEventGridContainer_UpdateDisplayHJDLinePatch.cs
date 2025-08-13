using System;
using HarmonyLib;

namespace ChroMapper_HalfJumpDurationMark.HarmonyPatches
{
    [HarmonyPatch(typeof(NJSEventGridContainer), "UpdateDisplayHJDLine")]
    public class NJSEventGridContainer_UpdateDisplayHJDLinePatch
    {
        public static event Action<bool> OnUpdateDisplayHJDLine;
        public static void Postfix(object value)
        {
            OnUpdateDisplayHJDLine?.Invoke((bool)value);
        }
    }
}
