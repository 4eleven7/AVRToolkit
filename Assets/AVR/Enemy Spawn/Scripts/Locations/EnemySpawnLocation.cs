using UnityEngine;

namespace AVRToolkit.EnemySpawner
{
    public class EnemySpawnLocation : MonoBehaviour
    {
        [SerializeField]
        private Color _gizmoColor = Color.red;
        
        public virtual void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawSphere(transform.position, 0.2f);
        }

        public virtual Point SpawnPoint()
        {
            Point point = new Point();
            point.position = transform.position;
            point.rotation = transform.rotation;
            return point;
        }
    }

    public struct Point
    {
        public Vector3 position;
        public Quaternion rotation;
    }
}
