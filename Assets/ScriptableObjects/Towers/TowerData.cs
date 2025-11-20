using UnityEngine;
using SimpleMOBA.Gameplay.Towers;

namespace SimpleMOBA.ScriptableObjects.Towers
{
    /// <summary>
    /// Configurable stats for tower prefabs.
    /// </summary>
    [CreateAssetMenu(menuName = "SimpleMOBA/TowerData")]
    public class TowerData : ScriptableObject
    {
        public TowerController TowerPrefab;
        public float MaxHealth = 500f;
        public float AttackRange = 6f;
        public float AttackDamage = 15f;
        public float AttackCooldown = 1f;
    }
}
