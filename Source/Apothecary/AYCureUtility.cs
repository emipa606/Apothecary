using System.Collections.Generic;
using Verse;

namespace Apothecary;

public class AYCureUtility
{
    internal static bool ImmuneTo(Pawn pawn, HediffDef def, out List<string> Immunities)
    {
        Immunities = new List<string>();
        var immune = false;
        var hediffs = pawn.health.hediffSet.hediffs;
        foreach (var hediff in hediffs)
        {
            var curStage = hediff.CurStage;
            if (curStage?.makeImmuneTo == null)
            {
                continue;
            }


            foreach (var hediffDef in curStage.makeImmuneTo)
            {
                if (hediffDef != def)
                {
                    continue;
                }

                Immunities.AddDistinct(hediff.def.defName);
                immune = true;
            }
        }

        return immune;
    }
}