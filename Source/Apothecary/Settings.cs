using UnityEngine;
using Verse;

namespace Apothecary
{
    // Token: 0x02000027 RID: 39
    public class Settings : ModSettings
    {
        // Token: 0x0400004E RID: 78
        public bool AllowCollapseRocks = true;

        // Token: 0x0400004C RID: 76
        public bool RealismBandages;

        // Token: 0x0400004F RID: 79
        public float ResPct = 100f;

        // Token: 0x0400004D RID: 77
        public bool WashLowersQual = true;

        // Token: 0x06000073 RID: 115 RVA: 0x00004EF4 File Offset: 0x000030F4
        public void DoWindowContents(Rect canvas)
        {
            var listing_Standard = new Listing_Standard {ColumnWidth = canvas.width};
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
                listing_Standard.Label("AY.ResPct".Translate() + "  " + (int) ResPct);
                ResPct = (int) listing_Standard.Slider((int) ResPct, 10f, 200f);
                listing_Standard.Gap();
                Text.Font = GameFont.Tiny;
                listing_Standard.Label("          " + "AY.ResWarn".Translate());
                listing_Standard.Gap();
                listing_Standard.Label("          " + "AY.ResTip".Translate());
                Text.Font = GameFont.Small;
                listing_Standard.Gap();
                listing_Standard.End();
            }
        }

        // Token: 0x06000074 RID: 116 RVA: 0x00005074 File Offset: 0x00003274
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref RealismBandages, "RealismBandages");
            Scribe_Values.Look(ref AllowCollapseRocks, "AllowCollapseRocks", true);
            Scribe_Values.Look(ref WashLowersQual, "WashLowersQual", true);
            Scribe_Values.Look(ref ResPct, "ResPct", 100f);
        }
    }
}