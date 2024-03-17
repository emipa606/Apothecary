using Verse;

namespace Apothecary;

public class HediffCompProperties_AYWakeAnasthetic : HediffCompProperties
{
    public readonly float sevReduce = 0.2f;

    public HediffCompProperties_AYWakeAnasthetic()
    {
        compClass = typeof(HediffComp_AYWakeAnasthetic);
    }
}