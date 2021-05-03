using UnityEngine;

public class MovementAgent : MonoBehaviour
{
    [SerializeField] 
    private float speed;

    [SerializeField] 
    private Vector3 target;
    
    private const float Tolerance = 0.1f;
    
    void Update()
    {
        float distance = (target - transform.position).magnitude; 
        if (distance < Tolerance)
            return;
        Vector3 direction = (target - transform.position).normalized;
        Vector3 delta = direction * (speed * Time.deltaTime );
        transform.Translate(delta);
    }
}
