using System;
using UnityEngine;

namespace Field
{
    public class GridHolder : MonoBehaviour
    {
        [SerializeField] private int gridWidth;

        [SerializeField] private int gridHeight;

        [SerializeField] private float nodeSize;

        [SerializeField] private Vector2Int targetCoordinate;

        [SerializeField] private Vector2Int startCoordinate;

        private Grid _grid;

        private Camera _camera;

        private Vector3 _offset;

        public Vector2Int StartCoordinate => startCoordinate;

        public Grid Grid => _grid;

        private void Start()
        {
            _grid = new Grid(gridWidth, gridHeight, _offset, nodeSize, targetCoordinate);
            _camera = Camera.main;

            var width = gridWidth * nodeSize;
            var height = gridHeight * nodeSize;

            // Default plane size is 10 by 10
            transform.localScale = new Vector3(width * 0.1f, 1f, height * 0.1f);

            _offset = transform.position - new Vector3(width, 0f, height) * 0.5f;
        }

        private void OnValidate()
        {
            var width = gridWidth * nodeSize;
            var height = gridHeight * nodeSize;

            // Default plane size is 10 by 10
            transform.localScale = new Vector3(width * 0.1f, 1f, height * 0.1f);

            _offset = transform.position - new Vector3(width, 0f, height) * 0.5f;
        }

        private void Update()
        {
            if (_grid == null || _camera == null)
                return;

            var mousePosition = Input.mousePosition;

            var ray = _camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.transform != transform)
                    return;

                var hitPosition = hit.point;
                var difference = hitPosition - _offset;

                var x = (int) (difference.x / nodeSize);
                var z = (int) (difference.z / nodeSize);

                if (Input.GetMouseButtonDown(0))
                {
                    var node = _grid.GetNode(x, z);
                    node.IsOccupied = !node.IsOccupied;
                    _grid.UpdatePathFinding();
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (_grid == null)
                return;

            foreach (var node in _grid.EnumerateAllNodes())
            {
                if (node.NextNode == null)
                    continue;
                
                if (node.IsOccupied)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawSphere(node.Position, 0.5f);
                    continue;
                }
                
                Gizmos.color = Color.red;

                Vector3 start = node.Position;
                Vector3 end = node.NextNode.Position;

                Vector3 direction = end - start;

                start -= direction * 0.25f;
                end -= direction * 0.75f;

                Gizmos.DrawLine(start, end);
                Gizmos.DrawSphere(end, 0.1f);
            }
        }
    }
}