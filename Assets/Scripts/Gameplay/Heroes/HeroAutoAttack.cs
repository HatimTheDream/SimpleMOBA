using UnityEngine;
using SimpleMOBA.Core;

namespace SimpleMOBA.Gameplay.Heroes
{
    /// <summary>
    /// Handles continuous auto-attack logic for a hero.
    /// </summary>
    public class HeroAutoAttack : MonoBehaviour
    {
        [SerializeField] private float attackRange = 3f;
        [SerializeField] private float attackDamage = 10f;
        [SerializeField] private float attackCooldown = 1f;

        private HeroController owner;
        private float attackSpeedMultiplier = 1f;
        private float lastAttackTime;

        public void ConfigureStats(float damage, float range, float cooldown)
        {
            attackDamage = damage;
            attackRange = range;
            attackCooldown = cooldown;
        }

        public void Initialize(HeroController hero)
        {
            owner = hero;
        }

        private void Update()
        {
            if (owner == null)
            {
                return;
            }

            var target = TargetingSystem.FindClosestTarget<HealthComponent>(transform.position, attackRange, owner.Team);
            if (target != null)
            {
                TryAttack(target);
            }
        }

        public void SetAttackSpeedMultiplier(float multiplier)
        {
            attackSpeedMultiplier = multiplier;
        }

        private void TryAttack(HealthComponent target)
        {
            var effectiveCooldown = attackCooldown / Mathf.Max(0.01f, attackSpeedMultiplier);
            if (Time.time - lastAttackTime < effectiveCooldown)
            {
                return;
            }

            lastAttackTime = Time.time;
            target.TakeDamage(attackDamage);
            // TODO: Hook up animation or projectile emission.
        }
    }
}
