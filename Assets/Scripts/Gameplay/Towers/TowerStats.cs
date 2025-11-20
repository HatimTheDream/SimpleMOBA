using System;
using UnityEngine;

namespace SimpleMOBA.Gameplay.Towers
{
    [Serializable]
    public class TowerStats
    {
        public float MaxHealth;
        public float AttackRange;
        public float AttackDamage;
        public float AttackCooldown;

        public TowerStats() { }

        public TowerStats(float maxHealth, float attackRange, float attackDamage, float attackCooldown)
        {
            MaxHealth = maxHealth;
            AttackRange = attackRange;
            AttackDamage = attackDamage;
            AttackCooldown = attackCooldown;
        }

        public static TowerStats FromData(ScriptableObjects.Towers.TowerData data)
        {
            if (data == null) return new TowerStats();
            return new TowerStats(data.MaxHealth, data.AttackRange, data.AttackDamage, data.AttackCooldown);
        }
    }
}
