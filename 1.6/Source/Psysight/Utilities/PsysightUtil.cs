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
    public static class PsysightUtil
    {
        public static HediffStage noneStage;

        public static void RefactorPsysightStages(PsysightSettings s)
        {
            if(noneStage == null)
            {
                noneStage = PsysightDefOf.Psysight.stages.FirstOrDefault();
            }

            HediffDef psysightDef = PsysightDefOf.Psysight;
            psysightDef.maxSeverity = s.levelRange.max;
            psysightDef.stages.Clear();
            psysightDef.stages.Add(noneStage);

            int curLevel = s.levelRange.min;
            int levelCount = ((s.levelRange.max + 1) - s.levelRange.min);
            if (s.verboseLogging) { LogUtil.LogMessage("Starting Refactor"); }
            for (int i = 1; i <= levelCount; i++)
            {
                HediffStage curStage = new HediffStage();
                curStage.minSeverity = curLevel;
                curLevel++;
                PawnCapacityModifier capMod = new PawnCapacityModifier();
                capMod.capacity = PawnCapacityDefOf.Sight;
                capMod.offset = float.Parse((s.sightPerLevel * i).ToString("0.00")) - 0.01f;
                curStage.capMods.Add(capMod);

                if (s.verboseLogging) { LogUtil.LogMessage($"- Stage {i}\nMinSeverity = {curStage.minSeverity}, Sight Offset = {capMod.offset}"); }

                psysightDef.stages.Add(curStage);
            }
        }

        public static void UpdateHediff(Pawn pawn)
        {
            if (pawn.health.hediffSet.HasHediff(PsysightDefOf.Psysight))
            {
                Hediff_Psysight def = (Hediff_Psysight)pawn.health.hediffSet.GetFirstHediffOfDef(PsysightDefOf.Psysight);
                if (def != null)
                {
                    def.SetLevelTo(pawn.GetPsylinkLevel());
                }

                HediffDef blindfold = DefDatabase<HediffDef>.GetNamedSilentFail("Blindfold");
                if (blindfold != null && pawn.health.hediffSet.HasHediff(blindfold))
                {
                    pawn.health.RemoveHediff(pawn.health.hediffSet.GetFirstHediffOfDef(blindfold));
                }
            }
        }

        public static void AddPsysightIfNeeded(Pawn pawn)
        {
            try
            {
                if (pawn == null || !pawn.Spawned || pawn.health == null || pawn.health.hediffSet == null)
                {
                    return;
                }
                if (!pawn.health.hediffSet.HasHediff(PsysightDefOf.Psysight) && ShouldHavePsysight(pawn))
                {
                    AddPsysight(pawn);
                }
                else if (pawn.health.hediffSet.HasHediff(PsysightDefOf.Psysight) && !ShouldHavePsysight(pawn))
                {
                    RemovePsysight(pawn);
                }
                UpdateHediff(pawn);
            }
            catch (Exception e)
            {
                if (PsysightMod.settings.verboseLogging) { LogUtil.LogError($"Exception caught in AddPsysightIfNeeded, likely the one off superfluous one that happens for an unknown reason.\n{e}"); }
            }
        }

        public static void AddPsysight(Pawn pawn)
        {
            if (!pawn.health.hediffSet.HasHediff(PsysightDefOf.Psysight))
            {
                pawn.health.AddHediff(PsysightDefOf.Psysight, pawn.health.hediffSet.GetBrain());

                HediffDef blindfold = DefDatabase<HediffDef>.GetNamedSilentFail("Blindfold");
                if (blindfold != null && pawn.health.hediffSet.HasHediff(blindfold))
                {
                    pawn.health.RemoveHediff(pawn.health.hediffSet.GetFirstHediffOfDef(blindfold));
                }
            }
        }

        public static void RemovePsysight(Pawn pawn)
        {
            Hediff_Psysight hediff = (Hediff_Psysight)pawn.health.hediffSet.GetFirstHediffOfDef(PsysightDefOf.Psysight);
            pawn.health.RemoveHediff(hediff);

            HediffDef blindfold = DefDatabase<HediffDef>.GetNamedSilentFail("Blindfold");
            if (blindfold != null )
            {
                Apparel apBlindfold = pawn.apparel?.wornApparel?.innerList?.FirstOrDefault(ap => ap?.def?.GetCompProperties<CompProperties_CauseHediff_Apparel>()?.hediff == blindfold) ?? null;
                if (apBlindfold != null && !pawn.health.hediffSet.HasHediff(blindfold))
                {
                    HediffComp_RemoveIfApparelDropped hediffComp = pawn.health.AddHediff(blindfold, pawn.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Undefined, null, null).FirstOrFallback((BodyPartRecord p) => p.def == BodyPartDefOf.Head, null), null, null).TryGetComp<HediffComp_RemoveIfApparelDropped>();
                    if(hediffComp != null)
                    {
                        hediffComp.wornApparel = apBlindfold;
                    }
                }
            }
        }

        public static bool ShouldHavePsysight(Pawn pawn)
        {
            return !pawn.Dead && pawn.HasPsylink && !HasSightSource(pawn);
        }

        //public static bool HasSightCapability(Pawn pawn)
        //{
        //    if (pawn.health.hediffSet.HasHediff(PsysightDefOf.Psysight))
        //    {
        //        if (pawn.health.capacities.GetLevel(PawnCapacityDefOf.Sight) > ((pawn?.health?.hediffSet?.GetFirstHediffOfDef(PsysightDefOf.Psysight)?.CurStage?.capMods?.Find(cm => cm.capacity == PawnCapacityDefOf.Sight)?.offset ?? 0f) + 0.01f))
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    return HasSightSource(pawn);
        //}

        public static bool HasSightSource(Pawn pawn)
        {
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
            int num = 0;
            for (int i = 0; i < hediffs.Count; i++)
            {
                if (hediffs[i] is Hediff_MissingPart hediff_MissingPart && hediff_MissingPart.Part.def.tags.Contains(BodyPartTagDefOf.SightSource))
                {
                    num++;
                }
                else if (hediffs[i] is Hediff_AddedPart hediff_AddedPart && hediff_AddedPart.Part.def.tags.Contains(BodyPartTagDefOf.SightSource) && hediff_AddedPart.def.addedPartProps.partEfficiency == 0f)
                {
                    num++;
                }
            }
            if (num <= 1)
            {
                return true;
            }
            return false;
            // Old method for archive purposes.
            //List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
            //for (int i = 0; i < hediffs.Count; i++)
            //{
            //    Hediff_AddedPart addedPart;
            //    if ((addedPart = hediffs[i] as Hediff_AddedPart) != null && addedPart.Part.def.tags.Contains(BodyPartTagDefOf.SightSource) && addedPart.def.addedPartProps.partEfficiency > 0f) 
            //    {
            //        return true;
            //    }
            //}
            //return false;
        }
    }
}
