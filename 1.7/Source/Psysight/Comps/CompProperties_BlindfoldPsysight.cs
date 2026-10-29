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
    public class CompProperties_BlindfoldPsysight : CompProperties
    {
        public CompProperties_BlindfoldPsysight()
        {
            compClass = typeof(Comp_BlindfoldPsysight);
        }

        public HediffDef hediff;
        public BodyPartDef bodyPart;
    }
}
