using UnityEngine;
using SimpleMOBA.Gameplay.Heroes;

namespace SimpleMOBA.ScriptableObjects.Heroes
{
    /// <summary>
    /// Defines base stats and ability prefabs for a hero.
    /// </summary>
    [CreateAssetMenu(menuName = "SimpleMOBA/HeroData")]
    public class HeroData : ScriptableObject
    {
        public float MaxHealth = 200f;
        public float MoveSpeed = 4f;
        public float AttackDamage = 12f;
        public float AttackRange = 3f;
        public float AttackCooldown = 1f;

        public HeroController HeroPrefab;
        public Ability BasicAbilityPrefab;
        public Ability UltimateAbilityPrefab;
    }
}
