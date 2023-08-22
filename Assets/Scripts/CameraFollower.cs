using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    [Range(0, 1)]
    float smoothTime;

    [SerializeField]
    Vector2 minLimit;

    [SerializeField]
    Vector2 maxLimit;

    Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 position = target.position;
        position.z = transform.position.z;

        position.x = Mathf.Clamp(position.x, minLimit.x, maxLimit.x);
        position.y = Mathf.Clamp(position.y, minLimit.y, maxLimit.y);

        transform.position =
            Vector3.SmoothDamp(transform.position, position, ref velocity, smoothTime);
    }

    
}
