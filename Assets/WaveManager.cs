using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private float timer; // The timer
    [SerializeField] private float waveTimer; // The wave timer
    [SerializeField] private int waveNumber; // The wave number
    [SerializeField] private GameObject[] spawnGroups; // The spawn groups that contain the spawn points
    [SerializeField] private UIManager uiManager; // The UI manager

    private int currentWave = 0; // The current wave

    void Start()
    {
        timer = waveTimer;
    }

}
