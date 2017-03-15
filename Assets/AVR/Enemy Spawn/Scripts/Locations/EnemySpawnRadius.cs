using UnityEngine;

namespace AVRToolkit.EnemySpawner
{
    public class EnemySpawnRadius : EnemySpawnLocation
    {
        [SerializeField]
        private bool _showSpawnRadius = false;
        
        [SerializeField]
        private bool _randomiseRotation = false;

        [SerializeField]
        private float _randomSpawnRadius = 2.0f;

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            if (_showSpawnRadius) {
                Gizmos.DrawWireSphere(transform.position, _randomSpawnRadius);
            }
        }

        public override Point SpawnPoint()
        {
            Quaternion rotation = transform.rotation;
            if (_randomiseRotation) {
                rotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
            }

            Vector3 position = transform.position;
            position.z = RandomNumber(position.z, _randomSpawnRadius);
            position.x = RandomNumber(position.x, _randomSpawnRadius);
            
            Point point = new Point();
            point.position = position;
            point.rotation = rotation;
            return point;
        }

        private float RandomNumber(float number, float range)
        {
            float min = number - range;
            float max = number + range;
            return Random.Range(min, max);
        }
    }
}