using System;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace Apothecary
{
    // Token: 0x02000021 RID: 33
    [HarmonyPatch(typeof(JobDriver_PlantHarvest), "PlantWorkDoneToil", null)]
    public static class JobDriver_PlantHarvest_PlantWorkDoneToil
    {
        // Token: 0x06000064 RID: 100 RVA: 0x000049AC File Offset: 0x00002BAC
        private static void Postfix(ref Toil __result, ref JobDriver_PlantHarvest __instance)
        {
            var unused = __instance.pawn.Map;
            var actor = __instance.pawn;
            __result.initAction = (Action) Delegate.Combine(__result.initAction, new Action(delegate
            {
                var plant = (Plant) actor.jobs.curJob.GetTarget(TargetIndex.A).Thing;
                AYPlantUtility.AddYield(actor, plant);
                if (plant.def.defName == "Plant_AYGinkgoBiloba")
                {
                    var skillRatio = 0.5f;
                    if (actor.skills != null)
                    {
                        skillRatio = (actor.skills.GetSkill(SkillDefOf.Plants).levelInt + 5) / (float) 20;
                        if (skillRatio > 1f)
                        {
                            skillRatio = 1f;
                        }

                        if (skillRatio < 0.5f)
                        {
                            skillRatio = 0.5f;
                        }
                    }

                    var thing = ThingMaker.MakeThing(ThingDef.Named("AYGinkgoLeaves"));
                    var num = plant.def.plant.harvestYield * 2f * plant.Growth * skillRatio;
                    num = AdjustYield(num);
                    thing.stackCount = checked((int) num);
                    if (thing.stackCount > 0)
                    {
                        GenPlace.TryPlaceThing(thing, actor.Position, actor.Map, ThingPlaceMode.Near);
                    }
                }
                else if (plant.def.defName == "BurnedTree")
                {
                    var skillRatio2 = 0.5f;
                    var actor2 = actor;
                    if (actor2.skills != null)
                    {
                        skillRatio2 = (actor.skills.GetSkill(SkillDefOf.Plants).levelInt + 5) / (float) 20;
                        if (skillRatio2 > 1f)
                        {
                            skillRatio2 = 1f;
                        }

                        if (skillRatio2 < 0.5f)
                        {
                            skillRatio2 = 0.5f;
                        }
                    }

                    var def = ThingDef.Named("AYCharcoal");
                    var defTwo = ThingDef.Named("AYWoodAshes");
                    var thingOne = ThingMaker.MakeThing(def);
                    var thingTwo = ThingMaker.MakeThing(defTwo);
                    var numOne = 10f * skillRatio2;
                    var numTwo = 20f * skillRatio2;
                    numOne = AdjustYield(numOne);
                    numTwo = AdjustYield(numTwo);
                    checked
                    {
                        thingOne.stackCount = (int) numOne;
                        thingTwo.stackCount = (int) numTwo;
                        if (thingOne.stackCount > 0)
                        {
                            GenPlace.TryPlaceThing(thingOne, actor.Position, actor.Map, ThingPlaceMode.Near);
                        }

                        if (thingTwo.stackCount > 0)
                        {
                            GenPlace.TryPlaceThing(thingTwo, actor.Position, actor.Map, ThingPlaceMode.Near);
                        }
                    }
                }
            }));
        }

        // Token: 0x06000065 RID: 101 RVA: 0x000049FD File Offset: 0x00002BFD
        public static float AdjustYield(float num)
        {
            num *= Rand.Range(0.8f, 1.2f);
            return num;
        }
    }
}