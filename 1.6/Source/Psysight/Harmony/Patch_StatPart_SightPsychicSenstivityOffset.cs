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
    [HarmonyPatch(typeof(StatPart_SightPsychicSensitivityOffset), "TryGetPsychicOffset")]
    public static class Patch_StatPart_SightPsychicSenstivityOffset_TryGetPsychicOffset
    {
        [HarmonyPostfix]
        public static void Postfix(StatPart_SightPsychicSensitivityOffset __instance, ref bool __result, Thing t, out float offset)
        {
            Pawn pawn;
            if ((pawn = (t as Pawn)) != null && pawn.health.hediffSet.HasHediff(PsysightDefOf.Psysight))
            {
                float level = pawn.health.capacities.GetLevel(PawnCapacityDefOf.Sight) - (pawn.health.hediffSet.GetFirstHediffOfDef(PsysightDefOf.Psysight).CurStage.capMods.First(cm => cm.capacity == PawnCapacityDefOf.Sight).offset + 0.01f);
                if (level <= __instance.startsAt)
                {
                    offset = GenMath.LerpDoubleClamped(__instance.startsAt, __instance.endsAt, __instance.minBonus, __instance.maxBonus, level);
                    __result = offset >= 0.01f;
                    return;
                }
            }
            offset = 0;
            __result = false;
            return;

        }
    }
}
