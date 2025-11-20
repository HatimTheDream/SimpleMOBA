using System;
using UnityEngine;

namespace SimpleMOBA.Core
{
    /// <summary>
    /// Generic health and damage handling component for heroes, towers, and minions.
    /// </summary>
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField]
        private float maxHealth = 100f;

        private float currentHealth;
        private bool isInvulnerable;

        public event Action<float, float> OnHealthChanged;
        public event Action OnDeath;

        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;
        public bool IsInvulnerable => isInvulnerable;

        private void Awake()
        {
            ResetHealth();
        }

        /// <summary>
        /// Restores health to full and notifies listeners.
        /// </summary>
        public void ResetHealth()
        {
            currentHealth = maxHealth;
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        /// <summary>
        /// Sets a new max health value and optionally resets the current health.
        /// </summary>
        public void ConfigureMaxHealth(float newMaxHealth, bool resetCurrent = true)
        {
            maxHealth = Mathf.Max(1f, newMaxHealth);
            if (resetCurrent)
            {
                ResetHealth();
            }
            else
            {
                currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
                OnHealthChanged?.Invoke(currentHealth, maxHealth);
            }
        }

        /// <summary>
        /// Applies damage and triggers death when health reaches zero.
        /// </summary>
        public void TakeDamage(float amount)
        {
            if (amount <= 0f || currentHealth <= 0f || isInvulnerable)
            {
                return;
            }

            currentHealth = Mathf.Max(0f, currentHealth - amount);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);

            if (currentHealth <= 0f)
            {
                OnDeath?.Invoke();
            }
        }

        /// <summary>
        /// Heals the entity up to its maximum health.
        /// </summary>
        public void Heal(float amount)
        {
            if (amount <= 0f || currentHealth <= 0f)
            {
                return;
            }

            currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        /// <summary>
        /// Toggles invulnerability without altering current health.
        /// </summary>
        public void SetInvulnerable(bool value)
        {
            isInvulnerable = value;
        }
    }
}
