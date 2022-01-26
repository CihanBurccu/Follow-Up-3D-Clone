using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public int camDelay = 2;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * camDelay);
    }
}
