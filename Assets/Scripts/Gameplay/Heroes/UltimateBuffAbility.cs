using System.Collections;
using UnityEngine;

namespace SimpleMOBA.Gameplay.Heroes
{
    /// <summary>
    /// Example ultimate: temporarily boosts movement and attack speed.
    /// </summary>
    public class UltimateBuffAbility : Ability
    {
        [SerializeField] private float duration = 5f;
        [SerializeField] private float moveSpeedMultiplier = 1.5f;
        [SerializeField] private float attackSpeedMultiplier = 1.5f;

        private Coroutine activeRoutine;

        protected override bool OnCast()
        {
            if (activeRoutine != null)
            {
                return false;
            }

            activeRoutine = StartCoroutine(BuffRoutine());
            return true;
        }

        private IEnumerator BuffRoutine()
        {
            var hero = GetComponent<HeroController>();
            if (hero != null)
            {
                hero.ModifyMovementMultiplier(moveSpeedMultiplier);
                hero.ModifyAttackSpeedMultiplier(attackSpeedMultiplier);
            }

            yield return new WaitForSeconds(duration);

            if (hero != null)
            {
                hero.ModifyMovementMultiplier(1f);
                hero.ModifyAttackSpeedMultiplier(1f);
            }

            activeRoutine = null;
        }
    }
}
