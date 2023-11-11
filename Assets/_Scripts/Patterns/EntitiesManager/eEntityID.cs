using System.Collections.Generic;
using System.Linq;

namespace _Scripts.Patterns.EntitiesManager
{
    public static class EntityID
    {
        public static string ARM_AIM_TARGET = "ARM_AIM_TARGET";
        public static string CAMERA_MAIN = "CAMERA_MAIN";
        public static string CANVAS_MAIN = "CANVAS_MAIN";
        public static string COLLECTED_OBJECT_LOOK_AT_PARENT = "COLLECTED_OBJECT_LOOK_AT_PARENT";
        
        //Buildings
        public static string STORAGE_BUILDING = "STORAGE_BUILDING";
        public static string ELEVATOR_BUILDING = "ELEVATOR_BUILDING";
        
        
        public static IEnumerable<string> GetAllEntitiesNames()
        {
            return typeof(EntityID).GetFields().Select(fieldInfo => fieldInfo.Name);
        }
    }
}