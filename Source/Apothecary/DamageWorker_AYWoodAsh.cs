using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Apothecary
{
    // Token: 0x02000016 RID: 22
    public class DamageWorker_AYWoodAsh : DamageWorker
    {
        // Token: 0x0600003F RID: 63 RVA: 0x00003B50 File Offset: 0x00001D50
        public override void ExplosionStart(Explosion explosion, List<IntVec3> cellsToAffect)
        {
            FleckMaker.ThrowSmoke(explosion.Position.ToVector3(), explosion.Map, 1f);
            ExplosionVisualEffectCenter(explosion);
        }
    }
}