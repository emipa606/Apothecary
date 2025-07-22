using System;
using RimWorld;
using Verse;

namespace Apothecary;

public class AYFilth_Salt : Filth
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

    public float GetRndMelt()
    {
        return Rand.Range(0.05f, 0.1f);
    }
}