using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace Apothecary
{
    // Token: 0x02000017 RID: 23
    [HarmonyPatch(typeof(HealthAIUtility))]
    [HarmonyPatch("FindBestMedicine")]
    public class HealthAIUtility_FindBestMedicine_PostPatch
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003B8C File Offset: 0x00001D8C
        [HarmonyPriority(0)]
        public static void Postfix(Pawn healer, Pawn patient, ref Thing __result)
        {
            if (ModLister.HasActiveModWithName("Medical Supplements") || !Controller.Settings.RealismBandages ||
                __result == null || !IsBandage(__result.def) || BandagesValid(patient))
            {
                return;
            }

            var medPot = __result.def.GetStatValueAbstract(StatDefOf.MedicalPotency);
            __result = GenClosest.ClosestThing_Global_Reachable(patient.Position, patient.Map,
                patient.Map.listerThings.ThingsInGroup(ThingRequestGroup.Medicine), PathEndMode.ClosestTouch,
                TraverseParms.For(healer), 9999f,
                m => !m.IsForbidden(healer) && healer.CanReserve(m) && !IsBandage(m.def) &&
                     m.def.GetStatValueAbstract(StatDefOf.MedicalPotency) <= medPot,
                m => m.def.GetStatValueAbstract(StatDefOf.MedicalPotency));
        }

        // Token: 0x06000042 RID: 66 RVA: 0x00003C78 File Offset: 0x00001E78
        public static bool BandagesValid(Pawn pawn)
        {
            HediffSet hediffSet;
            if (pawn == null)
            {
                hediffSet = null;
            }
            else
            {
                var health = pawn.health;
                hediffSet = health?.hediffSet;
            }

            var hedset = hediffSet;

            var injuries = hedset?.GetHediffsTendable().ToList();
            if (injuries == null || injuries.Count <= 0)
            {
                return false;
            }

            foreach (var injury in injuries)
            {
                if (!(injury is Hediff_Injury) && !Inclusions().Contains(injury.def.defName))
                {
                    return false;
                }

                if (injury is Hediff_Injury && injury.Part.depth == BodyPartDepth.Inside)
                {
                    return false;
                }
            }

            return true;
        }

        // Token: 0x06000043 RID: 67 RVA: 0x00003D3C File Offset: 0x00001F3C
        public static bool IsBandage(ThingDef def)
        {
            return def.IsMedicine && Bandages().Contains(def.defName);
        }

        // Token: 0x06000044 RID: 68 RVA: 0x00003D5B File Offset: 0x00001F5B
        public static List<string> Bandages()
        {
            var list = new List<string>();
            list.AddDistinct("MSBandage");
            list.AddDistinct("MSASBandage");
            list.AddDistinct("MSNanoBandage");
            list.AddDistinct("AYYarrowBandage");
            list.AddDistinct("Bandagekit");
            return list;
        }

        // Token: 0x06000045 RID: 69 RVA: 0x00003D9C File Offset: 0x00001F9C
        public static List<string> Inclusions()
        {
            var list = new List<string>();
            list.AddDistinct("CA_Knick_Hand");
            list.AddDistinct("CA_Knick_Arm");
            list.AddDistinct("CA_Knick_Foot");
            list.AddDistinct("CA_Knick_Leg");
            list.AddDistinct("CA_Sprain_Hand");
            list.AddDistinct("CA_Sprain_Arm");
            list.AddDistinct("CA_Sprain_Foot");
            list.AddDistinct("CA_Sprain_Leg");
            return list;
        }
    }
}