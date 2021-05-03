using System;
using UnityEngine;

namespace Field
{
    public class Grid
    {
        private Node[,] _nodes;

        private int _width;
        
        private int _height;

        public Grid(int width, int height)
        {
            _width = width;
            _height = height;

            _nodes = new Node[width, height];
            for (int i = 0; i < _nodes.GetLength(0); ++i)
            for (int j = 0; j < _nodes.GetLength(1); ++j)
                 _nodes[i, j] = new Node();
        }

        public Node GetNode(Vector2Int coordinate)
        {
            return GetNode(coordinate.x, coordinate.y);
        }
        
        public Node GetNode(int i, int j)
        {
            if (i < 0 || i >= _width)
                return null;
            if (j < 0 || j >= _height)
                return null;
            return _nodes[i, j];
        }
    }
}