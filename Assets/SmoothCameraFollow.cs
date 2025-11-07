using UnityEngine;

    public class SmoothCameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target;          
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 2, -10); 

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Camera target is not assigned!");
            return;
        }

        
        Vector3 desiredPosition = target.position + offset;

       
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

       
        transform.position = smoothedPosition;

       
        transform.LookAt(target);
    }
}


