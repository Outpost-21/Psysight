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
    public class PsysightSettings : ModSettings
    {
        public bool verboseLogging = false;

        public IntRange levelRange = new IntRange(1, 20);

        public float sightPerLevel = 0.1f;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref levelRange, "levelRange", new IntRange(1, 20));
            Scribe_Values.Look(ref sightPerLevel, "sightPerLevel", 0.1f);

        }

        public bool IsValidSetting(string input)
        {
            if (GetType().GetFields().Where(p => p.FieldType == typeof(bool)).Any(i => i.Name == input))
            {
                return true;
            }

            return false;
        }

        public IEnumerable<string> GetEnabledSettings
        {
            get
            {
                return GetType().GetFields().Where(p => p.FieldType == typeof(bool) && (bool)p.GetValue(this)).Select(p => p.Name);
            }
        }
    }
}
