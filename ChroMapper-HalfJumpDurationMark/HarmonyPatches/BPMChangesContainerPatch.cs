using System;
using HarmonyLib;

namespace ChroMapper_HalfJumpDurationMark.HarmonyPatches
{
    [HarmonyPatch(typeof(BPMChangesContainer), "RefreshModifiedBeat")]
    public class BPMChangesContainerPatch
    {
        public static event Action OnRefreshModifiedBeat;
        public static void Postfix()
        {
            OnRefreshModifiedBeat?.Invoke();
        }
    }
}
