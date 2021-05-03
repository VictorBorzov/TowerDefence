using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class Grid
    {
        private readonly Node[,] _nodes;

        private readonly FlowFieldPathFinding _pathFinding;
        public int Width { get; }

        public int Height { get; }

        public Grid(int width, int height, Vector3 offset, float nodeSize, Vector2Int target)
        {
            Width = width;
            Height = height;

            _nodes = new Node[width, height];
            for (var i = 0; i < _nodes.GetLength(0); ++i)
            for (var j = 0; j < _nodes.GetLength(1); ++j)
                _nodes[i, j] = new Node(offset + new Vector3(i + .5f, 0, j + .5f) * nodeSize);

            _pathFinding = new FlowFieldPathFinding(this, target);

            _pathFinding.UpdateField();
        }

        public Node GetNode(Vector2Int coordinate)
        {
            return GetNode(coordinate.x, coordinate.y);
        }

        public Node GetNode(int i, int j)
        {
            if (i < 0 || i >= Width)
                return null;
            if (j < 0 || j >= Height)
                return null;
            return _nodes[i, j];
        }

        public IEnumerable<Node> EnumerateAllNodes()
        {
            for (var i = 0; i < Width; ++i)
            for (var j = 0; j < Height; ++j)
                yield return GetNode(i, j);
        }

        public void UpdatePathFinding()
        {
            _pathFinding.UpdateField();
        }
    }
}