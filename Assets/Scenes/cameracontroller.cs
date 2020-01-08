using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour
{
    [Header("速度"), Range(0, 10)]
    public float speed = 3;

    private Transform target;

    private void Start()
    {
        target = GameObject.Find("Playr").transform;
    }

    private void LateUpdate()
    {
        Vector3 cam = transform.position;
        Vector3 tar = target.position;
        tar.z = -20;
        tar.y = Mathf.Clamp(tar.y, -1, 1);
        transform.position = Vector3.Lerp(cam, target.position, 0.3f * Time.deltaTime * speed);
    }
}
