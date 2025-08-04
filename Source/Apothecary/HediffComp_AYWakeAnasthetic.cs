using RimWorld;
using Verse;

namespace Apothecary;

public class HediffComp_AYWakeAnasthetic : HediffComp
{
    private HediffCompProperties_AYWakeAnasthetic AYProps => (HediffCompProperties_AYWakeAnasthetic)props;

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