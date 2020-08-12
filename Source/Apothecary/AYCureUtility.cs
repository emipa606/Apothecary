using System;
using System.Collections.Generic;
using Verse;

namespace Apothecary
{
	// Token: 0x02000004 RID: 4
	public class AYCureUtility
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002B14 File Offset: 0x00000D14
		internal static bool ImmuneTo(Pawn pawn, HediffDef def, out List<string> Immunities)
		{
			Immunities = new List<string>();
			bool immune = false;
			List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
			for (int i = 0; i < hediffs.Count; i++)
			{
				HediffStage curStage = hediffs[i].CurStage;
				if (curStage != null && curStage.makeImmuneTo != null)
				{
					for (int j = 0; j < curStage.makeImmuneTo.Count; j++)
					{
						if (curStage.makeImmuneTo[j] == def)
						{
							Immunities.AddDistinct(hediffs[i].def.defName);
							immune = true;
						}
					}
				}
			}
			return immune;
		}
	}
}
