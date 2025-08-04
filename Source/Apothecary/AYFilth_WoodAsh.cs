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

    protected override void Tick()
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

        var targetMap = Map;
        var targetCell = Position;
        var thingList = targetCell.GetThingList(targetMap);
        if (thingList.Count <= 0)
        {
            return;
        }

        foreach (var thing in thingList)
        {
            switch (thing)
            {
                case Blight when thing.Position == targetCell:
                    doAyToxicDamage(thing);
                    break;
                case Plant plant when plant.def.plant.IsTree &&
                                      plant.Position == targetCell:
                    doAyTreeBenefit(plant, thickness);
                    break;
            }
        }
    }

    private static void doAyTreeBenefit(Thing targ, int thick)
    {
        if (targ is not Plant { Growth: < 1f } tree)
        {
            return;
        }

        tree.Growth += ayGrowthPerTick(tree) * (1f - (thick / 20f));
        if (tree.Growth > 1f)
        {
            tree.Growth = 1f;
        }
    }

    private static float ayGrowthPerTick(Plant tree)
    {
        if (tree.LifeStage != PlantLifeStage.Growing || GenLocalDate.DayPercent(tree) < 0.25f ||
            GenLocalDate.DayPercent(tree) > 0.8f)
        {
            return 0f;
        }

        return 1f / (60000f * tree.def.plant.growDays) * tree.GrowthRate * 400f;
    }

    private static void doAyToxicDamage(Thing targ)
    {
        if (targ is not Blight blight)
        {
            return;
        }

        const float dmg = 1f;
        blight.Severity -= dmg;
        if (blight.Severity <= 0f)
        {
            blight.Destroy();
        }
    }
}