using System;
using Verse;

namespace Apothecary
{
	// Token: 0x02000003 RID: 3
	public class AYBitsUtility
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002114 File Offset: 0x00000314
		internal static bool GetIsBitsSource(ThingDef defSource, bool isSource, Pawn pawn, out ThingDef bitsdef, out int bitsyield, out ThingDef newthingdef)
		{
			bitsdef = null;
			newthingdef = null;
			bitsyield = 0;
			if (AYBitsUtility.GetBitsSource(defSource) || isSource)
			{
				bitsdef = DefDatabase<ThingDef>.GetNamed(AYBitsUtility.bitschance.RandomElementByWeight((Pair<string, float> x) => x.Second).First, false);
				if (bitsdef != null)
				{
                    bitsyield = AYBitsUtility.GetBitsYield(bitsdef, defSource, isSource, out ThingDef newdef);
                    newthingdef = newdef;
					if (bitsyield > 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002190 File Offset: 0x00000390
		public static int GetBitsYield(ThingDef def, ThingDef defSource, bool isSource, out ThingDef newdef)
		{
			int yield = 0;
			newdef = null;
			string defName = def.defName;
			if (!(defName == "AYCalciumCarbonate"))
			{
				if (defName == "AYSalt")
				{
					if (defSource != null)
					{
						if (defSource.defName == "ChunkLimestone" || defSource.defName == "ChunkMarble" || defSource.defName == "ChunkEmperadordark" || defSource.defName == "ChunkCreoleMarble" || defSource.defName == "ChunkEtowahMarble" || defSource.defName == "ChunkChalk" || defSource.defName == "ChunkLignite" || defSource.defName == "ChunkSovite" || defSource.defName == "ChunkDacite" || defSource.defName == "ChunkBlueschist" || defSource.defName == "ChunkGreenSchist" || defSource.defName == "ChunkSyenite")
						{
							yield = Rand.Range(2, 5);
						}
						else
						{
							yield = Rand.Range(5, 12);
						}
					}
					else if (isSource)
					{
						yield = Rand.Range(5, 12);
					}
				}
			}
			else if (defSource != null)
			{
				if (defSource.defName == "ChunkLimestone" || defSource.defName == "ChunkMarble")
				{
					yield = Rand.Range(5, 10);
				}
				else if (defSource.defName == "ChunkEmperadordark" || defSource.defName == "ChunkCreoleMarble" || defSource.defName == "ChunkEtowahMarble" || defSource.defName == "ChunkQuartzite")
				{
					yield = Rand.Range(5, 10);
				}
				else if (defSource.defName == "ChunkChalk" || defSource.defName == "ChunkSyenite")
				{
					yield = Rand.Range(10, 20);
				}
				else if (defSource.defName == "ChunkSovite" || defSource.defName == "ChunkSerpentinite" || defSource.defName == "ChunkDacite" || defSource.defName == "ChunkBlueschist" || defSource.defName == "ChunkGreenSchist")
				{
					yield = Rand.Range(3, 6);
				}
				else if (defSource.defName == "ChunkClayStone" || defSource.defName == "ChunkDiorite")
				{
					yield = Rand.Range(2, 5);
				}
				else
				{
					ThingDef chknewdef = DefDatabase<ThingDef>.GetNamed("AYSalt", false);
					if (chknewdef != null)
					{
						newdef = chknewdef;
						yield = Rand.Range(5, 10);
					}
				}
			}
			return yield;
		}
		internal static uint ComputeStringHash(string s)
		{
			uint num = 0;
			if (s != null)
			{
				num = 2166136261U;
				for (int i = 0; i < s.Length; i++)
				{
					num = (s[i] ^ num) * 16777619U;
				}
			}
			return num;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002454 File Offset: 0x00000654
		internal static bool GetBitsSource(ThingDef def)
		{
			bool isBitsSource = false;
			if (def != null)
			{
				string defName = def.defName;
				uint num = ComputeStringHash(defName);
				if (num <= 1154306412U)
				{
					if (num <= 702995605U)
					{
						if (num <= 334370396U)
						{
							if (num <= 135430043U)
							{
								if (num != 130326325U)
								{
									if (num == 135430043U)
									{
										if (defName == "ChunkBasalt")
										{
											isBitsSource = true;
										}
									}
								}
								else if (defName == "ChunkCreoleMarble")
								{
									isBitsSource = true;
								}
							}
							else if (num != 237089535U)
							{
								if (num == 334370396U)
								{
									if (defName == "ChunkBlueschist")
									{
										isBitsSource = true;
									}
								}
							}
							else if (defName == "ChunkGreenSchist")
							{
								isBitsSource = true;
							}
						}
						else if (num <= 459180747U)
						{
							if (num != 450668030U)
							{
								if (num == 459180747U)
								{
									if (defName == "ChunkLepidolite")
									{
										isBitsSource = true;
									}
								}
							}
							else if (defName == "ChunkAnorthosite")
							{
								isBitsSource = true;
							}
						}
						else if (num != 468849279U)
						{
							if (num != 497423552U)
							{
								if (num == 702995605U)
								{
									if (defName == "ChunkDarkAndesite")
									{
										isBitsSource = true;
									}
								}
							}
							else if (defName == "ChunkDiorite")
							{
								isBitsSource = true;
							}
						}
						else if (defName == "ChunkGneiss")
						{
							isBitsSource = true;
						}
					}
					else if (num <= 990094961U)
					{
						if (num <= 803651291U)
						{
							if (num != 754271499U)
							{
								if (num == 803651291U)
								{
									if (defName == "ChunkQuartzite")
									{
										isBitsSource = true;
									}
								}
							}
							else if (defName == "ChunkSiltstone")
							{
								isBitsSource = true;
							}
						}
						else if (num != 840754687U)
						{
							if (num != 930377356U)
							{
								if (num == 990094961U)
								{
									if (defName == "ChunkGabbro")
									{
										isBitsSource = true;
									}
								}
							}
							else if (defName == "ChunkLignite")
							{
								isBitsSource = true;
							}
						}
						else if (defName == "ChunkSlate")
						{
							isBitsSource = true;
						}
					}
					else if (num <= 1113897122U)
					{
						if (num != 1103574401U)
						{
							if (num == 1113897122U)
							{
								if (defName == "ChunkDacite")
								{
									isBitsSource = true;
								}
							}
						}
						else if (defName == "ChunkEtowahMarble")
						{
							isBitsSource = true;
						}
					}
					else if (num != 1117948611U)
					{
						if (num != 1138947383U)
						{
							if (num == 1154306412U)
							{
								if (defName == "ChunkSchist")
								{
									isBitsSource = true;
								}
							}
						}
						else if (defName == "ChunkSyenite")
						{
							isBitsSource = true;
						}
					}
					else if (defName == "ChunkMarble")
					{
						isBitsSource = true;
					}
				}
				else if (num <= 3134642644U)
				{
					if (num <= 2223887258U)
					{
						if (num <= 1807593693U)
						{
							if (num != 1789891285U)
							{
								if (num == 1807593693U)
								{
									if (defName == "ChunkChalk")
									{
										isBitsSource = true;
									}
								}
							}
							else if (defName == "ChunkMonzonite")
							{
								isBitsSource = true;
							}
						}
						else if (num != 2078373069U)
						{
							if (num != 2104149328U)
							{
								if (num == 2223887258U)
								{
									if (defName == "ChunkSovite")
									{
										isBitsSource = true;
									}
								}
							}
							else if (defName == "ChunkRhyolite")
							{
								isBitsSource = true;
							}
						}
						else if (defName == "ChunkVibrantDunite")
						{
							isBitsSource = true;
						}
					}
					else if (num <= 2723944204U)
					{
						if (num != 2277173191U)
						{
							if (num == 2723944204U)
							{
								if (defName == "ChunkClayStone")
								{
									isBitsSource = true;
								}
							}
						}
						else if (defName == "ChunkScoria")
						{
							isBitsSource = true;
						}
					}
					else if (num != 2981327554U)
					{
						if (num != 3027871598U)
						{
							if (num == 3134642644U)
							{
								if (defName == "ChunkGranite")
								{
									isBitsSource = true;
								}
							}
						}
						else if (defName == "ChunkGreenGabbro")
						{
							isBitsSource = true;
						}
					}
					else if (defName == "ChunkSerpentinite")
					{
						isBitsSource = true;
					}
				}
				else if (num <= 3386876289U)
				{
					if (num <= 3317400192U)
					{
						if (num != 3242717934U)
						{
							if (num == 3317400192U)
							{
								if (defName == "ChunkPegmatite")
								{
									isBitsSource = true;
								}
							}
						}
						else if (defName == "ChunkLherzolite")
						{
							isBitsSource = true;
						}
					}
					else if (num != 3349364057U)
					{
						if (num != 3354811171U)
						{
							if (num == 3386876289U)
							{
								if (defName == "ChunkSandstone")
								{
									isBitsSource = true;
								}
							}
						}
						else if (defName == "ChunkMigmatite")
						{
							isBitsSource = true;
						}
					}
					else if (defName == "ChunkCharnockite")
					{
						isBitsSource = true;
					}
				}
				else if (num <= 3660010543U)
				{
					if (num != 3604908289U)
					{
						if (num == 3660010543U)
						{
							if (defName == "ChunkEmperadordark")
							{
								isBitsSource = true;
							}
						}
					}
					else if (defName == "ChunkJaspillite")
					{
						isBitsSource = true;
					}
				}
				else if (num != 3782560473U)
				{
					if (num != 4006516401U)
					{
						if (num == 4282638488U)
						{
							if (defName == "ChunkLimestone")
							{
								isBitsSource = true;
							}
						}
					}
					else if (defName == "ChunkAndesite")
					{
						isBitsSource = true;
					}
				}
				else if (defName == "ChunkThometzekite")
				{
					isBitsSource = true;
				}
			}
			return isBitsSource;
		}

		// Token: 0x04000001 RID: 1
		internal const string ChunkGranite = "ChunkGranite";

		// Token: 0x04000002 RID: 2
		internal const string ChunkMarble = "ChunkMarble";

		// Token: 0x04000003 RID: 3
		internal const string ChunkLimestone = "ChunkLimestone";

		// Token: 0x04000004 RID: 4
		internal const string ChunkSlate = "ChunkSlate";

		// Token: 0x04000005 RID: 5
		internal const string ChunkSandstone = "ChunkSandstone";

		// Token: 0x04000006 RID: 6
		internal const string CollapsedRocks = "CollapsedRocks";

		// Token: 0x04000007 RID: 7
		internal const string rxCollapsedRoofRocks = "rxCollapsedRoofRocks";

		// Token: 0x04000008 RID: 8
		internal const string ChunkEmperadordark = "ChunkEmperadordark";

		// Token: 0x04000009 RID: 9
		internal const string ChunkBlueschist = "ChunkBlueschist";

		// Token: 0x0400000A RID: 10
		internal const string ChunkSerpentinite = "ChunkSerpentinite";

		// Token: 0x0400000B RID: 11
		internal const string ChunkDacite = "ChunkDacite";

		// Token: 0x0400000C RID: 12
		internal const string ChunkSovite = "ChunkSovite";

		// Token: 0x0400000D RID: 13
		internal const string ChunkChalk = "ChunkChalk";

		// Token: 0x0400000E RID: 14
		internal const string ChunkCreoleMarble = "ChunkCreoleMarble";

		// Token: 0x0400000F RID: 15
		internal const string ChunkEtowahMarble = "ChunkEtowahMarble";

		// Token: 0x04000010 RID: 16
		internal const string ChunkGreenSchist = "ChunkGreenSchist";

		// Token: 0x04000011 RID: 17
		internal const string ChunkVibrantDunite = "ChunkVibrantDunite";

		// Token: 0x04000012 RID: 18
		internal const string ChunkDarkAndesite = "ChunkDarkAndesite";

		// Token: 0x04000013 RID: 19
		internal const string ChunkAnorthosite = "ChunkAnorthosite";

		// Token: 0x04000014 RID: 20
		internal const string ChunkBasalt = "ChunkBasalt";

		// Token: 0x04000015 RID: 21
		internal const string ChunkCharnockite = "ChunkCharnockite";

		// Token: 0x04000016 RID: 22
		internal const string ChunkGreenGabbro = "ChunkGreenGabbro";

		// Token: 0x04000017 RID: 23
		internal const string ChunkLherzolite = "ChunkLherzolite";

		// Token: 0x04000018 RID: 24
		internal const string ChunkMonzonite = "ChunkMonzonite";

		// Token: 0x04000019 RID: 25
		internal const string ChunkRhyolite = "ChunkRhyolite";

		// Token: 0x0400001A RID: 26
		internal const string ChunkScoria = "ChunkScoria";

		// Token: 0x0400001B RID: 27
		internal const string ChunkJaspillite = "ChunkJaspillite";

		// Token: 0x0400001C RID: 28
		internal const string ChunkLignite = "ChunkLignite";

		// Token: 0x0400001D RID: 29
		internal const string ChunkSiltstone = "ChunkSiltstone";

		// Token: 0x0400001E RID: 30
		internal const string ChunkMigmatite = "ChunkMigmatite";

		// Token: 0x0400001F RID: 31
		internal const string ChunkThometzekite = "ChunkThometzekite";

		// Token: 0x04000020 RID: 32
		internal const string ChunkLepidolite = "ChunkLepidolite";

		// Token: 0x04000021 RID: 33
		internal const string ChunkClaystone = "ChunkClayStone";

		// Token: 0x04000022 RID: 34
		internal const string ChunkAndesite = "ChunkAndesite";

		// Token: 0x04000023 RID: 35
		internal const string ChunkSyenite = "ChunkSyenite";

		// Token: 0x04000024 RID: 36
		internal const string ChunkGneiss = "ChunkGneiss";

		// Token: 0x04000025 RID: 37
		internal const string ChunkQuartzite = "ChunkQuartzite";

		// Token: 0x04000026 RID: 38
		internal const string ChunkSchist = "ChunkSchist";

		// Token: 0x04000027 RID: 39
		internal const string ChunkGabbro = "ChunkGabbro";

		// Token: 0x04000028 RID: 40
		internal const string ChunkDiorite = "ChunkDiorite";

		// Token: 0x04000029 RID: 41
		internal const string ChunkDunite = "ChunkDunite";

		// Token: 0x0400002A RID: 42
		internal const string ChunkPegmatite = "ChunkPegmatite";

		// Token: 0x0400002B RID: 43
		internal const string AYCalciumCarbonate = "AYCalciumCarbonate";

		// Token: 0x0400002C RID: 44
		internal const string AYSalt = "AYSalt";

		// Token: 0x0400002D RID: 45
		public const int AYCharcoalBits = 10;

		// Token: 0x0400002E RID: 46
		public const int AYWoodAshesBits = 20;

		// Token: 0x0400002F RID: 47
		private static readonly Pair<string, float>[] bitschance = new Pair<string, float>[]
		{
			new Pair<string, float>("AYCalciumCarbonate", 1f),
			new Pair<string, float>("AYSalt", 1f)
		};
	}
}
