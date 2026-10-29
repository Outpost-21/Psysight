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
    [HarmonyPatch(typeof(Hediff_Psylink), "PostAdd")]
    public static class Patch_Hediff_Psylink_PostAdd
    {
        [HarmonyPostfix]
        public static void Postfix(Hediff_Psylink __instance)
        {
            PsysightUtil.AddPsysightIfNeeded(__instance.pawn);
        }
    }
}
