using Verse;

namespace Apothecary
{
    // Token: 0x02000011 RID: 17
    public class CompProperties_AYCompostBin : CompProperties
    {
        // Token: 0x0400003E RID: 62
        public int fermentTicks;

        // Token: 0x0400003B RID: 59
        public int numProductOne;

        // Token: 0x0400003D RID: 61
        public int numProductTwo;

        // Token: 0x0400003A RID: 58
        public string productOne;

        // Token: 0x0400003C RID: 60
        public string productTwo;

        // Token: 0x06000033 RID: 51 RVA: 0x000037FC File Offset: 0x000019FC
        public CompProperties_AYCompostBin()
        {
            compClass = typeof(CompAYCompostBin);
        }
    }
}