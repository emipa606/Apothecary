using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace Apothecary;

public class AYWashUtility
{
    public static Thing AYWashApparel(ref Thing ingredient, Map map, bool loseQual)
    {
        var CQ = ingredient.TryGetComp<CompQuality>();
        if (CQ != null)
        {
            var quality = CQ.Quality;
            if (loseQual)
            {
                switch (CQ.Quality)
                {
                    case QualityCategory.Awful:
                        quality = QualityCategory.Awful;
                        break;
                    case QualityCategory.Poor:
                        quality = QualityCategory.Awful;
                        break;
                    case QualityCategory.Normal:
                        quality = QualityCategory.Poor;
                        break;
                    case QualityCategory.Good:
                        quality = QualityCategory.Normal;
                        break;
                    case QualityCategory.Excellent:
                        quality = QualityCategory.Good;
                        break;
                    case QualityCategory.Masterwork:
                        quality = QualityCategory.Excellent;
                        break;
                    case QualityCategory.Legendary:
                        quality = QualityCategory.Masterwork;
                        break;
                    default:
                        quality = QualityCategory.Normal;
                        break;
                }
            }

            CQ.SetQuality(quality, ArtGenerationContext.Colony);
        }

        NonPublicFields.wornCorpse.SetValue(ingredient, false);
        return ingredient;
    }

    public static void AYWashHaulJob(Thing t)
    {
        if (!t.Spawned)
        {
            return;
        }

        var tub = t.Position.GetFirstBuilding(t.Map);
        if (tub == null || tub.def.defName != "AYApparelWashingTub")
        {
            return;
        }

        var p = tub.InteractionCell.GetFirstPawn(t.Map);
        if (p == null)
        {
            return;
        }

        var jobs = p.jobs;
        JobDef jobDef;
        if (jobs == null)
        {
            jobDef = null;
        }
        else
        {
            var curJob = jobs.curJob;
            jobDef = curJob?.def;
        }

        if (jobDef != JobDefOf.DoBill)
        {
            return;
        }

        var newHaul = HaulAIUtility.HaulToStorageJob(p, t, true);
        if (newHaul != null)
        {
            p.jobs.jobQueue.EnqueueFirst(newHaul, JobTag.MiscWork);
        }
    }

    public static class NonPublicFields
    {
        public static readonly FieldInfo wornCorpse = AccessTools.Field(typeof(Apparel), "wornByCorpseInt");
    }
}