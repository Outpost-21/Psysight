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
    [HarmonyPatch(typeof(PawnCapacityWorker_Sight), "CalculateCapacityLevel")]
    public static class Patch_PawnCapacityWorker_Sight_CalculateCapacityLevel
    {
        public static HediffSet curHediffSet;

        public static PawnCapacityDef capacity;

        [HarmonyPostfix]
        public static void Postfix(ref float __result, HediffSet diffSet)
        {
            curHediffSet = diffSet;
            if(diffSet.HasHediff(PsysightDefOf.Psysight))
            {
                __result += 0.01f;
            }
        }
    }
}
