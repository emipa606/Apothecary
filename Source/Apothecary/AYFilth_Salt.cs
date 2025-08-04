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

        var targetMap = Map;
        var targetCell = Position;
        var snowDepth = targetMap.snowGrid.GetDepth(targetCell);
        if (!(snowDepth > 0f))
        {
            return;
        }

        snowDepth -= Math.Max(0f, GetRndMelt());
        targetMap.snowGrid.SetDepth(targetCell, snowDepth);
    }

    public static float GetRndMelt()
    {
        return Rand.Range(0.05f, 0.1f);
    }
}