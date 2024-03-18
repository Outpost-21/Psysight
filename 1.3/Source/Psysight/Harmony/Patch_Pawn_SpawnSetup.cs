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
    [HarmonyPatch(typeof(Pawn), "SpawnSetup")]
    public static class Patch_Pawn_SpawnSetup
    {
        [HarmonyPostfix]
        public static void Postfix(Pawn __instance)
        {
            if (!(__instance.mapIndexOrState > Find.Maps.Count) && !(__instance.mapIndexOrState < 0))
            {
                PsysightUtil.AddPsysightIfNeeded(__instance);
            }
        }
    }
}
