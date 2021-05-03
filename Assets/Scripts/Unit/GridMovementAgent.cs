using Field;
using UnityEngine;

namespace Unit
{
    public class GridMovementAgent : MonoBehaviour
    {
        [SerializeField] private float speed;

        private const float Tolerance = 0.1f;

        private Node targetNode;

        private void Update()
        {
            if (targetNode == null)
                return;

            Vector3 target = targetNode.Position;
            var distance = (target - transform.position).magnitude;
            if (distance < Tolerance)
            {
                targetNode = targetNode.NextNode;
                return;
            }
            var direction = (target - transform.position).normalized;
            var delta = direction * (speed * Time.deltaTime);
            transform.Translate(delta);
        }

        public void SetStartNode(Node node)
        {
            targetNode = node;
        }

    }
}