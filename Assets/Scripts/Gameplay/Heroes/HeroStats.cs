using System;
using UnityEngine;

namespace SimpleMOBA.Gameplay.Heroes
{
    [Serializable]
    public class HeroStats
    {
        public float MaxHealth;
        public float MoveSpeed;
        public float AttackDamage;
        public float AttackRange;
        public float AttackCooldown;

        public Ability BasicAbilityPrefab;
        public Ability UltimateAbilityPrefab;

        public HeroStats() { }

        public HeroStats(float maxHealth, float moveSpeed, float attackDamage, float attackRange, float attackCooldown,
                         Ability basicAbilityPrefab, Ability ultimateAbilityPrefab)
        {
            MaxHealth = maxHealth;
            MoveSpeed = moveSpeed;
            AttackDamage = attackDamage;
            AttackRange = attackRange;
            AttackCooldown = attackCooldown;
            BasicAbilityPrefab = basicAbilityPrefab;
            UltimateAbilityPrefab = ultimateAbilityPrefab;
        }

        public static HeroStats FromData(ScriptableObjects.Heroes.HeroData data)
        {
            if (data == null) return new HeroStats();
            return new HeroStats(data.MaxHealth, data.MoveSpeed, data.AttackDamage, data.AttackRange, data.AttackCooldown,
                                 data.BasicAbilityPrefab, data.UltimateAbilityPrefab);
        }
    }
}
