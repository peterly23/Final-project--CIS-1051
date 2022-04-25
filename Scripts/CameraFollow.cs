using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(1,10)]
    public float smoothFactor;
    public Vector3 minValues, maxValue;
    

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        //Definte minimum x,y,z values and maximum x,y,x values
        Vector3 targetPosition = target.position + offset;
        //verify if the targetPosition is out of bound or not
        //limit to the min and max value
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValues.x, maxValue.x),
            Mathf.Clamp(targetPosition.y, minValues.y, maxValue.y),
            Mathf.Clamp(targetPosition.z, minValues.z, maxValue.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor*Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
