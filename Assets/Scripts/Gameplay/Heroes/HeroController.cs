using UnityEngine;
using SimpleMOBA.Core;
using SimpleMOBA.UI;

namespace SimpleMOBA.Gameplay.Heroes
{
    /// <summary>
    /// Handles hero movement, ability input, and integration with auto-attack.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(HeroAutoAttack))]
    [RequireComponent(typeof(HealthComponent))]
    public class HeroController : MonoBehaviour, ITeamProvider
    {
        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private Ability basicAbility;
        [SerializeField] private Ability ultimateAbility;

        private Team team;
        private bool isPlayerControlled;
        private Rigidbody2D body;
        private HeroAutoAttack autoAttack;
        private MobileJoystick joystick;
        private float movementMultiplier = 1f;
        private HealthComponent health;

        public Team Team => team;
        public Ability BasicAbility => basicAbility;
        public Ability UltimateAbility => ultimateAbility;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            autoAttack = GetComponent<HeroAutoAttack>();
            health = GetComponent<HealthComponent>();
        }

        public void Initialize(Team owningTeam, bool isPlayerControlled)
        {
            team = owningTeam;
            this.isPlayerControlled = isPlayerControlled;
            autoAttack.Initialize(this);
        }

        public void ApplyData(ScriptableObjects.Heroes.HeroData data)
        {
            if (data == null)
            {
                return;
            }

            moveSpeed = data.MoveSpeed;
            autoAttack.ConfigureStats(data.AttackDamage, data.AttackRange, data.AttackCooldown);
            health?.ConfigureMaxHealth(data.MaxHealth);

            basicAbility = InstantiateAbility(data.BasicAbilityPrefab);
            ultimateAbility = InstantiateAbility(data.UltimateAbilityPrefab);
        }

        private void Update()
        {
            if (isPlayerControlled)
            {
                HandlePlayerInput();
            }
            else
            {
                HandleAiInput();
            }
        }

        private void FixedUpdate()
        {
            MoveHero();
        }

        public void BindJoystick(MobileJoystick joystickInput)
        {
            joystick = joystickInput;
        }

        public void ModifyMovementMultiplier(float multiplier)
        {
            movementMultiplier = multiplier;
        }

        public void ModifyAttackSpeedMultiplier(float multiplier)
        {
            autoAttack.SetAttackSpeedMultiplier(multiplier);
        }

        public void TriggerBasicAbility()
        {
            basicAbility?.TryCast();
        }

        public void TriggerUltimateAbility()
        {
            ultimateAbility?.TryCast();
        }

        private Ability InstantiateAbility(Ability abilityPrefab)
        {
            if (abilityPrefab == null)
            {
                return null;
            }

            return Instantiate(abilityPrefab, transform);
        }

        private void HandlePlayerInput()
        {
            // Movement handled via joystick; abilities bound to UI buttons.
        }

        private void HandleAiInput()
        {
            // TODO: Add simple AI for demo purposes (follow lane and cast abilities opportunistically).
        }

        private void MoveHero()
        {
            var inputDirection = joystick != null ? joystick.Direction : Vector2.zero;
            var movement = inputDirection * moveSpeed * movementMultiplier * Time.fixedDeltaTime;
            body.MovePosition(body.position + movement);
        }
    }
}
