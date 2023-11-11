using System.Collections.Generic;
using System.Linq;
using _Scripts.Patterns.Events;

namespace _Scripts.Services
{
    public static class PrefsNames
    {
        public static string MAIN_DATA_SAVE = "MAIN_DATA";
        public static string PLAYER_DATA_SAVE = "PLAYER_DATA_SAVE";
        public static string LEVEL_MANAGER_SAVE = "LEVEL_MANAGER_SAVE";
        public static string LAST_LEVEL_CONFIG = "LAST_LEVEL_CONFIG";
        public static string SETTINGS_SAVE = "SETTINGS_SAVE";
        public static string TUTORIAL_COMPLETED = "TUTORIAL_COMPLETED";
        public static string UNLOCKED_ARTIFACTS_SAVE = "UNLOCKED_ARTIFACTS_SAVE";
        public static string COLLECTED_FIRST_ARTIFACT = "COLLECTED_FIRST_ARTIFACT";
        public static string LAST_SESSION_TIME = "LAST_SESSION_TIME";
    }

    public static class DebugEnableableFeaturesPrefsNames
    {
        public static string DEBUG_SHOW_FPS_COUNTER = "DEBUG_SHOW_FPS_COUNTER";
        public static string DEBUG_SCROLL_LEVELS = "DEBUG_SCROLL_LEVELS";
        public static string DEBUG_SHOW_CONSOLE = "DEBUG_SHOW_CONSOLE";
        public static string DEBUG_ADD_MONEY = "DEBUG_ADD_MONEY";
        public static string DEBUG_RESET_PLAYER_CONFGIS = "DEBUG_RESET_PLAYER_CONFGIS";
        public static string DEBUG_RUNTIME_INSPECTOR = "DEBUG_RUNTIME_INSPECTOR";
        public static string DEBUG_HIDE_UI = "DEBUG_HIDE_UI";
        public static string DEBUG_PANEL = "DEBUG_PANEL";
        public static string DEBUG_ENABLE_TEST_SUITE = "DEBUG_ENABLE_TEST_SUITE";
        public static string DEBUG_WINDOW_UNLOCKED = "DEBUG_WINDOW_UNLOCKED";
        public static string DEBUG_SET_NEXT_ACTIVITY = "DEBUG_SET_NEXT_ACTIVITY";
        public static string DEBUG_SET_NEXT_ACTIVITY_IGNORES_LEVELS = "DEBUG_SET_NEXT_ACTIVITY_IGNORES_LEVELS";
        public static string DEBUG_SHOW_CURRENT_ACTIVITY_TEXT = "DEBUG_SHOW_CURRENT_ACTIVITY_TEXT";

        public static IEnumerable<string> GetAllDebugFeaturesNames()
        {
            return typeof(DebugEnableableFeaturesPrefsNames).GetFields().Select(fieldInfo => fieldInfo.Name);
        }
    }

    public static class DebugInternalSelfControlledPrefsNames
    {
        public static string DEBUG_LOAD_DEBUG_LEVEL = "DEBUG_LOAD_DEBUG_LEVEL";
    }
}
