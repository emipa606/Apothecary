using System;
using System.Text;
using RimWorld;
using Verse;

namespace Apothecary
{
    // Token: 0x0200000F RID: 15
    public class Building_AYCompostBin : Building
    {
        // Token: 0x04000039 RID: 57
        public static string AYCharcoalKiln = "AYCharcoalKiln";

        // Token: 0x04000037 RID: 55
        private CompAYCompostBin compostBinComp;

        // Token: 0x04000038 RID: 56
        private int fermentTicks;

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000029 RID: 41 RVA: 0x00003510 File Offset: 0x00001710
        private int TargetTicks => compostBinComp.Props.fermentTicks;

        // Token: 0x0600002A RID: 42 RVA: 0x00003522 File Offset: 0x00001722
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref fermentTicks, "fermentTicks");
        }

        // Token: 0x0600002B RID: 43 RVA: 0x0000353C File Offset: 0x0000173C
        public override void SpawnSetup(Map currentGame, bool respawningAfterLoad)
        {
            base.SpawnSetup(currentGame, respawningAfterLoad);
            compostBinComp = GetComp<CompAYCompostBin>();
        }

        // Token: 0x0600002C RID: 44 RVA: 0x00003554 File Offset: 0x00001754
        public override void TickRare()
        {
            base.TickRare();
            if (fermentTicks < TargetTicks)
            {
                fermentTicks++;
                var heat = 4f;
                if (def.defName == AYCharcoalKiln)
                {
                    FleckMaker.ThrowSmoke(Position.ToVector3(), Map, 1f);
                    heat = 12f;
                }

                GenTemperature.PushHeat(Position, Map, heat);
            }

            if (fermentTicks >= TargetTicks)
            {
                PlaceProduct();
            }
        }

        // Token: 0x0600002D RID: 45 RVA: 0x000035EC File Offset: 0x000017EC
        public void PlaceProduct()
        {
            var position = Position;
            var map = Map;
            var thing = ThingMaker.MakeThing(ThingDef.Named(compostBinComp.Props.productOne));
            thing.stackCount = compostBinComp.Props.numProductOne;
            GenPlace.TryPlaceThing(thing, position, map, ThingPlaceMode.Near);
            var thing2 = ThingMaker.MakeThing(ThingDef.Named(compostBinComp.Props.productTwo));
            thing2.stackCount = compostBinComp.Props.numProductTwo;
            GenPlace.TryPlaceThing(thing2, position, map, ThingPlaceMode.Near);
            if (def.defName != AYCharcoalKiln)
            {
                var random = new Random();
                var Chance = random.Next(3);
                var num = random.Next(3);
                if (Chance < 2)
                {
                    GenPlace.TryPlaceThing(ThingMaker.MakeThing(ThingDef.Named("WoodLog")), position, map,
                        ThingPlaceMode.Near);
                }

                if (num < 1)
                {
                    GenPlace.TryPlaceThing(ThingMaker.MakeThing(ThingDef.Named("WoodLog")), position, map,
                        ThingPlaceMode.Near);
                }
            }

            ThingDef prevStuff = null;
            if (Stuff != null)
            {
                prevStuff = Stuff;
            }

            Destroy();
            GenConstruct.PlaceBlueprintForBuild(ThingDef.Named(def.defName), position, map, Rot4.North,
                Faction.OfPlayer, prevStuff);
        }

        // Token: 0x0600002E RID: 46 RVA: 0x00003748 File Offset: 0x00001948
        public override string GetInspectString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString());
            if (stringBuilder.Length != 0)
            {
                stringBuilder.AppendLine();
            }

            stringBuilder.AppendLine("AY.Progress".Translate() + " " +
                                     (fermentTicks / (float) TargetTicks * 100f).ToString("#0.00") + "%");
            return stringBuilder.ToString().TrimEndNewlines();
        }
    }
}