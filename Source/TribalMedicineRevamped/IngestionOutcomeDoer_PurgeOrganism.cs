using System.Collections.Generic;

using RimWorld;
using Verse;
using Verse.AI;

namespace TribalMedicineRevamped
{
    internal class IngestionOutcomeDoer_PurgeOrganism : IngestionOutcomeDoer
    {
        private readonly float purgeChance = 0.5f;
        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested, int ingestedCount)
        {
            List<Hediff> pawnHediffs = pawn.health.hediffSet.hediffs;
            List<Hediff> affectedHediffs = new List<Hediff>();

            if (pawnHediffs != null && pawnHediffs.Any())
            {
                foreach (Hediff hediff in pawnHediffs)
                {
                    if (hediff.CurStage?.lifeThreatening ?? false && hediff.def.defName != "TM_BrootPoisoning")
                    {
                        if (Rand.Chance(purgeChance + (hediff.Severity * 0.3f)))
                        {
                            affectedHediffs.Add(hediff);
                        }
                    }
                }

                if (affectedHediffs.Any())
                {
                    foreach (Hediff hediff in affectedHediffs)
                    {
                        pawn.health.RemoveHediff(hediff);
                        Messages.Message("TM_MessageHediffCleansed".Translate(pawn.LabelShort, hediff.Label, pawn.Named("PAWN"), hediff.Named("HEDIFF")), pawn, MessageTypeDefOf.PositiveEvent, true);
                    }
                }
            }

            pawn.jobs.StartJob(JobMaker.MakeJob(JobDefOf.Vomit), JobCondition.InterruptForced);

        }
    }
}
