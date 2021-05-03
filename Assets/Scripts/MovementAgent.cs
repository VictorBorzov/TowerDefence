using UnityEngine;

public class MovementAgent : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Vector3 target;

    private const float Tolerance = 0.1f;

    private void Update()
    {
        var distance = (target - transform.position).magnitude;
        if (distance < Tolerance)
            return;
        var direction = (target - transform.position).normalized;
        var delta = direction * (speed * Time.deltaTime);
        transform.Translate(delta);
    }
}