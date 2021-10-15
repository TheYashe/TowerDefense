using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    public class Tower : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] protected GameObject bulletPrefab;
        [SerializeField] protected Transform bulletSpawnPoint;
        [SerializeField] protected float firingRate;
        [SerializeField] protected float firingRange;

        public IReadOnlyList<Enemy> enemies;
        protected Enemy targetEnemy;
        protected float fireTimer;

        public void Initialize(IReadOnlyList<Enemy> enemies)
        {
            this.enemies = enemies;
            fireTimer = firingRate;
        }

        protected Enemy FindClosestEnemy()
        {
            Enemy closestEnemy = null;
            var closestDistance = float.MaxValue;

            foreach (var enemy in enemies)
            {
                var distance = (enemy.transform.position - transform.position).magnitude;
                if (distance <= firingRange && distance <= closestDistance)
                {
                    closestEnemy = enemy;
                    closestDistance = distance;
                }
            }

            return closestEnemy;
        }
    }
}
