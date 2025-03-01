using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private string levelName; // The name of the level
    public void LoadLevel(string levelName)
    {
        // Load the level
        SceneManager.LoadScene(levelName);
    }
}
