using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

using HarmonyLib;

namespace Psysight
{
    [HarmonyPatch(typeof(Pawn_HealthTracker), "CheckForStateChange")]
    public static class Patch_Pawn_HealthTracker_CheckForStateChange
    {
        [HarmonyPostfix]
        public static void Postfix(Pawn __instance)
        {
            if (!(__instance.mapIndexOrState > Find.Maps.Count))
            {
                PsysightUtil.AddPsysightIfNeeded(__instance);
            }
        }
    }
}
