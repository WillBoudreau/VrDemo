using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private GameObject player; // The player object
    [SerializeField] private static GameManager instance; // The instance of the game manager
    [SerializeField] private UIManager uiManager; // The UI manager

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
