using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  2D eyes follow target
public class EyeFollow : MonoBehaviour
{
    public Transform target;
    public float minimizeFactor = 0.25f;
    [Range(0, 1)]
    public float limitX = 0.15f, limitY = 0.05f;
    public float speed = 0.1f;
    private Vector3 center;
    void Start()
    {
        //Capture the starting position as a vector3
        center = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = target.position * minimizeFactor;
        targetPos = new Vector3(Mathf.Clamp(targetPos.x, -limitX, limitX), Mathf.Clamp(targetPos.y, -limitY, limitY), targetPos.z);
        Vector3 toPos = center + targetPos;
        Vector3 lerped = Vector3.Lerp(transform.position, new Vector3(toPos.x, toPos.y, transform.position.z), Mathf.Clamp(Mathf.Sqrt(speed * Time.deltaTime), 0, 1));
        transform.position = lerped;
    }
}
