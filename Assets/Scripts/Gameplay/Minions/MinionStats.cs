using System;
using UnityEngine;

namespace SimpleMOBA.Gameplay.Minions
{
    /// <summary>
    /// Runtime copy of minion configuration taken from a `MinionData` asset.
    /// Use this for per-instance mutable state so the ScriptableObject isn't modified at runtime.
    /// </summary>
    [Serializable]
    public class MinionStats
    {
        public float MaxHealth;
        public float MoveSpeed;
        public float AttackDamage;
        public float AttackRange;
        public float AttackCooldown;

        public MinionStats() { }

        public MinionStats(float maxHealth, float moveSpeed, float attackDamage, float attackRange, float attackCooldown)
        {
            MaxHealth = maxHealth;
            MoveSpeed = moveSpeed;
            AttackDamage = attackDamage;
            AttackRange = attackRange;
            AttackCooldown = attackCooldown;
        }

        public static MinionStats FromData(ScriptableObjects.Minions.MinionData data)
        {
            if (data == null) return new MinionStats();
            return new MinionStats(data.MaxHealth, data.MoveSpeed, data.AttackDamage, data.AttackRange, data.AttackCooldown);
        }
    }
}
