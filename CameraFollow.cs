using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public Transform selfTrans;
    public float smoothspeed = 0.125f;
    public Vector3 offset;


    private void Start()
    {
        selfTrans = transform;
       
        offset = new Vector3(0,0,-10);
    }
    void FixedUpdate()
    {
        selfTrans = transform;
        
        Vector3 desiredPosition = Target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothspeed);
        transform.position = smoothedPosition;
    }

}
