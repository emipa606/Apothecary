using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Apothecary;

public class DamageWorker_AYSalt : DamageWorker
{
    public override void ExplosionStart(Explosion explosion, List<IntVec3> cellsToAffect)
    {
        FleckMaker.ThrowSmoke(explosion.Position.ToVector3(), explosion.Map, 1f);
        ExplosionVisualEffectCenter(explosion);
    }
}