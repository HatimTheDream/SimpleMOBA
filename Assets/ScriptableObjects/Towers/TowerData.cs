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
        [Header("Prefabs")]
        [SerializeField] private TowerController towerPrefab;

        [Header("Stats")]
        [SerializeField, Min(0f)] private float maxHealth = 500f;
        [SerializeField, Min(0f)] private float attackRange = 6f;
        [SerializeField, Min(0f)] private float attackDamage = 15f;
        [SerializeField, Min(0f)] private float attackCooldown = 1f;

        public TowerController TowerPrefab => towerPrefab;
        public float MaxHealth => maxHealth;
        public float AttackRange => attackRange;
        public float AttackDamage => attackDamage;
        public float AttackCooldown => attackCooldown;

        private void OnValidate()
        {
            maxHealth = Mathf.Max(0f, maxHealth);
            attackRange = Mathf.Max(0f, attackRange);
            attackDamage = Mathf.Max(0f, attackDamage);
            attackCooldown = Mathf.Max(0f, attackCooldown);
        }
    }
}
