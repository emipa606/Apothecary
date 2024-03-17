using RimWorld;
using Verse;

namespace Apothecary;

public class HediffComp_AYCure : HediffComp
{
    private bool curing;

    private int ticksToCure;

    public HediffCompProperties_AYCure MSProps => (HediffCompProperties_AYCure)props;

    public void SetTicksToCure()
    {
        var period = 2500;
        int basehours;
        if (MSProps.CureHoursMin > 0f && MSProps.CureHoursMax > 0f && MSProps.CureHoursMax >= MSProps.CureHoursMin)
        {
            basehours = (int)(Rand.Range(MSProps.CureHoursMin, MSProps.CureHoursMax) * period);
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

    public override void CompPostTick(ref float severityAdjustment)
    {
        switch (curing)
        {
            case true when ticksToCure > 0:
                ticksToCure--;
                return;
            case true:
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

    public override void CompExposeData()
    {
        Scribe_Values.Look(ref ticksToCure, "ticksToCure");
        Scribe_Values.Look(ref curing, "curing");
    }

    public override string CompDebugString()
    {
        return curing ? $"AYticksToCure: {ticksToCure}" : "No active cure.";
    }
}