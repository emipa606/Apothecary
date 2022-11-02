using Verse;

namespace Apothecary;

public class CompProperties_AYCompostBin : CompProperties
{
    public int fermentTicks;

    public int numProductOne;

    public int numProductTwo;

    public string productOne;

    public string productTwo;

    public CompProperties_AYCompostBin()
    {
        compClass = typeof(CompAYCompostBin);
    }
}