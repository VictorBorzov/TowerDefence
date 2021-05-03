using System.Collections;
using Field;
using UnityEngine;

namespace Unit
{
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private GridMovementAgent movementAgent;

        [SerializeField] private GridHolder gridHolder;

        private void Awake()
        {
            StartCoroutine(SpawnUnitsCoroutine());
        }

        private IEnumerator SpawnUnitsCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                SpawnUnit();
            }
        }

        private void SpawnUnit()
        {
            var startNode = gridHolder.Grid.GetNode(gridHolder.StartCoordinate);
            var position = startNode.Position;
            var newMovementAgent = Instantiate(movementAgent, position, Quaternion.identity);
            newMovementAgent.SetStartNode(startNode);
        }
    }
}