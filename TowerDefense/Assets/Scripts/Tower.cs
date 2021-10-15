using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    public abstract class Tower : MonoBehaviour
    {
        [SerializeField] public float firingRate;
        [SerializeField] public float firingRange;

        public IReadOnlyList<Enemy> enemies;
        public float fireTimer;

        public virtual void Initialize(IReadOnlyList<Enemy> enemies)
        {
            this.enemies = enemies;
            fireTimer = firingRate;
        }

        public Enemy FindClosestEnemy()
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
