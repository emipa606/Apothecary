using System.Linq;
using RimWorld;
using Verse;

namespace Apothecary;

public class Building_BirdBath : Building_Art
{
    public readonly int inspirationTicks = 30000;

    protected override void Tick()
    {
        base.Tick();
        if (!Spawned || !this.IsHashIntervalTick(inspirationTicks))
        {
            return;
        }

        var things = GenRadial.RadialDistinctThingsAround(this.TrueCenter().ToIntVec3(), Map, 10f, false)
            .ToList();
        if (things.Count <= 0)
        {
            return;
        }

        foreach (var thing in things)
        {
            Pawn p;
            if ((p = thing as Pawn) == null || !IsValidForInspiration(thing, this) || !IsInspired(25) ||
                p.mindState.inspirationHandler.Inspired)
            {
                continue;
            }

            var IDef = (from x in DefDatabase<InspirationDef>.AllDefsListForReading
                where x.Worker.InspirationCanOccur(p)
                select x).RandomElementByWeightWithFallback(x => x.Worker.CommonalityFor(p));
            p.mindState.inspirationHandler.TryStartInspiration(IDef);
        }
    }

    public bool IsValidForInspiration(Thing thing, Building BB)
    {
        Pawn p;
        if ((p = thing as Pawn) == null || !thing.Spawned || !p.Awake() ||
            !p.health.capacities.CapableOf(PawnCapacityDefOf.Sight))
        {
            return false;
        }

        var needs = p.needs;
        var b = needs?.beauty != null;

        return b && GenSight.LineOfSight(p.Position, BB.Position, BB.Map, true);
    }

    public bool IsInspired(int chance)
    {
        return Rand.Range(1, 100) < chance;
    }
}