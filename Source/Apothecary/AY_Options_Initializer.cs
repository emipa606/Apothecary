using System;
using System.Collections.Generic;
using Verse;

namespace Apothecary
{
	// Token: 0x0200000B RID: 11
	[StaticConstructorOnStartup]
	internal static class AY_Options_Initializer
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000031CC File Offset: 0x000013CC
		static AY_Options_Initializer()
		{
			LongEventHandler.QueueLongEvent(new Action(AY_Options_Initializer.Setup), "LibraryStartup", false, null, true);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000031E8 File Offset: 0x000013E8
		public static void Setup()
		{
			List<ResearchProjectDef> allDefs = DefDatabase<ResearchProjectDef>.AllDefsListForReading;
			if (allDefs.Count > 0)
			{
				List<string> AYList = AY_Options_Initializer.AYResearchList();
				foreach (ResearchProjectDef ResDef in allDefs)
				{
					if (AYList.Contains(ResDef.defName))
					{
						float Resbase = ResDef.baseCost;
						Resbase = checked((int)Math.Round(unchecked(Resbase * (Controller.Settings.ResPct / 100f))));
						ResDef.baseCost = Resbase;
					}
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003280 File Offset: 0x00001480
		public static List<string> AYResearchList()
		{
			List<string> list = new List<string>();
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
}
