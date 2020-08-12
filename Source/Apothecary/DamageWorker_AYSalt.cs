using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Apothecary
{
	// Token: 0x02000015 RID: 21
	public class DamageWorker_AYSalt : DamageWorker
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00003B14 File Offset: 0x00001D14
		public override void ExplosionStart(Explosion explosion, List<IntVec3> cellsToAffect)
		{
			MoteMaker.ThrowSmoke(explosion.Position.ToVector3(), explosion.Map, 1f);
			this.ExplosionVisualEffectCenter(explosion);
		}
	}
}
