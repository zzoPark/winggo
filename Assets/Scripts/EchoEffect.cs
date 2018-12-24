using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    public GameObject echo;
    public float timeBetweenSpawns = 0.05f;
    public float lifetime = 5f;
    private float timer;

    private void Start()
    {
        timer = timeBetweenSpawns;
    }

    private void Update()
    {
        if (timer <= 0)
        {
            GameObject instance = Instantiate(echo, transform.position, Quaternion.identity);
            Destroy(instance, lifetime);
            timer = timeBetweenSpawns;
        } else
        {
            timer -= Time.deltaTime;
        }
    }
}
