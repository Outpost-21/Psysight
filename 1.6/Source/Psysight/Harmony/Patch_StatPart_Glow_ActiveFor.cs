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
    [HarmonyPatch(typeof(StatPart_Glow), "ActiveFor")]
    public static class Patch_StatPart_Glow_ActiveFor
    {
        [HarmonyPostfix]
        public static void Postfix(ref bool __result, ref bool ___humanlikeOnly, Thing t)
        {
            Pawn pawn;
            if ((pawn = (t as Pawn)) != null && pawn.health.hediffSet.HasHediff(PsysightDefOf.Psysight))
            {
                __result = false;
                return;
            }
        }
    }
}
