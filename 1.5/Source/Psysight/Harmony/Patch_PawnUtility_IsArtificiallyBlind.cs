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
    [HarmonyPatch(typeof(PawnUtility), "IsArtificiallyBlind")]
    public static class Patch_PawnUtility_IsArtificiallyBlind
    {
        [HarmonyPostfix]
        public static void Postfix(ref bool __result, Pawn p)
        {
            if (p.health.hediffSet.HasHediff(PsysightDefOf.Psysight))
            {
                __result = false;
            }
        }
    }
}
