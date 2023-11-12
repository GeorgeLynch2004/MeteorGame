using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModifier : MonoBehaviour
{
    [SerializeField] private float damageChange;
    [SerializeField] private bool destroyOnCollision;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.GetComponent<HealthSystem>() != null)
        {
            other.gameObject.GetComponent<HealthSystem>().currentHealth += damageChange;

            if (damageChange < 0)
            {
                other.gameObject.GetComponent<AudioSource>().Play();    
            }
            

            if (destroyOnCollision)
            {
                Meteor meteorComponent = GetComponent<Meteor>();
                meteorComponent.SpawnAudioObject();
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.GetComponent<HealthSystem>() != null)
        {
            other.gameObject.GetComponent<HealthSystem>().currentHealth += damageChange;

            if (destroyOnCollision)
            {
                Destroy(gameObject);
            }
        }
    }
}
