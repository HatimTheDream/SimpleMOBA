using System.Collections;
using UnityEngine;
using SimpleMOBA.Core;
using SimpleMOBA.Gameplay.Minions;

namespace SimpleMOBA.Gameplay.Managers
{
    /// <summary>
    /// Spawns minion waves for each team at fixed intervals.
    /// </summary>
    public class MinionSpawner : MonoBehaviour
    {
        [SerializeField] private ScriptableObjects.Minions.MinionData teamAMinionData;
        [SerializeField] private ScriptableObjects.Minions.MinionData teamBMinionData;
        [SerializeField] private Transform teamASpawnPoint;
        [SerializeField] private Transform teamBSpawnPoint;
        [SerializeField] private float spawnIntervalSeconds = 10f;

        private Coroutine spawnRoutine;

        public void BeginSpawning()
        {
            if (spawnRoutine == null)
            {
                spawnRoutine = StartCoroutine(SpawnRoutine());
            }
        }

        public void StopSpawning()
        {
            if (spawnRoutine != null)
            {
                StopCoroutine(spawnRoutine);
                spawnRoutine = null;
            }
        }

        private IEnumerator SpawnRoutine()
        {
            var wait = new WaitForSeconds(spawnIntervalSeconds);
            while (true)
            {
                SpawnWave();
                yield return wait;
            }
        }

        private void SpawnWave()
        {
            // TODO: Extend with different compositions per wave.
            if (teamAMinionData != null && teamAMinionData.MinionPrefab != null && teamASpawnPoint != null)
            {
                var minion = Instantiate(teamAMinionData.MinionPrefab, teamASpawnPoint.position, Quaternion.identity);
                minion.Initialize(Team.TeamA);
                minion.GetComponent<MinionAI>()?.ApplyData(
                    teamAMinionData.MaxHealth,
                    teamAMinionData.MoveSpeed,
                    teamAMinionData.AttackDamage,
                    teamAMinionData.AttackRange,
                    teamAMinionData.AttackCooldown);
            }

            if (teamBMinionData != null && teamBMinionData.MinionPrefab != null && teamBSpawnPoint != null)
            {
                var minion = Instantiate(teamBMinionData.MinionPrefab, teamBSpawnPoint.position, Quaternion.identity);
                minion.Initialize(Team.TeamB);
                minion.GetComponent<MinionAI>()?.ApplyData(
                    teamBMinionData.MaxHealth,
                    teamBMinionData.MoveSpeed,
                    teamBMinionData.AttackDamage,
                    teamBMinionData.AttackRange,
                    teamBMinionData.AttackCooldown);
            }
        }
    }
}
