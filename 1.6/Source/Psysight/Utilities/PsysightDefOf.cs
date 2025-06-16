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
    [DefOf]
    public static class PsysightDefOf
    {
        static PsysightDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PsysightDefOf));
        }

        public static HediffDef Psysight;
    }
}
