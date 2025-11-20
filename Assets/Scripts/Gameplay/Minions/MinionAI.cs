using UnityEngine;
using SimpleMOBA.Core;

namespace SimpleMOBA.Gameplay.Minions
{
    /// <summary>
    /// Simple lane-following and auto-attacking AI for minions.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class MinionAI : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float attackDamage = 5f;
        [SerializeField] private float attackCooldown = 1.5f;
        [SerializeField] private Transform[] pathWaypoints;

        private Team team;
        private int currentWaypointIndex;
        private float lastAttackTime;
        private Rigidbody2D body;

        public void ApplyData(float maxHealth, float newMoveSpeed, float newAttackDamage, float newAttackRange, float newAttackCooldown)
        {
            moveSpeed = newMoveSpeed;
            attackDamage = newAttackDamage;
            attackRange = newAttackRange;
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

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (!TryAttack())
            {
                AdvanceAlongPath();
            }
        }

        private void AdvanceAlongPath()
        {
            if (pathWaypoints == null || pathWaypoints.Length == 0)
            {
                return;
            }

            var targetWaypoint = pathWaypoints[currentWaypointIndex];
            var direction = (targetWaypoint.position - transform.position).normalized;
            body.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);

            if (Vector3.Distance(transform.position, targetWaypoint.position) <= 0.1f)
            {
                currentWaypointIndex = Mathf.Min(currentWaypointIndex + 1, pathWaypoints.Length - 1);
            }
        }

        private bool TryAttack()
        {
            var target = TargetingSystem.FindClosestTarget<HealthComponent>(transform.position, attackRange, team);
            if (target == null)
            {
                return false;
            }

            if (Time.time - lastAttackTime < attackCooldown)
            {
                return true;
            }

            lastAttackTime = Time.time;
            target.TakeDamage(attackDamage);
            // TODO: Add attack animation/effects.
            return true;
        }
    }
}
