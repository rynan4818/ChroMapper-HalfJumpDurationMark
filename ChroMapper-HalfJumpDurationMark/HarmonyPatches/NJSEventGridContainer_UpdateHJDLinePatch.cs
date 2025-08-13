using System;
using HarmonyLib;

namespace ChroMapper_HalfJumpDurationMark.HarmonyPatches
{
    [HarmonyPatch(typeof(NJSEventGridContainer), nameof(NJSEventGridContainer.UpdateHJDLine))]
    public class NJSEventGridContainer_UpdateHJDLinePatch
    {
        public static event Action<float> OnUpdateHJDLine;
        public static void Postfix(float ___currentNJS)
        {
            OnUpdateHJDLine?.Invoke(___currentNJS);
        }
    }
}
