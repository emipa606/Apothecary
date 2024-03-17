using System;
using System.Text;
using RimWorld;
using Verse;

namespace Apothecary;

public class Building_AYCompostBin : Building
{
    public static readonly string AYCharcoalKiln = "AYCharcoalKiln";

    private CompAYCompostBin compostBinComp;

    private int fermentTicks;

    private int TargetTicks => compostBinComp.Props.fermentTicks;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref fermentTicks, "fermentTicks");
    }

    public override void SpawnSetup(Map currentGame, bool respawningAfterLoad)
    {
        base.SpawnSetup(currentGame, respawningAfterLoad);
        compostBinComp = GetComp<CompAYCompostBin>();
    }

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

    public override string GetInspectString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(base.GetInspectString());
        if (stringBuilder.Length != 0)
        {
            stringBuilder.AppendLine();
        }

        stringBuilder.AppendLine("AY.Progress".Translate() + " " +
                                 (fermentTicks / (float)TargetTicks * 100f).ToString("#0.00") + "%");
        return stringBuilder.ToString().TrimEndNewlines();
    }
}