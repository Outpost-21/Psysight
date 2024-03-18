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
    [StaticConstructorOnStartup]
    public static class PsysightStartup
    {
        static PsysightStartup()
        {
            PsysightUtil.RefactorPsysightStages(PsysightMod.settings);
        }
    }
}
