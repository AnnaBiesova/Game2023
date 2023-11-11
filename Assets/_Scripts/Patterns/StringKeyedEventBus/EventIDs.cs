using System.Collections.Generic;
using System.Linq;

namespace _Scripts.Patterns.Events
{
    public static class EventID
    {
        public static string LEVEL_START = "LEVEL_START";
        public static string LEVEL_FAIL = "LEVEL_FAIL";
        public static string LEVEL_DONE = "LEVEL_DONE";
        public static string LEVEL_CONTINUE = "LEVEL_CONTINUE";
        public static string LEVEL_STAGE_STARTED = "LEVEL_STAGE_STARTED";
        public static string LEVEL_STAGE_FAILED = "LEVEL_STAGE_FAILED";
        public static string LEVEL_STAGE_COMPLETED = "LEVEL_STAGE_COMPLETED";
        public static string PLAYER_TRY_BUY_SOMETHING_WITHOUT_MONEY = "PLAYER_TRY_BUY_SOMETHING_WITHOUT_MONEY";
        public static string PLAYER_TAP_ON_WORLD_SPACE_MONEY = "PLAYER_TAP_ON_WORLD_SPACE_MONEY";
        public static string INPUT = "INPUT";
        public static string SHADED_DATA_REQUEST = "SHARED_DATA_REQUEST";
        public static string PLAYER_MONEY_CHANGED = "PLAYER_MONEY_CHANGED";
        public static string BOSS_FIGHT_STARTED = "BOSS_FIGHT_STARTED";
        public static string TRAP_CARD_UPGRADED = "TRAP_CARD_UPGRADED";


        public static IEnumerable<string> GetAllEventsNames()
        {
            return typeof(EventID).GetFields().Select(fieldInfo => fieldInfo.Name);
        }
    }
}
