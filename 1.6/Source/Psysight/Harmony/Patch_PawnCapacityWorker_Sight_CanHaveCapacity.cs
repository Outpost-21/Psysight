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

    [HarmonyPatch(typeof(PawnCapacityWorker_Sight), "CanHaveCapacity")]
    public static class Patch_PawnCapacityWorker_Sight_CanHaveCapacity
    {
        [HarmonyPostfix]
        public static void Postfix(PawnCapacityWorker_Sight __instance, ref bool __result)
        {
            HediffSet diffSet = Patch_PawnCapacityWorker_Sight_CalculateCapacityLevel.curHediffSet;
            if (diffSet.HasHediff(PsysightDefOf.Psysight))
            {
                __result = true;
            }
        }
    }
}
