using TabletopTweaks.Core.Config;

namespace MythicArcanist.Config
{
    public class AddedContent : IUpdatableSettings
    {
        public bool NewSettingsOffByDefault = false;
        public SettingGroup Archetypes = new SettingGroup();

        public void Init()
        {
            //MythicReworks.Init();
        }

        public void OverrideSettings(IUpdatableSettings userSettings)
        {
            var loadedSettings = userSettings as AddedContent;
            NewSettingsOffByDefault = loadedSettings.NewSettingsOffByDefault;
            Archetypes.LoadSettingGroup(loadedSettings.Archetypes, NewSettingsOffByDefault);
        }
    }
    public class Homebrew : IUpdatableSettings
    {
        public bool NewSettingsOffByDefault = false;
        public SettingGroup MythicAbilities = new SettingGroup();

        public void Init()
        {
            //MythicReworks.Init();
        }

        public void OverrideSettings(IUpdatableSettings userSettings)
        {
            var loadedSettings = userSettings as Homebrew;
            NewSettingsOffByDefault = loadedSettings.NewSettingsOffByDefault;
            MythicAbilities.LoadSettingGroup(loadedSettings.MythicAbilities, NewSettingsOffByDefault);
        }
    }
    public class ThirdParty : IUpdatableSettings
    {
        public bool NewSettingsOffByDefault = false;
        public SettingGroup Spells = new SettingGroup();
        public SettingGroup ArcanistExploits = new SettingGroup();

        public void Init()
        {
            //MythicReworks.Init();
        }

        public void OverrideSettings(IUpdatableSettings userSettings)
        {
            var loadedSettings = userSettings as ThirdParty;
            NewSettingsOffByDefault = loadedSettings.NewSettingsOffByDefault;
            Spells.LoadSettingGroup(loadedSettings.Spells, NewSettingsOffByDefault);
            ArcanistExploits.LoadSettingGroup(loadedSettings.ArcanistExploits, NewSettingsOffByDefault);
        }
    }
}
