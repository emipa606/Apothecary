using UnityEngine;
using Verse;

namespace Apothecary;

public class Settings : ModSettings
{
    public bool AllowCollapseRocks = true;

    public bool RealismBandages;

    public float ResPct = 100f;

    public bool WashLowersQual = true;

    public void DoWindowContents(Rect canvas)
    {
        var listingStandard = new Listing_Standard { ColumnWidth = canvas.width };
        listingStandard.Begin(canvas);
        listingStandard.Gap();
        if (!ModLister.HasActiveModWithName("Medical Supplements"))
        {
            listingStandard.CheckboxLabeled("AY.RealismBandages".Translate(), ref RealismBandages);
            listingStandard.Gap();
        }

        listingStandard.CheckboxLabeled("AY.WashLowersQual".Translate(), ref WashLowersQual);
        listingStandard.Gap();
        listingStandard.CheckboxLabeled("AY.AllowCollapseRocks".Translate(), ref AllowCollapseRocks);
        listingStandard.Gap(36f);
        checked
        {
            listingStandard.Label("AY.ResPct".Translate() + "  " + (int)ResPct);
            ResPct = (int)listingStandard.Slider((int)ResPct, 10f, 200f);
            listingStandard.Gap();
            Text.Font = GameFont.Tiny;
            listingStandard.Label("          " + "AY.ResWarn".Translate());
            listingStandard.Gap();
            listingStandard.Label("          " + "AY.ResTip".Translate());
            Text.Font = GameFont.Small;
            if (Controller.CurrentVersion != null)
            {
                listingStandard.Gap();
                GUI.contentColor = Color.gray;
                listingStandard.Label("AY.CurrentModVersion".Translate(Controller.CurrentVersion));
                GUI.contentColor = Color.white;
            }

            listingStandard.End();
        }
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref RealismBandages, "RealismBandages");
        Scribe_Values.Look(ref AllowCollapseRocks, "AllowCollapseRocks", true);
        Scribe_Values.Look(ref WashLowersQual, "WashLowersQual", true);
        Scribe_Values.Look(ref ResPct, "ResPct", 100f);
    }
}