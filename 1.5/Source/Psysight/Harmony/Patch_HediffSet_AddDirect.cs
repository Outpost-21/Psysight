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
    [HarmonyPatch(typeof(HediffSet), "AddDirect")]
    public static class Patch_HediffSet_AddDirect
    {
        [HarmonyPostfix]
        public static void Postfix(HediffSet __instance)
        {
            PsysightUtil.AddPsysightIfNeeded(__instance.pawn);
        }
    }
}
