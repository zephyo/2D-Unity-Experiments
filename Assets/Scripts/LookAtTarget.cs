using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LookAtTarget : MonoBehaviour
{
    public float rotationSpeed = 0.5f;
    public Transform target;
    public Vector3 upwards = Vector3.back;
    public float distanceFactor = 1;

    void Update()
    {
        //  decrease distance between transform rotation and target by distanceFactor
        Vector3 targetPos = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, target.transform.position.z), target.transform.position, distanceFactor);
        transform.rotation = LookAt(transform.rotation, targetPos);
    }

    private Quaternion LookAt(Quaternion original, Vector3 target)
    {
        var rotate = Quaternion.LookRotation(target, upwards);
        Quaternion lerped = Quaternion.Slerp(transform.rotation, rotate, Mathf.Clamp(Mathf.Sqrt(rotationSpeed * Time.deltaTime), 0, 1));
        return lerped;
    }
}
