using UnityEngine;
using SimpleMOBA.Core;

namespace SimpleMOBA.Gameplay.Heroes
{
    /// <summary>
    /// Example basic ability: deals area damage around the hero.
    /// </summary>
    public class BasicBurstAbility : Ability
    {
        [SerializeField] private float radius = 3f;
        [SerializeField] private float damage = 20f;
        [SerializeField] private LayerMask hitMask;

        protected override bool OnCast()
        {
            var hits = Physics2D.OverlapCircleAll(transform.position, radius, hitMask);
            foreach (var hit in hits)
            {
                var health = hit.GetComponent<HealthComponent>();
                var teamProvider = hit.GetComponent<ITeamProvider>();
                var ownerTeam = GetComponent<HeroController>()?.Team ?? Team.TeamA;

                if (health != null && teamProvider != null && teamProvider.Team != ownerTeam)
                {
                    health.TakeDamage(damage);
                }
            }

            // TODO: Add visual effect for impact radius.
            return true;
        }
    }
}
