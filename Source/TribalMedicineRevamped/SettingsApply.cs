using Verse;
using ArgonicCore.Comps;

namespace TribalMedicineRevamped
{
    [StaticConstructorOnStartup]
    public static class SettingsApply
    {
        private static bool ModSettings_AltNames => LoadedModManager.GetMod<TribalMedicineMod>().GetSettings<TribalMedicine_ModSettings>().EnableAltNames;
        private static bool ModSettings_BrootPoisoning => LoadedModManager.GetMod<TribalMedicineMod>().GetSettings<TribalMedicine_ModSettings>().EnableBrootPoisoning;

        static SettingsApply()
        {
            if (ModSettings_AltNames)
            {
                DefDatabase<ThingDef>.GetNamed("TM_BlackTea").label = "Immuni-tea";
            }
            if (ModSettings_BrootPoisoning)
            {
                CompProperties_Contaminable comp = DefDatabase<ThingDef>.GetNamed("TM_Broots").comps.Find(x => x.compClass == typeof(CompContaminable)) as CompProperties_Contaminable;
                if (comp != null)
                {
                    comp.contaminesProducts = false;
                    comp.isBaseContaminator = false;
                    comp.contaminationPower = 0f;
                }
            }
        }
    }
}