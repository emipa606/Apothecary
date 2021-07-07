using System.Linq;
using RimWorld;
using Verse;

namespace Apothecary
{
    // Token: 0x0200000C RID: 12
    public class Building_BirdBath : Building_Art
    {
        // Token: 0x04000036 RID: 54
        public readonly int inspirationTicks = 30000;

        // Token: 0x06000022 RID: 34 RVA: 0x00003324 File Offset: 0x00001524
        public override void Tick()
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

        // Token: 0x06000023 RID: 35 RVA: 0x00003448 File Offset: 0x00001648
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

            if (b && GenSight.LineOfSight(p.Position, BB.Position, BB.Map, true))
            {
                return true;
            }

            return false;
        }

        // Token: 0x06000024 RID: 36 RVA: 0x000034C0 File Offset: 0x000016C0
        public bool IsInspired(int chance)
        {
            return Rand.Range(1, 100) < chance;
        }
    }
}