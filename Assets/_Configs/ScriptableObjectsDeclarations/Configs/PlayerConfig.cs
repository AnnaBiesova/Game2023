using System;
using _Scripts.Patterns;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace _Configs.ScriptableObjectsDeclarations.Configs
{
    [Serializable]
    [CreateAssetMenu(fileName = "Player Movement Config", menuName = "Pawsome/Gameplay/Player Config", order = 1)]
    public class PlayerConfig : SingletonScriptableObject<PlayerConfig>
    {
        
    }
}
