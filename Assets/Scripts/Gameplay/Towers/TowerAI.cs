using UnityEngine;
using SimpleMOBA.Core;

namespace SimpleMOBA.Gameplay.Towers
{
    /// <summary>
    /// Automated target selection and attacking behavior for towers.
    /// </summary>
    public class TowerAI : MonoBehaviour
    {
        [SerializeField] private float attackRange = 6f;
        [SerializeField] private float attackDamage = 15f;
        [SerializeField] private float attackCooldown = 1f;

        private Team team;
        private float lastAttackTime;
        private HealthComponent currentTarget;

        public void ApplyData(float maxHealth, float newAttackRange, float newAttackDamage, float newAttackCooldown)
        {
            attackRange = newAttackRange;
            attackDamage = newAttackDamage;
            attackCooldown = newAttackCooldown;

            var health = GetComponent<HealthComponent>();
            if (health != null)
            {
                health.ConfigureMaxHealth(maxHealth);
            }
        }

        public void AssignTeam(Team owningTeam)
        {
            team = owningTeam;
        }

        private void Update()
        {
            if (TryAcquireTarget(out var target))
            {
                TryAttack(target);
            }
        }

        private bool TryAcquireTarget(out HealthComponent target)
        {
            // TODO: Replace with aggro priority that reacts to hero aggression.
            target = TargetingSystem.FindClosestTarget<HealthComponent>(transform.position, attackRange, team);
            currentTarget = target;
            return target != null;
        }

        private void TryAttack(HealthComponent target)
        {
            if (Time.time - lastAttackTime < attackCooldown)
            {
                return;
            }

            lastAttackTime = Time.time;
            target.TakeDamage(attackDamage);
            // TODO: Add projectile/beam visuals and sound.
        }
    }
}
