using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Patterns.EntitiesManager
{
    [AutoCreateSingelton]
    public class EntitiesManager : Singleton<EntitiesManager>
    {
        private void OnDestroy()
        {
            enteties.Clear();
        }

        private static Dictionary<string, EntityItem> enteties = new Dictionary<string, EntityItem>();

        public static EntityItem Get(string EntityID) => enteties.ContainsKey(EntityID) ? enteties[EntityID] : null;
        public static T GetAs<T>(string EntityID) where T : Component
        {
            if (enteties.TryGetValue(EntityID, out var go))
            {
                return GetComponentFromEntity<T>(go);
            }
            
            Debug.LogWarning($"Can't find registered entity with a EntityID = {EntityID}, searching in scene.");

            EntityItem searchResult = GameObject.
                FindObjectsByType<EntityItem>(FindObjectsInactive.Include, FindObjectsSortMode.None).
                FirstOrDefault(e => string.Equals(e.Id, EntityID, StringComparison.OrdinalIgnoreCase));

            if (searchResult != null)
            {
                return GetComponentFromEntity<T>(searchResult);
            }

            Debug.LogError($"Can't find entity with a EntityID = {EntityID} in scene. Null returned");
            return null;
        }

        private static T GetComponentFromEntity<T>(EntityItem from)
        {
            if (from.TryGetComponent(out T comp))
            {
                return comp;
            }

            Debug.LogWarning($"Can't find component {nameof(T)} on a {from.name}");
            return default(T);
        }

        public void Register(EntityItem entityItem)
        {
            if (EntitiesManager.enteties.ContainsKey(entityItem.Id))
                Debug.LogWarning("Collection already contains " + entityItem.Id + "!", entityItem);
            else enteties.Add(entityItem.Id, entityItem);
        }
    }
}