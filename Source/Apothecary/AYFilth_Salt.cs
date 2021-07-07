using System;
using RimWorld;
using Verse;

namespace Apothecary
{
    // Token: 0x02000005 RID: 5
    public class AYFilth_Salt : Filth
    {
        // Token: 0x04000030 RID: 48
        private int AYspawnTick;

        // Token: 0x0600000C RID: 12 RVA: 0x00002BB1 File Offset: 0x00000DB1
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref AYspawnTick, "AYspawnTick");
        }

        // Token: 0x0600000D RID: 13 RVA: 0x00002BCC File Offset: 0x00000DCC
        public override void Tick()
        {
            AYspawnTick++;
            var removeDelay = 180000;
            if (AYspawnTick >= removeDelay)
            {
                Destroy();
                return;
            }

            if (Find.TickManager.TicksGame % 2497 != 0)
            {
                return;
            }

            var TargetMap = Map;
            var TargetCell = Position;
            var SnowDepth = TargetMap.snowGrid.GetDepth(TargetCell);
            if (!(SnowDepth > 0f))
            {
                return;
            }

            SnowDepth -= Math.Max(0f, GetRndMelt());
            TargetMap.snowGrid.SetDepth(TargetCell, SnowDepth);
        }

        // Token: 0x0600000E RID: 14 RVA: 0x00002C53 File Offset: 0x00000E53
        public float GetRndMelt()
        {
            return Rand.Range(0.05f, 0.1f);
        }
    }
}