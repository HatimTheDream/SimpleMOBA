using UnityEngine;
using UnityEngine.UI;
using SimpleMOBA.Core;
using SimpleMOBA.Gameplay.Heroes;

namespace SimpleMOBA.UI
{
    /// <summary>
    /// Coordinates HUD elements such as health bars, cooldowns, and match states.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("Hero UI")]
        [SerializeField] private Slider heroHealthBar;
        [SerializeField] private Image basicCooldownFill;
        [SerializeField] private Image ultimateCooldownFill;

        [Header("Tower UI")]
        [SerializeField] private Slider playerTier1Health;
        [SerializeField] private Slider playerTier2Health;
        [SerializeField] private Slider enemyTier1Health;
        [SerializeField] private Slider enemyTier2Health;

        [Header("Meta")]
        [SerializeField] private Text matchTimerText;

        private HeroController trackedHero;
        private HealthComponent heroHealth;
        private Ability basicAbility;
        private Ability ultimateAbility;
        private float matchStartTime;

        private void Start()
        {
            matchStartTime = Time.time;
        }

        private void Update()
        {
            UpdateTimer();
            UpdateAbilityCooldowns();
        }

        public void TrackHero(HeroController hero)
        {
            trackedHero = hero;
            heroHealth = hero != null ? hero.GetComponent<HealthComponent>() : null;
            basicAbility = hero?.BasicAbility;
            ultimateAbility = hero?.UltimateAbility;

            if (heroHealthBar != null && heroHealth != null)
            {
                heroHealthBar.minValue = 0f;
                heroHealthBar.maxValue = heroHealth.MaxHealth;
                heroHealthBar.value = heroHealth.CurrentHealth;
                heroHealth.OnHealthChanged += (current, max) => heroHealthBar.value = current;
            }
        }

        public void BindTowerHealth(Slider slider, HealthComponent health)
        {
            if (slider == null || health == null)
            {
                return;
            }

            slider.minValue = 0f;
            slider.maxValue = health.MaxHealth;
            slider.value = health.CurrentHealth;
            health.OnHealthChanged += (current, max) => slider.value = current;
        }

        private void UpdateTimer()
        {
            if (matchTimerText == null)
            {
                return;
            }

            var elapsed = Time.time - matchStartTime;
            var minutes = Mathf.FloorToInt(elapsed / 60f);
            var seconds = Mathf.FloorToInt(elapsed % 60f);
            matchTimerText.text = $"{minutes:00}:{seconds:00}";
        }

        private void UpdateAbilityCooldowns()
        {
            if (basicAbility != null && basicCooldownFill != null)
            {
                basicCooldownFill.fillAmount = Mathf.Clamp01(basicAbility.TimeSinceCast / basicAbility.CooldownSeconds);
            }

            if (ultimateAbility != null && ultimateCooldownFill != null)
            {
                ultimateCooldownFill.fillAmount = Mathf.Clamp01(ultimateAbility.TimeSinceCast / ultimateAbility.CooldownSeconds);
            }
        }
    }
}
