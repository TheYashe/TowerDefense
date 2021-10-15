namespace AFSInterview
{
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class GameplayManager : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private GameObject simpleTowerPrefab;
        [SerializeField] private GameObject burstTowerPrefab;

        [Header("Settings")]
        [SerializeField] private Vector2 boundsMin;
        [SerializeField] private Vector2 boundsMax;
        [SerializeField] private float enemySpawnRate;

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI enemiesCountText;
        [SerializeField] private TextMeshProUGUI scoreText;

        private List<Enemy> enemies;
        private float enemySpawnTimer;
        private int score;

        private void Awake()
        {
            enemies = new List<Enemy>();
            RefreshUI();
        }

        private void Update()
        {
            enemySpawnTimer -= Time.deltaTime;

            if (enemySpawnTimer <= 0f)
            {
                SpawnEnemy();
                enemySpawnTimer = enemySpawnRate;
            }

            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, LayerMask.GetMask("Ground")))
                {
                    var spawnPosition = hit.point;
                    spawnPosition.y = simpleTowerPrefab.transform.position.y;

                    SpawnTower(simpleTowerPrefab, spawnPosition);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, LayerMask.GetMask("Ground")))
                {
                    var spawnPosition = hit.point;
                    spawnPosition.y = simpleTowerPrefab.transform.position.y;

                    SpawnTower(burstTowerPrefab, spawnPosition);
                }
            }
        }

        private void SpawnEnemy()
        {
            var position = new Vector3(Random.Range(boundsMin.x, boundsMax.x), enemyPrefab.transform.position.y, Random.Range(boundsMin.y, boundsMax.y));

            var enemy = Instantiate(enemyPrefab, position, Quaternion.identity).GetComponent<Enemy>();
            enemy.OnEnemyDied += Enemy_OnEnemyDied;
            enemy.Initialize(boundsMin, boundsMax);

            enemies.Add(enemy);
        }

        private void Enemy_OnEnemyDied(Enemy enemy)
        {
            enemies.Remove(enemy);
            score++;
            RefreshUI();
        }

        private void SpawnTower(GameObject tower, Vector3 position)
        {
            var newTower = Instantiate(tower, position, Quaternion.identity).GetComponent<Tower>();
            newTower.Initialize(enemies);
        }

        private void RefreshUI()
        {
            scoreText.SetText($"Score: {score}");
            enemiesCountText.SetText($"Enemies: {enemies.Count}");
        }
    }
}