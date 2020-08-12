using System;
using Verse;

namespace Apothecary
{
	// Token: 0x02000019 RID: 25
	public class HediffCompProperties_AYRegen : HediffCompProperties
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00003E26 File Offset: 0x00002026
		public HediffCompProperties_AYRegen()
		{
			this.compClass = typeof(HediffComp_AYRegen);
		}

		// Token: 0x04000042 RID: 66
		public int RegenHoursMin;

		// Token: 0x04000043 RID: 67
		public int RegenHoursMax;

		// Token: 0x04000044 RID: 68
		public int RegenHealVal;
	}
}
