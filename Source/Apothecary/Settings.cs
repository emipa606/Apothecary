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
        var listing_Standard = new Listing_Standard { ColumnWidth = canvas.width };
        listing_Standard.Begin(canvas);
        listing_Standard.Gap();
        if (!ModLister.HasActiveModWithName("Medical Supplements"))
        {
            listing_Standard.CheckboxLabeled("AY.RealismBandages".Translate(), ref RealismBandages);
            listing_Standard.Gap();
        }

        listing_Standard.CheckboxLabeled("AY.WashLowersQual".Translate(), ref WashLowersQual);
        listing_Standard.Gap();
        listing_Standard.CheckboxLabeled("AY.AllowCollapseRocks".Translate(), ref AllowCollapseRocks);
        listing_Standard.Gap(36f);
        checked
        {
            listing_Standard.Label("AY.ResPct".Translate() + "  " + (int)ResPct);
            ResPct = (int)listing_Standard.Slider((int)ResPct, 10f, 200f);
            listing_Standard.Gap();
            Text.Font = GameFont.Tiny;
            listing_Standard.Label("          " + "AY.ResWarn".Translate());
            listing_Standard.Gap();
            listing_Standard.Label("          " + "AY.ResTip".Translate());
            Text.Font = GameFont.Small;
            if (Controller.currentVersion != null)
            {
                listing_Standard.Gap();
                GUI.contentColor = Color.gray;
                listing_Standard.Label("AY.CurrentModVersion".Translate(Controller.currentVersion));
                GUI.contentColor = Color.white;
            }

            listing_Standard.End();
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