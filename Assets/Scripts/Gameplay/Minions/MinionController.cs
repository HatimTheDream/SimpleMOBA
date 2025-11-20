using UnityEngine;
using SimpleMOBA.Core;

namespace SimpleMOBA.Gameplay.Minions
{
    /// <summary>
    /// Holds minion state and proxies into AI/animations.
    /// </summary>
    [RequireComponent(typeof(MinionAI))]
    [RequireComponent(typeof(HealthComponent))]
    public class MinionController : MonoBehaviour, ITeamProvider
    {
        [SerializeField] private Team team;

        private MinionAI ai;
        private HealthComponent health;

        public Team Team => team;

        private void Awake()
        {
            ai = GetComponent<MinionAI>();
            health = GetComponent<HealthComponent>();
        }

        public void Initialize(Team owningTeam)
        {
            team = owningTeam;
            ai.AssignTeam(owningTeam);
            health.ResetHealth();
        }
    }
}
