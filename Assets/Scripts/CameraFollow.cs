using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float offsetY = 0f;
    public float offsetZ = -10f;

    private void Start()
    {
        transform.parent = null;
    }

    private void Update()
    {
        transform.position = new Vector3(target.position.x, offsetY, offsetZ);
    }
}
