using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject GameUI; // The game UI
    [SerializeField] private GameObject DamageUI; // The damage UI
    [SerializeField] private GameObject GameOverUI; // The game over UI
    [SerializeField] private GameObject GameWinUI; // The game win UI
    public TextMeshProUGUI timerText; // The timer text 
    public Slider healthBar; // The health bar of the player


    void Start()
    {
        SetFalse();
        SetUI("GameUI");
    }

    /// <summary>
    /// Set the UI
    /// </summary>
    public void SetUI(string ui)
    {
        switch (ui)
        {
            case "GameUI":
                GameUI.SetActive(true);
                break;
            case "DamageUI":
                StartCoroutine(DamageUIAnimation());
                break;
            case "GameOverUI":
                GameOverUI.SetActive(true);
                break;
            case "GameWinUI":
                GameWinUI.SetActive(true);
                break;
        }   
    }
    void SetFalse()
    {
        GameUI.SetActive(false);
        DamageUI.SetActive(false);
        GameOverUI.SetActive(false);
        GameWinUI.SetActive(false);
    }
    /// <summary>
    /// Damage UI animation
    /// </summary>
    IEnumerator DamageUIAnimation()
    {
        // Set the damage UI to active
        DamageUI.SetActive(true);

        // Wait for 1 second
        yield return new WaitForSeconds(1);

        // Set the damage UI to inactive
        DamageUI.SetActive(false);
    }
}
