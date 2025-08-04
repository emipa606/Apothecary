using Verse;

namespace Apothecary;

public class AYBitsUtility
{
    internal const string ChunkGranite = "ChunkGranite";

    internal const string ChunkMarble = "ChunkMarble";

    internal const string ChunkLimestone = "ChunkLimestone";

    internal const string ChunkSlate = "ChunkSlate";

    internal const string ChunkSandstone = "ChunkSandstone";

    internal const string CollapsedRocks = "CollapsedRocks";

    internal const string rxCollapsedRoofRocks = "rxCollapsedRoofRocks";

    internal const string ChunkEmperadordark = "ChunkEmperadordark";

    internal const string ChunkBlueschist = "ChunkBlueschist";

    internal const string ChunkSerpentinite = "ChunkSerpentinite";

    internal const string ChunkDacite = "ChunkDacite";

    internal const string ChunkSovite = "ChunkSovite";

    internal const string ChunkChalk = "ChunkChalk";

    internal const string ChunkCreoleMarble = "ChunkCreoleMarble";

    internal const string ChunkEtowahMarble = "ChunkEtowahMarble";

    internal const string ChunkGreenSchist = "ChunkGreenSchist";

    internal const string ChunkVibrantDunite = "ChunkVibrantDunite";

    internal const string ChunkDarkAndesite = "ChunkDarkAndesite";

    internal const string ChunkAnorthosite = "ChunkAnorthosite";

    internal const string ChunkBasalt = "ChunkBasalt";

    internal const string ChunkCharnockite = "ChunkCharnockite";

    internal const string ChunkGreenGabbro = "ChunkGreenGabbro";

    internal const string ChunkLherzolite = "ChunkLherzolite";

    internal const string ChunkMonzonite = "ChunkMonzonite";

    internal const string ChunkRhyolite = "ChunkRhyolite";

    internal const string ChunkScoria = "ChunkScoria";

    internal const string ChunkJaspillite = "ChunkJaspillite";

    internal const string ChunkLignite = "ChunkLignite";

    internal const string ChunkSiltstone = "ChunkSiltstone";

    internal const string ChunkMigmatite = "ChunkMigmatite";

    internal const string ChunkThometzekite = "ChunkThometzekite";

    internal const string ChunkLepidolite = "ChunkLepidolite";

    internal const string ChunkClaystone = "ChunkClayStone";

    internal const string ChunkAndesite = "ChunkAndesite";

    internal const string ChunkSyenite = "ChunkSyenite";

    internal const string ChunkGneiss = "ChunkGneiss";

    internal const string ChunkQuartzite = "ChunkQuartzite";

    internal const string ChunkSchist = "ChunkSchist";

    internal const string ChunkGabbro = "ChunkGabbro";

    internal const string ChunkDiorite = "ChunkDiorite";

    internal const string ChunkDunite = "ChunkDunite";

    internal const string ChunkPegmatite = "ChunkPegmatite";

    internal const string AYCalciumCarbonate = "AYCalciumCarbonate";

    internal const string AYSalt = "AYSalt";

    public const int AYCharcoalBits = 10;

    public const int AYWoodAshesBits = 20;

    private static readonly Pair<string, float>[] bitschance =
    [
        new("AYCalciumCarbonate", 1f),
        new("AYSalt", 1f)
    ];

    internal static bool GetIsBitsSource(ThingDef defSource, bool isSource, Pawn pawn, out ThingDef bitsdef,
        out int bitsyield, out ThingDef newthingdef)
    {
        bitsdef = null;
        newthingdef = null;
        bitsyield = 0;
        if (!getBitsSource(defSource) && !isSource)
        {
            return false;
        }

        bitsdef = DefDatabase<ThingDef>.GetNamed(bitschance.RandomElementByWeight(x => x.Second).First, false);
        if (bitsdef == null)
        {
            return false;
        }

        bitsyield = GetBitsYield(bitsdef, defSource, isSource, out var newdef);
        newthingdef = newdef;
        return bitsyield > 0;
    }

    public static int GetBitsYield(ThingDef def, ThingDef defSource, bool isSource, out ThingDef newdef)
    {
        var yield = 0;
        newdef = null;
        var defName = def.defName;
        if (defName != "AYCalciumCarbonate")
        {
            if (defName != "AYSalt")
            {
                return yield;
            }

            if (defSource != null)
            {
                yield = defSource.defName is "ChunkLimestone" or "ChunkMarble" or "ChunkEmperadordark"
                    or "ChunkCreoleMarble" or "ChunkEtowahMarble" or "ChunkChalk" or "ChunkLignite"
                    or "ChunkSovite" or "ChunkDacite" or "ChunkBlueschist" or "ChunkGreenSchist" or "ChunkSyenite"
                    ? Rand.Range(2, 5)
                    : Rand.Range(5, 12);
            }
            else if (isSource)
            {
                yield = Rand.Range(5, 12);
            }
        }
        else if (defSource != null)
        {
            switch (defSource.defName)
            {
                case "ChunkLimestone":
                case "ChunkMarble":
                case "ChunkEmperadordark":
                case "ChunkCreoleMarble":
                case "ChunkEtowahMarble":
                case "ChunkQuartzite":
                    yield = Rand.Range(5, 10);
                    break;
                case "ChunkChalk":
                case "ChunkSyenite":
                    yield = Rand.Range(10, 20);
                    break;
                case "ChunkSovite":
                case "ChunkSerpentinite":
                case "ChunkDacite":
                case "ChunkBlueschist":
                case "ChunkGreenSchist":
                    yield = Rand.Range(3, 6);
                    break;
                case "ChunkClayStone":
                case "ChunkDiorite":
                    yield = Rand.Range(2, 5);
                    break;
                default:
                {
                    var chknewdef = DefDatabase<ThingDef>.GetNamed("AYSalt", false);
                    if (chknewdef == null)
                    {
                        return yield;
                    }

                    newdef = chknewdef;
                    yield = Rand.Range(5, 10);
                    break;
                }
            }
        }

        return yield;
    }

    private static uint computeStringHash(string s)
    {
        uint num = 0;
        if (s == null)
        {
            return num;
        }

        num = 2166136261U;
        foreach (var c in s)
        {
            num = (c ^ num) * 16777619U;
        }

        return num;
    }

    private static bool getBitsSource(ThingDef def)
    {
        var isBitsSource = false;
        if (def == null)
        {
            return false;
        }

        var defName = def.defName;
        var num = computeStringHash(defName);
        switch (num)
        {
            case <= 1154306412U and <= 702995605U:
            {
                switch (num)
                {
                    case <= 334370396U and <= 135430043U:
                    {
                        if (num != 130326325U)
                        {
                            if (num != 135430043U)
                            {
                                return false;
                            }

                            if (defName == "ChunkBasalt")
                            {
                                isBitsSource = true;
                            }
                        }
                        else if (defName == "ChunkCreoleMarble")
                        {
                            isBitsSource = true;
                        }

                        break;
                    }
                    case <= 334370396U when num != 237089535U:
                    {
                        if (num != 334370396U)
                        {
                            return false;
                        }

                        if (defName == "ChunkBlueschist")
                        {
                            isBitsSource = true;
                        }

                        break;
                    }
                    case <= 334370396U:
                    {
                        if (defName == "ChunkGreenSchist")
                        {
                            isBitsSource = true;
                        }

                        break;
                    }
                    case <= 459180747U when num != 450668030U:
                    {
                        if (num != 459180747U)
                        {
                            return false;
                        }

                        if (defName == "ChunkLepidolite")
                        {
                            isBitsSource = true;
                        }

                        break;
                    }
                    case <= 459180747U:
                    {
                        if (defName == "ChunkAnorthosite")
                        {
                            isBitsSource = true;
                        }

                        break;
                    }
                    default:
                    {
                        if (num != 468849279U)
                        {
                            if (num != 497423552U)
                            {
                                if (num != 702995605U)
                                {
                                    return false;
                                }

                                if (defName == "ChunkDarkAndesite")
                                {
                                    isBitsSource = true;
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

                        break;
                    }
                }

                break;
            }
            case <= 990094961U:
            {
                if (num <= 803651291U)
                {
                    if (num != 754271499U)
                    {
                        if (num != 803651291U)
                        {
                            return false;
                        }

                        if (defName == "ChunkQuartzite")
                        {
                            isBitsSource = true;
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
                        if (num != 990094961U)
                        {
                            return false;
                        }

                        if (defName == "ChunkGabbro")
                        {
                            isBitsSource = true;
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

                break;
            }
            case <= 1113897122U:
            {
                if (num != 1103574401U)
                {
                    if (num != 1113897122U)
                    {
                        return false;
                    }

                    if (defName == "ChunkDacite")
                    {
                        isBitsSource = true;
                    }
                }
                else if (defName == "ChunkEtowahMarble")
                {
                    isBitsSource = true;
                }

                break;
            }
            case <= 1154306412U when num != 1117948611U:
            {
                if (num != 1138947383U)
                {
                    if (num != 1154306412U)
                    {
                        return false;
                    }

                    if (defName == "ChunkSchist")
                    {
                        isBitsSource = true;
                    }
                }
                else if (defName == "ChunkSyenite")
                {
                    isBitsSource = true;
                }

                break;
            }
            case <= 1154306412U:
            {
                if (defName == "ChunkMarble")
                {
                    isBitsSource = true;
                }

                break;
            }
            case <= 3134642644U:
                switch (num)
                {
                    case <= 2223887258U and <= 1807593693U:
                    {
                        if (num != 1789891285U)
                        {
                            if (num != 1807593693U)
                            {
                                return false;
                            }

                            if (defName == "ChunkChalk")
                            {
                                isBitsSource = true;
                            }
                        }
                        else if (defName == "ChunkMonzonite")
                        {
                            isBitsSource = true;
                        }

                        break;
                    }
                    case <= 2223887258U when num != 2078373069U:
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

                        break;
                    }
                    case <= 2223887258U:
                    {
                        if (defName == "ChunkVibrantDunite")
                        {
                            isBitsSource = true;
                        }

                        break;
                    }
                    case <= 2723944204U when num != 2277173191U:
                    {
                        if (num == 2723944204U)
                        {
                            if (defName == "ChunkClayStone")
                            {
                                isBitsSource = true;
                            }
                        }

                        break;
                    }
                    case <= 2723944204U:
                    {
                        if (defName == "ChunkScoria")
                        {
                            isBitsSource = true;
                        }

                        break;
                    }
                    default:
                    {
                        if (num != 2981327554U)
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

                        break;
                    }
                }

                break;
            case <= 3386876289U and <= 3317400192U:
            {
                if (num != 3242717934U)
                {
                    if (num != 3317400192U)
                    {
                        return false;
                    }

                    if (defName == "ChunkPegmatite")
                    {
                        isBitsSource = true;
                    }
                }
                else if (defName == "ChunkLherzolite")
                {
                    isBitsSource = true;
                }

                break;
            }
            case <= 3386876289U when num != 3349364057U:
            {
                if (num != 3354811171U)
                {
                    if (num != 3386876289U)
                    {
                        return false;
                    }

                    if (defName == "ChunkSandstone")
                    {
                        isBitsSource = true;
                    }
                }
                else if (defName == "ChunkMigmatite")
                {
                    isBitsSource = true;
                }

                break;
            }
            case <= 3386876289U:
            {
                if (defName == "ChunkCharnockite")
                {
                    isBitsSource = true;
                }

                break;
            }
            case <= 3660010543U when num != 3604908289U:
            {
                if (num != 3660010543U)
                {
                    return false;
                }

                if (defName == "ChunkEmperadordark")
                {
                    isBitsSource = true;
                }

                break;
            }
            case <= 3660010543U:
            {
                if (defName == "ChunkJaspillite")
                {
                    isBitsSource = true;
                }

                break;
            }
            default:
            {
                if (num != 3782560473U)
                {
                    if (num != 4006516401U)
                    {
                        if (num != 4282638488U)
                        {
                            return false;
                        }

                        if (defName == "ChunkLimestone")
                        {
                            isBitsSource = true;
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

                break;
            }
        }

        return isBitsSource;
    }
}