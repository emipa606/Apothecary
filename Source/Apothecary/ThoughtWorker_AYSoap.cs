using RimWorld;
using Verse;

namespace Apothecary;

public class ThoughtWorker_AYSoap : ThoughtWorker
{
    private readonly HediffDef AYHedCheckLavSoap = HediffDefAYSoaps.AYLavenderSoapHigh;

    protected override ThoughtState CurrentSocialStateInternal(Pawn pawn, Pawn other)
    {
        if (!other.RaceProps.Humanlike || !RelationsUtility.PawnsKnowEachOther(pawn, other))
        {
            return false;
        }

        if (!pawn.health.capacities.CapableOf(PawnCapacityDefOf.Breathing))
        {
            return false;
        }

        if (other.health == null)
        {
            return false;
        }

        var health = other.health;
        var hediffSet = health?.hediffSet;

        return hediffSet?.GetFirstHediffOfDef(AYHedCheckLavSoap) != null ? ThoughtState.ActiveAtStage(1) : false;
    }

    [DefOf]
    private static class HediffDefAYSoaps
    {
        public static HediffDef AYLavenderSoapHigh;
    }
}