using System;
using UnityEngine;
using Verse;

namespace Apothecary
{
	// Token: 0x02000027 RID: 39
	public class Settings : ModSettings
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00004EF4 File Offset: 0x000030F4
		public void DoWindowContents(Rect canvas)
		{
			Listing_Standard listing_Standard = new Listing_Standard();
			listing_Standard.ColumnWidth = canvas.width;
			listing_Standard.Begin(canvas);
			listing_Standard.Gap(12f);
			if (!ModLister.HasActiveModWithName("Medical Supplements"))
			{
				listing_Standard.CheckboxLabeled("AY.RealismBandages".Translate(), ref this.RealismBandages, null);
				listing_Standard.Gap(12f);
			}
			listing_Standard.CheckboxLabeled("AY.WashLowersQual".Translate(), ref this.WashLowersQual, null);
			listing_Standard.Gap(12f);
			listing_Standard.CheckboxLabeled("AY.AllowCollapseRocks".Translate(), ref this.AllowCollapseRocks, null);
			listing_Standard.Gap(36f);
			checked
			{
				listing_Standard.Label("AY.ResPct".Translate() + "  " + (int)this.ResPct, -1f, null);
				this.ResPct = (int)listing_Standard.Slider((int)this.ResPct, 10f, 200f);
				listing_Standard.Gap(12f);
				Text.Font = GameFont.Tiny;
				listing_Standard.Label("          " + "AY.ResWarn".Translate(), -1f, null);
				listing_Standard.Gap(12f);
				listing_Standard.Label("          " + "AY.ResTip".Translate(), -1f, null);
				Text.Font = GameFont.Small;
				listing_Standard.Gap(12f);
				listing_Standard.End();
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005074 File Offset: 0x00003274
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<bool>(ref this.RealismBandages, "RealismBandages", false, false);
			Scribe_Values.Look<bool>(ref this.AllowCollapseRocks, "AllowCollapseRocks", true, false);
			Scribe_Values.Look<bool>(ref this.WashLowersQual, "WashLowersQual", true, false);
			Scribe_Values.Look<float>(ref this.ResPct, "ResPct", 100f, false);
		}

		// Token: 0x0400004C RID: 76
		public bool RealismBandages;

		// Token: 0x0400004D RID: 77
		public bool WashLowersQual = true;

		// Token: 0x0400004E RID: 78
		public bool AllowCollapseRocks = true;

		// Token: 0x0400004F RID: 79
		public float ResPct = 100f;
	}
}
