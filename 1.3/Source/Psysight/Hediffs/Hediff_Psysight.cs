using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Psysight
{
    public class Hediff_Psysight : Hediff_Level
    {
        public override bool ShouldRemove => false;

        public override void Tick()
        {
            base.Tick();
            if(Find.TickManager.TicksGame % 180 == 0)
            {
                if (PsysightMod.settings.verboseLogging) { LogUtil.LogMessage($"Updating Psysight for {pawn.Name}"); }
                PsysightUtil.UpdateHediff(pawn);
                PsysightUtil.AddPsysightIfNeeded(pawn);
            }
        }
    }
}
