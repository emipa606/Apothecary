using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Apothecary;

public static class AYPlantUtility
{
    public static void AddYield(Pawn pawn, Plant plant)
    {
        if (!AYResearch.AYHerbsYield.IsFinished || !isAyPlant(plant))
        {
            return;
        }

        var def = plant.def;
        ThingDef thingDef;
        if (def == null)
        {
            thingDef = null;
        }
        else
        {
            var plant2 = def.plant;
            thingDef = plant2?.harvestedThingDef;
        }

        var harvDef = thingDef;
        if (harvDef == null)
        {
            return;
        }

        var harvNum = plant.def.plant.harvestYield;
        if (!(harvNum > 0f))
        {
            return;
        }

        var addyield = getAddYield(pawn, plant, harvNum);
        if (addyield > 0f)
        {
            genAddYield(pawn, harvDef, addyield);
        }
    }

    private static bool isAyPlant(Plant plant)
    {
        if (plant == null)
        {
            return false;
        }

        var def = plant.def;
        bool b;
        if (def == null)
        {
            b = false;
        }
        else
        {
            var plant2 = def.plant;
            b = plant2?.sowResearchPrerequisites != null;
        }

        if (!b)
        {
            return false;
        }

        var def2 = plant.def;
        List<ResearchProjectDef> list;
        if (def2 == null)
        {
            list = null;
        }
        else
        {
            var plant3 = def2.plant;
            list = plant3?.sowResearchPrerequisites;
        }

        var plantResearch = list;
        switch (plantResearch)
        {
            case { Count: <= 0 }:
            case null:
                return false;
        }

        foreach (var researchProjectDef in plantResearch)
        {
            if (researchProjectDef.defName.StartsWith("AYHerbs"))
            {
                return true;
            }
        }

        return false;
    }

    private static float getAddYield(Pawn pawn, Plant plant, float yield)
    {
        if (pawn == null || plant == null)
        {
            return 0f;
        }

        var skillRatio = 0.5f;
        if (pawn.skills == null)
        {
            return yield * plant.Growth * skillRatio * 0.4f;
        }

        skillRatio = (pawn.skills.GetSkill(SkillDefOf.Plants).levelInt + 5) / (float)20;
        if (skillRatio > 1f)
        {
            skillRatio = 1f;
        }

        if (skillRatio < 0.5f)
        {
            skillRatio = 0.5f;
        }

        return yield * plant.Growth * skillRatio * 0.4f;
    }

    private static void genAddYield(Pawn pawn, ThingDef harvDef, float yield)
    {
        var thing = ThingMaker.MakeThing(harvDef);
        thing.stackCount = checked((int)yield);
        if (thing.stackCount > 0)
        {
            GenPlace.TryPlaceThing(thing, pawn.Position, pawn.Map, ThingPlaceMode.Near);
        }
    }
}