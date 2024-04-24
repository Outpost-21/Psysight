using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Psysight
{
    public class PsysightMod : Mod
    {
        public static PsysightMod mod;
        public static PsysightSettings settings;

        internal static string VersionDir => Path.Combine(mod.Content.ModMetaData.RootDir.FullName, "Version.txt");
        public static string CurrentVersion { get; private set; }

        public PsysightMod(ModContentPack content) : base(content)
        {
            mod = this;
            settings = GetSettings<PsysightSettings>();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            CurrentVersion = $"{version.Major}.{version.Minor}.{version.Build}";

            LogUtil.LogMessage($"{CurrentVersion} ::");

            File.WriteAllText(VersionDir, CurrentVersion);

            Harmony OuterRimHarmony = new Harmony("Neronix17.Psysight.RimWorld");
            OuterRimHarmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override string SettingsCategory() => "Psysight";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();
            ls.Begin(inRect);
            ls.AddLabeledSlider($"Sight gained per level: {settings.sightPerLevel.ToStringPercent()}", ref settings.sightPerLevel, 0f, 2f, "Min: 0%", "Max: 200%", 0.01f);
            ls.Note("This value is added per level between the min and max levels below.", GameFont.Tiny);
            ls.GapLine();
            ls.Label("Psysight Growth Range");
            ls.Note($"Current Range: {settings.levelRange.min} - {settings.levelRange.max}", GameFont.Tiny);
            ls.IntRange(ref settings.levelRange, 1, 200);
            ls.Note("Level is max 200 to keep the slider usable. If you want to have the range be beyond that then you're beyond my help.", GameFont.Tiny);
            ls.End();

            base.DoSettingsWindowContents(inRect);
        }

        public override void WriteSettings()
        {
            PsysightUtil.RefactorPsysightStages(settings);
            base.WriteSettings();
        }
    }
}
