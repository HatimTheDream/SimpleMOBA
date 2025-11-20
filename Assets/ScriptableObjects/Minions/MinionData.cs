using UnityEngine;
using SimpleMOBA.Gameplay.Minions;

namespace SimpleMOBA.ScriptableObjects.Minions
{
    /// <summary>
    /// Configurable stats for minion prefabs.
    /// </summary>
    [CreateAssetMenu(menuName = "SimpleMOBA/MinionData")]
    public class MinionData : ScriptableObject
    {
        public MinionController MinionPrefab;
        public float MaxHealth = 100f;
        public float MoveSpeed = 2f;
        public float AttackDamage = 5f;
        public float AttackRange = 2f;
        public float AttackCooldown = 1.5f;
    }
}
