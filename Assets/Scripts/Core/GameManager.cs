using System.Collections;
using UnityEngine;
using SimpleMOBA.Core;
using SimpleMOBA.Gameplay.Heroes;
using SimpleMOBA.Gameplay.Managers;
using SimpleMOBA.Gameplay.Minions;
using SimpleMOBA.Gameplay.Towers;

namespace SimpleMOBA.Core
{
    /// <summary>
    /// Central game controller that manages lifecycle, spawns, and win conditions.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            PreGame,
            InGame,
            GameOver
        }

        [Header("Heroes")]
        [SerializeField] private ScriptableObjects.Heroes.HeroData playerHeroData;
        [SerializeField] private ScriptableObjects.Heroes.HeroData aiHeroData;
        [SerializeField] private Transform playerSpawn;
        [SerializeField] private Transform aiSpawn;

        [Header("Towers")]
        [SerializeField] private TowerController playerTier1Tower;
        [SerializeField] private TowerController playerTier2Tower;
        [SerializeField] private TowerController aiTier1Tower;
        [SerializeField] private TowerController aiTier2Tower;
        [SerializeField] private ScriptableObjects.Towers.TowerData playerTowerData;
        [SerializeField] private ScriptableObjects.Towers.TowerData aiTowerData;
        [SerializeField] private bool baseTowersStartInvulnerable = true;

        [Header("Minions")]
        [SerializeField] private MinionSpawner minionSpawner;

        [Header("Flow")]
        [SerializeField] private float preGameDelaySeconds = 2f;

        private GameState currentState = GameState.PreGame;
        private HeroController playerHeroInstance;
        private HeroController aiHeroInstance;

        public GameState CurrentState => currentState;

        private void Start()
        {
            StartCoroutine(BeginMatchRoutine());
        }

        private IEnumerator BeginMatchRoutine()
        {
            currentState = GameState.PreGame;
            yield return new WaitForSeconds(preGameDelaySeconds);
            SpawnHeroes();
            StartMatch();
        }

        private void SpawnHeroes()
        {
            playerHeroInstance = SpawnHero(playerHeroData, playerSpawn, Team.TeamA, true);
            aiHeroInstance = SpawnHero(aiHeroData, aiSpawn, Team.TeamB, false);
        }

        private HeroController SpawnHero(ScriptableObjects.Heroes.HeroData data, Transform spawnPoint, Team team, bool isPlayerControlled)
        {
            if (data == null || data.HeroPrefab == null || spawnPoint == null)
            {
                return null;
            }

            var hero = Instantiate(data.HeroPrefab, spawnPoint.position, Quaternion.identity);
            hero.Initialize(team, isPlayerControlled);
            hero.ApplyData(data);
            return hero;
        }

        private void StartMatch()
        {
            currentState = GameState.InGame;
            if (minionSpawner != null)
            {
                minionSpawner.BeginSpawning();
            }

            ConfigureTowers();
        }

        private void ConfigureTowers()
        {
            playerTier1Tower?.Initialize(Team.TeamA);
            playerTier2Tower?.Initialize(Team.TeamA);
            aiTier1Tower?.Initialize(Team.TeamB);
            aiTier2Tower?.Initialize(Team.TeamB);

            RegisterTowerEvents(playerTier1Tower, Team.TeamA, isBaseTower: false);
            RegisterTowerEvents(playerTier2Tower, Team.TeamA, isBaseTower: true);
            RegisterTowerEvents(aiTier1Tower, Team.TeamB, isBaseTower: false);
            RegisterTowerEvents(aiTier2Tower, Team.TeamB, isBaseTower: true);

            playerTier1Tower?.ApplyData(playerTowerData, startVulnerable: true);
            aiTier1Tower?.ApplyData(aiTowerData, startVulnerable: true);

            var baseVulnerable = !baseTowersStartInvulnerable;
            playerTier2Tower?.ApplyData(playerTowerData, startVulnerable: baseVulnerable);
            aiTier2Tower?.ApplyData(aiTowerData, startVulnerable: baseVulnerable);
        }

        private void RegisterTowerEvents(TowerController tower, Team team, bool isBaseTower)
        {
            if (tower == null)
            {
                return;
            }

            tower.OnDestroyed += () => HandleTowerDestroyed(team, isBaseTower);
        }

        private void HandleTowerDestroyed(Team owningTeam, bool isBaseTower)
        {
            // TODO: Hook up UI feedback and audio cues.
            if (owningTeam == Team.TeamA)
            {
                if (!isBaseTower)
                {
                    playerTier2Tower?.SetVulnerable(true);
                }
                else
                {
                    EndMatch(winner: Team.TeamB);
                }
            }
            else
            {
                if (!isBaseTower)
                {
                    aiTier2Tower?.SetVulnerable(true);
                }
                else
                {
                    EndMatch(winner: Team.TeamA);
                }
            }
        }

        private void EndMatch(Team winner)
        {
            if (currentState == GameState.GameOver)
            {
                return;
            }

            currentState = GameState.GameOver;
            minionSpawner?.StopSpawning();

            // TODO: Freeze inputs/AI, show win/lose UI, and provide restart button.
        }
    }
}
