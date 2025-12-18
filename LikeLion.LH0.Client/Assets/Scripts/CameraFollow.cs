using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    [SerializeField] private Vector3 offset;

    [SerializeField] private float smoothSpeed = 5f;

    [SerializeField] private Vector2 minBounds, maxBounds;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 destination = target.position + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, destination, smoothSpeed * Time.deltaTime);

            smoothPosition.x = Mathf.Clamp(smoothPosition.x, minBounds.x, maxBounds.x);
            smoothPosition.y = Mathf.Clamp(smoothPosition.y, minBounds.y, maxBounds.y);
            
            transform.position = smoothPosition;
        }
    }
}