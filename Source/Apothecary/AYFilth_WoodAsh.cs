using RimWorld;
using Verse;

namespace Apothecary;

public class AYFilth_WoodAsh : Filth
{
    private int AYspawnTick;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref AYspawnTick, "AYspawnTick");
    }

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
            switch (thing)
            {
                case Blight when thing.Position == TargetCell:
                    DoAYToxicDamage(thing);
                    break;
                case Plant plant when plant.def.plant.IsTree &&
                                      plant.Position == TargetCell:
                    DoAYTreeBenefit(plant, thickness);
                    break;
            }
        }
    }

    private void DoAYTreeBenefit(Thing targ, int thick)
    {
        if (targ is not Plant { Growth: < 1f } tree)
        {
            return;
        }

        tree.Growth += AYGrowthPerTick(tree) * (1f - (thick / 20f));
        if (tree.Growth > 1f)
        {
            tree.Growth = 1f;
        }
    }

    private float AYGrowthPerTick(Plant tree)
    {
        if (tree.LifeStage != PlantLifeStage.Growing || GenLocalDate.DayPercent(tree) < 0.25f ||
            GenLocalDate.DayPercent(tree) > 0.8f)
        {
            return 0f;
        }

        return 1f / (60000f * tree.def.plant.growDays) * tree.GrowthRate * 400f;
    }

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