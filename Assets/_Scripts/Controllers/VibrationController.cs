using _Scripts.Patterns;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Controllers
{
    public sealed class VibrationController : Singleton<VibrationController>
    {
        [FormerlySerializedAs("lightHapticDelayOnHandleRope")] [SerializeField] private float lightHapticDelay = 0.1f;

        private float lastLightHaptick;

        private void Awake()
        {
            //Taptic.tapticOn = SettingsController.SavedData.vibrationEnabled;
        }

        public void LightHapticWithCooldown()
        {
            if (lastLightHaptick + lightHapticDelay > Time.time) return;
            
            lastLightHaptick = Time.time;   
            Light();
        }
        
        public void Warning() => Taptic.Warning();
        public void Failure() => Taptic.Failure();
        public void Success() => Taptic.Success();
        public void Light() => Taptic.Light();
        public void Medium() => Taptic.Medium();
        public void Heavy() => Taptic.Heavy();
        public void Default() => Taptic.Default();
        public void Vibrate() => Taptic.Vibrate();
        public void Selection() => Taptic.Selection();

    }
}
