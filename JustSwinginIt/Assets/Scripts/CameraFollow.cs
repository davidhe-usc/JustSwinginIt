using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = .125f;

    [SerializeField]
    public Vector3 offset;

    private Vector3 def = new Vector3(-1, -1, -1);

    private void Start()
    {
        if(offset == def)
            offset = new Vector3(0, 3, -20);
    }

    private void FixedUpdate()
    {
        Vector3 destinPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, destinPosition, smoothSpeed);
        transform.position = smoothedPosition;

    }
}
