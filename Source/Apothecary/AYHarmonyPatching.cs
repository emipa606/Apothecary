using System.Reflection;
using HarmonyLib;
using Verse;

namespace Apothecary
{
    // Token: 0x02000007 RID: 7
    [StaticConstructorOnStartup]
    internal static class AYHarmonyPatching
    {
        // Token: 0x06000016 RID: 22 RVA: 0x00002EBB File Offset: 0x000010BB
        static AYHarmonyPatching()
        {
            new Harmony("com.Pelador.Rimworld.Apothecary").PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}