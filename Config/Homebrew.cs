using TabletopTweaks.Core.Config;

namespace MythicArcanist.Config
{
    public class Homebrew : IUpdatableSettings
    {
        public bool NewSettingsOffByDefault = false;
        //public SettingGroup Feats = new SettingGroup();
        public SettingGroup MythicAbilities = new SettingGroup();
        //public SettingGroup MythicFeats = new SettingGroup();

        public void Init()
        {
            //MythicReworks.Init();
        }

        public void OverrideSettings(IUpdatableSettings userSettings)
        {
            var loadedSettings = userSettings as Homebrew;
            NewSettingsOffByDefault = loadedSettings.NewSettingsOffByDefault;
            MythicAbilities.LoadSettingGroup(loadedSettings.MythicAbilities, NewSettingsOffByDefault);
            //MythicFeats.LoadSettingGroup(loadedSettings.MythicFeats, NewSettingsOffByDefault);
            //Feats.LoadSettingGroup(loadedSettings.Feats, NewSettingsOffByDefault);
        }
    }
}
