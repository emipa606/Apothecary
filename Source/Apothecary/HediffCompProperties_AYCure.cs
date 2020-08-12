using System;
using Verse;

namespace Apothecary
{
	// Token: 0x02000018 RID: 24
	public class HediffCompProperties_AYCure : HediffCompProperties
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00003E0E File Offset: 0x0000200E
		public HediffCompProperties_AYCure()
		{
			this.compClass = typeof(HediffComp_AYCure);
		}

		// Token: 0x04000040 RID: 64
		public float CureHoursMin;

		// Token: 0x04000041 RID: 65
		public float CureHoursMax;
	}
}
