using System.Reflection;
using HarmonyLib;
using Verse;

namespace Apothecary;

[StaticConstructorOnStartup]
internal static class AYHarmonyPatching
{
    static AYHarmonyPatching()
    {
        new Harmony("com.Pelador.Rimworld.Apothecary").PatchAll(Assembly.GetExecutingAssembly());
    }
}