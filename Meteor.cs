using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private float timeTillDestroy;
    [SerializeField] private float fallSpeed;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer trailRenderer;
    [SerializeField] private AudioClip sound;
    [SerializeField] private float volume;
    [SerializeField] private bool playSoundOnDeath;
    private float currentTimer;

    // Start is called before the first frame update
    void Start()
    {
        currentTimer = 0f;
        if (bodyRenderer != null){bodyRenderer.color = new Color(1, Random.value, Random.value);}
        if (trailRenderer != null){trailRenderer.color = new Color(1, Random.value, Random.value);}
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        pos.y -= fallSpeed * Time.deltaTime;

        transform.position = pos;

        if (currentTimer >= timeTillDestroy)
        {
            currentTimer = 0;
            Vector3 posAtDeath = transform.position;
            Quaternion rotAtDeath = new Quaternion(0,0,0,0);
            if (playSoundOnDeath){SpawnAudioObject();}
            if (objectToSpawn != null){Instantiate(objectToSpawn, posAtDeath, rotAtDeath);}
            Destroy(gameObject);
        }
        else
        {
            currentTimer += Time.deltaTime;
        }
    }

    public void SpawnAudioObject()
    {
        GameObject audioObject = new GameObject("audioObject");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        Despawn despawner = audioObject.AddComponent<Despawn>();
        despawner.timeTillDespawn = sound.length;
        audioSource.clip = sound;
        audioSource.volume = volume;
        audioSource.Play();
    }
}
