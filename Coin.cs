using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private GameSessionManager gameSessionManager;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioClip sound;
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rb;

    private void FixedUpdate() 
    {
        Color updatedColor = Color.HSVToRGB(Random.value, 1, 1);
        spriteRenderer.color = updatedColor;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().coinCount += value;
            SpawnAudioObject();
            Destroy(gameObject);
        }
    }

    private void SpawnAudioObject()
    {
        GameObject audioObject = new GameObject("audioObject");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        Despawn despawner = audioObject.AddComponent<Despawn>();
        despawner.timeTillDespawn = sound.length;
        audioSource.clip = sound;
        audioSource.Play();
    }
}
