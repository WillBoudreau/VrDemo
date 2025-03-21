using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private PlayerController player; // The player object
    [SerializeField] private CowBehaviour boss; // The boss object
    [SerializeField] private static GameManager instance; // The instance of the game manager
    [SerializeField] private UIManager uiManager; // The UI manager
    [SerializeField] private LevelManager levelManager; // The level manager

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<CowBehaviour>();
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }
    void Update()
    {
        CheckPlayerHealth();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// Check health of the player
    /// </summary>
    public void CheckPlayerHealth()
    {
        if (player.playerHealth <= 0)
        {
            uiManager.SetUI("GameOverUI");
            levelManager.LoadLevel("GameOver");
        }
    }
}
