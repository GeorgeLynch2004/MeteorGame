using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] public float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private SpriteRenderer bodyRenderer;
    private float colorChangeCurrentTimer;
    [SerializeField] private float colorChangeMaxTimer;
    private bool timerRunning;
    private float previousFrameHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        colorChangeCurrentTimer = 0;
        timerRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Health ran out");
            currentHealth = 0;
        }

        if (currentHealth < previousFrameHealth && previousFrameHealth != 0)
        {
            timerRunning = true;
        }
        
        if (timerRunning && colorChangeCurrentTimer < colorChangeMaxTimer)
        {
            bodyRenderer.color = new Color(1f,0f,0f);
            colorChangeCurrentTimer += Time.deltaTime;
        }
        else
        {
            timerRunning = false;
            bodyRenderer.color = new Color(1f,1f,1f);
            colorChangeCurrentTimer = 0;
        }


        previousFrameHealth = currentHealth;
    }
}
