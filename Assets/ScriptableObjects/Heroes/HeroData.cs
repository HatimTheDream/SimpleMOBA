using UnityEngine;
using SimpleMOBA.Gameplay.Heroes;

namespace SimpleMOBA.ScriptableObjects.Heroes
{
    /// <summary>
    /// Defines base stats and ability prefabs for a hero.
    /// </summary>
    [CreateAssetMenu(menuName = "SimpleMOBA/HeroData")]
    public class HeroData : ScriptableObject
    {
        [Header("Stats")]
        [SerializeField, Min(0f)] private float maxHealth = 200f;
        [SerializeField, Min(0f)] private float moveSpeed = 4f;
        [SerializeField, Min(0f)] private float attackDamage = 12f;
        [SerializeField, Min(0f)] private float attackRange = 3f;
        [SerializeField, Min(0f)] private float attackCooldown = 1f;

        [Header("Prefabs")]
        [SerializeField] private HeroController heroPrefab;
        [SerializeField] private Ability basicAbilityPrefab;
        [SerializeField] private Ability ultimateAbilityPrefab;

        public float MaxHealth => maxHealth;
        public float MoveSpeed => moveSpeed;
        public float AttackDamage => attackDamage;
        public float AttackRange => attackRange;
        public float AttackCooldown => attackCooldown;

        public HeroController HeroPrefab => heroPrefab;
        public Ability BasicAbilityPrefab => basicAbilityPrefab;
        public Ability UltimateAbilityPrefab => ultimateAbilityPrefab;

        private void OnValidate()
        {
            maxHealth = Mathf.Max(0f, maxHealth);
            moveSpeed = Mathf.Max(0f, moveSpeed);
            attackDamage = Mathf.Max(0f, attackDamage);
            attackRange = Mathf.Max(0f, attackRange);
            attackCooldown = Mathf.Max(0f, attackCooldown);
        }
    }
}
