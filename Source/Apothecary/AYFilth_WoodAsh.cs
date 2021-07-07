using RimWorld;
using Verse;

namespace Apothecary
{
    // Token: 0x02000006 RID: 6
    public class AYFilth_WoodAsh : Filth
    {
        // Token: 0x04000031 RID: 49
        private int AYspawnTick;

        // Token: 0x06000010 RID: 16 RVA: 0x00002C6C File Offset: 0x00000E6C
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref AYspawnTick, "AYspawnTick");
        }

        // Token: 0x06000011 RID: 17 RVA: 0x00002C88 File Offset: 0x00000E88
        public override void Tick()
        {
            AYspawnTick++;
            var removeDelay = 300000;
            if (def.defName == "AYAshScrap_Filth")
            {
                removeDelay += removeDelay / 2;
            }

            if (AYspawnTick >= removeDelay)
            {
                Destroy();
                return;
            }

            if (Find.TickManager.TicksGame % 295 != 0)
            {
                return;
            }

            var TargetMap = Map;
            var TargetCell = Position;
            var Thinglist = TargetCell.GetThingList(TargetMap);
            if (Thinglist.Count <= 0)
            {
                return;
            }

            foreach (var thing in Thinglist)
            {
                if (thing is Blight && thing.Position == TargetCell)
                {
                    DoAYToxicDamage(thing);
                }

                if (thing is Plant plant && plant.def.plant.IsTree &&
                    plant.Position == TargetCell)
                {
                    DoAYTreeBenefit(plant, thickness);
                }
            }
        }

        // Token: 0x06000012 RID: 18 RVA: 0x00002DB4 File Offset: 0x00000FB4
        private void DoAYTreeBenefit(Thing targ, int thick)
        {
            if (targ is not Plant {Growth: < 1f} tree)
            {
                return;
            }

            tree.Growth += AYGrowthPerTick(tree) * (1f - (thick / 20f));
            if (tree.Growth > 1f)
            {
                tree.Growth = 1f;
            }
        }

        // Token: 0x06000013 RID: 19 RVA: 0x00002E14 File Offset: 0x00001014
        private float AYGrowthPerTick(Plant tree)
        {
            if (tree.LifeStage != PlantLifeStage.Growing || GenLocalDate.DayPercent(tree) < 0.25f ||
                GenLocalDate.DayPercent(tree) > 0.8f)
            {
                return 0f;
            }

            return 1f / (60000f * tree.def.plant.growDays) * tree.GrowthRate * 400f;
        }

        // Token: 0x06000014 RID: 20 RVA: 0x00002E74 File Offset: 0x00001074
        private void DoAYToxicDamage(Thing targ)
        {
            if (targ is not Blight blight)
            {
                return;
            }

            var Dmg = 1f;
            blight.Severity -= Dmg;
            if (blight.Severity <= 0f)
            {
                blight.Destroy();
            }
        }
    }
}