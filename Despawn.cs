using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour // Literally just a script to get a temporary gameobject to despawn
{
    [SerializeField] public float timeTillDespawn;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= timeTillDespawn)
        {
            Destroy(gameObject);
        }
    }
}
