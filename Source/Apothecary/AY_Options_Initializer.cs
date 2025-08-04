using System;
using System.Collections.Generic;
using Verse;

namespace Apothecary;

[StaticConstructorOnStartup]
internal static class AY_Options_Initializer
{
    static AY_Options_Initializer()
    {
        LongEventHandler.QueueLongEvent(setup, "LibraryStartup", false, null);
    }

    private static void setup()
    {
        var allDefs = DefDatabase<ResearchProjectDef>.AllDefsListForReading;
        if (allDefs.Count <= 0)
        {
            return;
        }

        var ayList = ayResearchList();
        foreach (var resDef in allDefs)
        {
            if (!ayList.Contains(resDef.defName))
            {
                continue;
            }

            var resbase = resDef.baseCost;
            resbase = checked((int)Math.Round(resbase * (Controller.Settings.ResPct / 100f)));
            resDef.baseCost = resbase;
        }
    }

    private static List<string> ayResearchList()
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