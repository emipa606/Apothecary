using System;
using System.Collections.Generic;
using Verse;

namespace Apothecary;

[StaticConstructorOnStartup]
internal static class AY_Options_Initializer
{
    static AY_Options_Initializer()
    {
        LongEventHandler.QueueLongEvent(Setup, "LibraryStartup", false, null);
    }

    public static void Setup()
    {
        var allDefs = DefDatabase<ResearchProjectDef>.AllDefsListForReading;
        if (allDefs.Count <= 0)
        {
            return;
        }

        var AYList = AYResearchList();
        foreach (var ResDef in allDefs)
        {
            if (!AYList.Contains(ResDef.defName))
            {
                continue;
            }

            var Resbase = ResDef.baseCost;
            Resbase = checked((int)Math.Round(Resbase * (Controller.Settings.ResPct / 100f)));
            ResDef.baseCost = Resbase;
        }
    }

    public static List<string> AYResearchList()
    {
        var list = new List<string>();
        list.AddDistinct("AYHerbsSimple");
        list.AddDistinct("AYHerbsIntermediate");
        list.AddDistinct("AYHerbsYield");
        list.AddDistinct("AYHerbsComplex");
        list.AddDistinct("AYTallow");
        list.AddDistinct("AYCharcoal");
        list.AddDistinct("AYPowders");
        list.AddDistinct("AYExtracts");
        list.AddDistinct("AYSalts");
        list.AddDistinct("AYTeas");
        list.AddDistinct("AYElixirs");
        list.AddDistinct("AYOintments");
        list.AddDistinct("AYTonics");
        return list;
    }
}