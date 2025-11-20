using System.Collections.Generic;
using UnityEngine;

namespace SimpleMOBA.Core
{
    /// <summary>
    /// Utility methods for selecting targets in range.
    /// </summary>
    public static class TargetingSystem
    {
        public static T FindClosestTarget<T>(Vector3 origin, float range, Team seekingTeam) where T : MonoBehaviour
        {
            var candidates = Object.FindObjectsOfType<T>();
            var closest = default(T);
            var closestSqr = float.MaxValue;

            foreach (var candidate in candidates)
            {
                if (!IsEnemy(candidate.gameObject, seekingTeam))
                {
                    continue;
                }

                var sqrDistance = (candidate.transform.position - origin).sqrMagnitude;
                if (sqrDistance <= range * range && sqrDistance < closestSqr)
                {
                    closestSqr = sqrDistance;
                    closest = candidate;
                }
            }

            return closest;
        }

        /// <summary>
        /// Placeholder team-check helper. Replace with a proper team service when networking arrives.
        /// </summary>
        private static bool IsEnemy(GameObject target, Team seekingTeam)
        {
            var teamHolder = target.GetComponent<ITeamProvider>();
            return teamHolder != null && teamHolder.Team != seekingTeam;
        }
    }

    /// <summary>
    /// Provides access to team data for shared systems.
    /// </summary>
    public interface ITeamProvider
    {
        Team Team { get; }
    }
}
