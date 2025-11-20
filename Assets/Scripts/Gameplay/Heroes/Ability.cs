using UnityEngine;

namespace SimpleMOBA.Gameplay.Heroes
{
    /// <summary>
    /// Base class for hero abilities with cooldown handling.
    /// </summary>
    public abstract class Ability : MonoBehaviour
    {
        [SerializeField] private string abilityName = "Ability";
        [SerializeField] private float cooldownSeconds = 5f;

        private float lastCastTime = -999f;

        public string AbilityName => abilityName;
        public float CooldownSeconds => cooldownSeconds;
        public float TimeSinceCast => Time.time - lastCastTime;
        public bool IsReady => TimeSinceCast >= cooldownSeconds;

        public bool TryCast()
        {
            if (!IsReady)
            {
                return false;
            }

            if (OnCast())
            {
                lastCastTime = Time.time;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Executes the ability. Return true if activation succeeded.
        /// </summary>
        protected abstract bool OnCast();
    }
}
