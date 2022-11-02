using RimWorld;
using Verse;

namespace Apothecary
{
    public class ThoughtWorker_AYSoap : ThoughtWorker
    {
        public HediffDef AYHedCheckLavSoap = HediffDefAYSoaps.AYLavenderSoapHigh;

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

            var hedSet = hediffSet;
            if (hedSet?.GetFirstHediffOfDef(AYHedCheckLavSoap) != null)
            {
                return ThoughtState.ActiveAtStage(1);
            }

            return false;
        }

        [DefOf]
        public static class HediffDefAYSoaps
        {
            public static HediffDef AYLavenderSoapHigh;
        }
    }
}
