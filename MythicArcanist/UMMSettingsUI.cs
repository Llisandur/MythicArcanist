using TabletopTweaks.Core.UMMTools;
using UnityModManagerNet;

namespace MythicArcanist
{
    internal static class UMMSettingsUI
    {
        private static int selectedTab;
        public static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            UI.AutoWidth();
            UI.TabBar(ref selectedTab,
                    () => UI.Label("SETTINGS WILL NOT BE UPDATED UNTIL YOU RESTART YOUR GAME.".yellow().bold()),
                    new NamedAction("Added Content", () => SettingsTabs.AddedContent()),
                    new NamedAction("Homebrew", () => SettingsTabs.Homebrew()),
                    new NamedAction("Third Party Publisher", () => SettingsTabs.ThirdParty())
            );
        }
    }

    static class SettingsTabs
    {
        public static void AddedContent()
        {
            var TabLevel = SetttingUI.TabLevel.Zero;
            var AddedContent = Main.ThisModContext.AddedContent;
            UI.Div(0, 15);
            using (UI.VerticalScope())
            {
                UI.Toggle("New Settings Off By Default".bold(), ref AddedContent.NewSettingsOffByDefault);
                UI.Space(25);

                SetttingUI.SettingGroup("Archetypes", TabLevel, AddedContent.Archetypes);
            }
        }
        public static void Homebrew()
        {
            var TabLevel = SetttingUI.TabLevel.Zero;
            var Homebrew = Main.ThisModContext.Homebrew;
            UI.Div(0, 15);
            using (UI.VerticalScope())
            {
                UI.Toggle("New Settings Off By Default".bold(), ref Homebrew.NewSettingsOffByDefault);
                UI.Space(25);

                SetttingUI.SettingGroup("Mythic Abilties", TabLevel, Homebrew.MythicAbilities);
            }
        }
        public static void ThirdParty()
        {
            var TabLevel = SetttingUI.TabLevel.Zero;
            var ThirdParty = Main.ThisModContext.ThirdParty;
            UI.Div(0, 15);
            using (UI.VerticalScope())
            {
                UI.Toggle("New Settings Off By Default".bold(), ref ThirdParty.NewSettingsOffByDefault);
                UI.Space(25);

                SetttingUI.SettingGroup("Spells", TabLevel, ThirdParty.Spells);
                SetttingUI.SettingGroup("Arcanist Exploits", TabLevel, ThirdParty.ArcanistExploits);
            }
        }
    }
}
