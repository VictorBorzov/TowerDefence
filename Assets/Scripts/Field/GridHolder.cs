using System;
using UnityEngine;

namespace Field
{
    public class GridHolder : MonoBehaviour
    {
        [SerializeField] 
        private int gridWidth;

        [SerializeField] 
        private int gridHeight;

        [SerializeField]
        private float nodeSize;
        
        private Grid _grid;

        private Camera _camera;

        private Vector3 _offset;
        private void Awake()
        {
            _grid = new Grid(gridWidth, gridHeight);
            _camera = Camera.main;

            var width = gridWidth * nodeSize;
            var height = gridHeight * nodeSize;
            
            // Default plane size is 10 by 10
            transform.localScale = new Vector3(width * 0.1f, 1f, height * 0.1f);

            _offset = transform.position - (new Vector3(width, 0f, height) * 0.5f);
        }

        private void Update()
        {
            if (_grid == null || _camera == null)
                return;
            
            Vector3 mousePosition = Input.mousePosition;

            Ray ray = _camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform != transform)
                    return;
                
                Vector3 hitPosition = hit.point;
                Vector3 difference = hitPosition - _offset;

                int x = (int) (difference.x / nodeSize);
                int z = (int) (difference.z / nodeSize);
                Debug.Log($"{x} {z}");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_offset, 0.1f);
        }
    }
}