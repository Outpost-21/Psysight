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
    public class Comp_BlindfoldPsysight : ThingComp
    {
        public CompProperties_BlindfoldPsysight Props => (CompProperties_BlindfoldPsysight)props;

        public override void Notify_Equipped(Pawn pawn)
        {
            base.Notify_Equipped(pawn);
            if(pawn != null && pawn.health.hediffSet.HasHediff(PsysightDefOf.Psysight))
            {
                if (pawn.health.hediffSet.HasHediff(Props.hediff))
                {
                    pawn.health.RemoveHediff(pawn.health.hediffSet.GetFirstHediffOfDef(Props.hediff));
                }
            }
        }
    }
}
