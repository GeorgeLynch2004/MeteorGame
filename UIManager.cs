using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameSessionManager gameSessionManager;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI longestTimeAlive;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI currentTime;
    [SerializeField] private TextMeshProUGUI currentHealth;
    [SerializeField] private TextMeshProUGUI meteorsPerSecond;
    

    // Update is called once per frame
    void Update()
    {
        highScore.text = "High_score:" + gameSessionManager.maxScore;
        longestTimeAlive.text = "Best_time:" + Mathf.RoundToInt(gameSessionManager.maxTime);
        currentScore.text = "Score:" + player.coinCount;
        currentTime.text = "Time:" + Mathf.RoundToInt(gameSessionManager.currentTime);
        currentHealth.text = "Health:" + gameSessionManager.player.GetComponent<HealthSystem>().currentHealth;
        meteorsPerSecond.text = "MPS:" + System.Math.Round(gameSessionManager.spawnsPerSecond, 1);
    }
}
