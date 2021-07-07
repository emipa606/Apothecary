using RimWorld;
using Verse;

namespace Apothecary
{
    // Token: 0x0200001A RID: 26
    public class HediffComp_AYCure : HediffComp
    {
        // Token: 0x04000046 RID: 70
        private bool curing;

        // Token: 0x04000045 RID: 69
        private int ticksToCure;

        // Token: 0x17000004 RID: 4
        // (get) Token: 0x06000049 RID: 73 RVA: 0x00003E3E File Offset: 0x0000203E
        public HediffCompProperties_AYCure MSProps => (HediffCompProperties_AYCure) props;

        // Token: 0x0600004A RID: 74 RVA: 0x00003E4C File Offset: 0x0000204C
        public void SetTicksToCure()
        {
            var period = 2500;
            int basehours;
            if (MSProps.CureHoursMin > 0f && MSProps.CureHoursMax > 0f && MSProps.CureHoursMax >= MSProps.CureHoursMin)
            {
                basehours = (int) (Rand.Range(MSProps.CureHoursMin, MSProps.CureHoursMax) * period);
            }
            else
            {
                basehours = Rand.Range(2, 5) * period;
            }

            if (basehours < period)
            {
                basehours = period;
            }

            if (basehours > 36 * period)
            {
                basehours = 36 * period;
            }

            ticksToCure = basehours;
        }

        // Token: 0x0600004B RID: 75 RVA: 0x00003EE0 File Offset: 0x000020E0
        public override void CompPostTick(ref float severityAdjustment)
        {
            if (curing && ticksToCure > 0)
            {
                ticksToCure--;
                return;
            }

            if (curing)
            {
                parent.Severity = 0f;
                Messages.Message(
                    "Apothecary.CureMsg".Translate(Pawn.LabelShort.CapitalizeFirst(), Def.label.CapitalizeFirst()),
                    Pawn, MessageTypeDefOf.PositiveEvent);
                return;
            }

            if (!AYCureUtility.ImmuneTo(Pawn, Def, out var Immunities))
            {
                return;
            }

            var ImmunitiesAsCure = 0;
            foreach (var s in Immunities)
            {
                if (s != "MSCondom_High")
                {
                    ImmunitiesAsCure++;
                }
            }

            if (ImmunitiesAsCure <= 0)
            {
                return;
            }

            SetTicksToCure();
            curing = true;
        }

        // Token: 0x0600004C RID: 76 RVA: 0x00003FCB File Offset: 0x000021CB
        public override void CompExposeData()
        {
            Scribe_Values.Look(ref ticksToCure, "ticksToCure");
            Scribe_Values.Look(ref curing, "curing");
        }

        // Token: 0x0600004D RID: 77 RVA: 0x00003FF1 File Offset: 0x000021F1
        public override string CompDebugString()
        {
            if (curing)
            {
                return "AYticksToCure: " + ticksToCure;
            }

            return "No active cure.";
        }
    }
}