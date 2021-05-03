using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class FlowFieldPathFinding
    {
        private readonly Grid _grid;

        private readonly Vector2Int _target;

        public FlowFieldPathFinding(Grid grid, Vector2Int target)
        {
            _grid = grid;
            _target = target;
        }

        public void UpdateField()
        {
            foreach (var node in _grid.EnumerateAllNodes()) node.ResetWeight();

            var queue = new Queue<Vector2Int>();

            queue.Enqueue(_target);
            _grid.GetNode(_target).PathWeight = 0;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var currentNode = _grid.GetNode(current);
                var weightToTarget = currentNode.PathWeight + 1;
                foreach (var neighbour in GetNeighbours(current))
                {
                    var neighbourNode = _grid.GetNode(neighbour);
                    if (!(weightToTarget < neighbourNode.PathWeight)) continue;
                    neighbourNode.NextNode = currentNode;
                    neighbourNode.PathWeight = weightToTarget;
                    queue.Enqueue(neighbour);
                }
            }
        }

        private IEnumerable<Vector2Int> GetNeighbours(Vector2Int coordinate)
        {
            var rightCoordinate = coordinate + Vector2Int.right;
            var leftCoordinate = coordinate + Vector2Int.left;
            var upCoordinate = coordinate + Vector2Int.up;
            var downCoordinate = coordinate + Vector2Int.down;

            var hasRightNode = rightCoordinate.x < _grid.Width && !_grid.GetNode(rightCoordinate).IsOccupied;
            var hasLeftNode = leftCoordinate.x >= 0 && !_grid.GetNode(leftCoordinate).IsOccupied;
            var hasUpNode = upCoordinate.y < _grid.Height && !_grid.GetNode(upCoordinate).IsOccupied;
            var hasDownNode = downCoordinate.y >= 0 && !_grid.GetNode(downCoordinate).IsOccupied;

            if (hasRightNode)
                yield return rightCoordinate;

            if (hasLeftNode)
                yield return leftCoordinate;

            if (hasUpNode)
                yield return upCoordinate;

            if (hasDownNode)
                yield return downCoordinate;
        }
    }
}