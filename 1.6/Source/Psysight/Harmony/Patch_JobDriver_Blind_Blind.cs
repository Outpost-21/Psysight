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
    [HarmonyPatch(typeof(JobDriver_Blind), "Blind")]
    public static class Patch_JobDriver_Blind_Blind
    {
        [HarmonyPostfix]
        public static void Postfix(Pawn pawn, Pawn doer)
        {
            if (!(pawn.mapIndexOrState > Find.Maps.Count))
            {
                PsysightUtil.AddPsysightIfNeeded(pawn);
            }
        }
    }
}
