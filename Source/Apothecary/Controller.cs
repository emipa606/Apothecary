using Mlie;
using UnityEngine;
using Verse;

namespace Apothecary;

public class Controller : Mod
{
    public static Settings Settings;
    public static string CurrentVersion;

    public Controller(ModContentPack content) : base(content)
    {
        Settings = GetSettings<Settings>();
        CurrentVersion =
            VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    public override string SettingsCategory()
    {
        return "AY.Name".Translate();
    }

    public override void DoSettingsWindowContents(Rect canvas)
    {
        Settings.DoWindowContents(canvas);
    }
}