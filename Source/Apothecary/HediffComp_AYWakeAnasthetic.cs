using RimWorld;
using Verse;

namespace Apothecary
{
    // Token: 0x0200001C RID: 28
    public class HediffComp_AYWakeAnasthetic : HediffComp
    {
        // Token: 0x17000006 RID: 6
        // (get) Token: 0x06000059 RID: 89 RVA: 0x000044CC File Offset: 0x000026CC
        public HediffCompProperties_AYWakeAnasthetic AYProps => (HediffCompProperties_AYWakeAnasthetic) props;

        // Token: 0x0600005A RID: 90 RVA: 0x000044DC File Offset: 0x000026DC
        public override void CompPostTick(ref float severityAdjustment)
        {
            if (!Pawn.Awake())
            {
                return;
            }

            var health = Pawn.health;
            bool b;
            if (health == null)
            {
                b = false;
            }
            else
            {
                var capacities = health.capacities;
                var num = capacities != null
                    ? new float?(capacities.GetLevel(PawnCapacityDefOf.Consciousness))
                    : null;
                var num2 = 0.05f;
                b = (num.GetValueOrDefault() > num2) & (num != null);
            }

            if (!b || !Pawn.IsHashIntervalTick(2500))
            {
                return;
            }

            var pawn = Pawn;
            HediffSet hediffSet;
            if (pawn == null)
            {
                hediffSet = null;
            }
            else
            {
                var health2 = pawn.health;
                hediffSet = health2?.hediffSet;
            }

            var set = hediffSet;

            var anasthetic = set?.GetFirstHediffOfDef(HediffDefOf.Anesthetic);
            if (anasthetic == null)
            {
                return;
            }

            var sev = anasthetic.Severity;
            if (!(sev > 0f))
            {
                return;
            }

            var sevloss = sev * AYProps.sevReduce;
            if (sev - sevloss > 0f)
            {
                anasthetic.Severity = sev - sevloss;
                return;
            }

            anasthetic.Severity = 0f;
        }
    }
}