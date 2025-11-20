using UnityEngine;
using SimpleMOBA.Core;

namespace SimpleMOBA.Gameplay.Towers
{
    /// <summary>
    /// Wraps tower state and communicates with AI and health.
    /// </summary>
    [RequireComponent(typeof(TowerAI))]
    [RequireComponent(typeof(HealthComponent))]
    public class TowerController : MonoBehaviour, ITeamProvider
    {
        [SerializeField] private Team team;
        [SerializeField] private bool isVulnerable = true;

        private TowerAI ai;
        private HealthComponent health;

        public Team Team => team;
        public bool IsVulnerable => isVulnerable;

        public event System.Action OnDestroyed;

        private void Awake()
        {
            ai = GetComponent<TowerAI>();
            health = GetComponent<HealthComponent>();
            health.OnDeath += HandleDestroyed;
            health.SetInvulnerable(!isVulnerable);
        }

        private void OnDestroy()
        {
            health.OnDeath -= HandleDestroyed;
        }

        public void Initialize(Team owningTeam)
        {
            team = owningTeam;
            ai.AssignTeam(owningTeam);
            health.ResetHealth();
        }

        public void ApplyData(ScriptableObjects.Towers.TowerData data, bool startVulnerable)
        {
            ApplyData(Gameplay.Towers.TowerStats.FromData(data), startVulnerable);
        }

        public void ApplyData(Gameplay.Towers.TowerStats stats, bool startVulnerable)
        {
            if (stats == null)
            {
                return;
            }

            ai.ApplyData(stats.MaxHealth, stats.AttackRange, stats.AttackDamage, stats.AttackCooldown);
            SetVulnerable(startVulnerable);
        }

        public void SetVulnerable(bool value)
        {
            isVulnerable = value;
            health?.SetInvulnerable(!isVulnerable);
        }

        private void HandleDestroyed()
        {
            OnDestroyed?.Invoke();
            // TODO: Add VFX/SFX and disable colliders.
            Destroy(gameObject, 0.5f);
        }
    }
}
