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
    [HarmonyPatch(typeof(PawnUtility), "IsBiologicallyBlind")]
    public static class Patch_PawnUtility_IsBiologicallyBlind
    {
        [HarmonyPostfix]
        public static void Postfix(ref bool __result, Pawn pawn)
        {
            if (pawn.health.hediffSet.HasHediff(PsysightDefOf.Psysight))
            {
                __result = true;
            }
        }
    }
}
