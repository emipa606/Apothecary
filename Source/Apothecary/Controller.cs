using System;
using UnityEngine;
using Verse;

namespace Apothecary
{
	// Token: 0x02000012 RID: 18
	public class Controller : Mod
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00003814 File Offset: 0x00001A14
		public override string SettingsCategory()
		{
			return "AY.Name".Translate();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003825 File Offset: 0x00001A25
		public override void DoSettingsWindowContents(Rect canvas)
		{
			Controller.Settings.DoWindowContents(canvas);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003832 File Offset: 0x00001A32
		public Controller(ModContentPack content) : base(content)
		{
			Controller.Settings = base.GetSettings<Settings>();
		}

		// Token: 0x0400003F RID: 63
		public static Settings Settings;
	}
}
