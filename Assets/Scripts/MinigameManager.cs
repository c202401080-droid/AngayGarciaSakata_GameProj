using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    [Header("Configurable Minigames")]
    [SerializeField] private List<GameObject> minigames = new List<GameObject>();

    void Start()
    {
        // Ensure all minigames are hidden/disabled when the game starts
        DisableAll();
    }

    public void ActivateRandomMinigame()
    {
        // Safety check to make sure the list isn't empty
        if (minigames == null || minigames.Count == 0)
        {
            Debug.LogWarning("No minigames assigned in the MinigameManager!");
            return;
        }

        // First, make sure we reset and turn off any currently active minigames
        DisableAll();

        // Pick a random index based on how many items are currently in the list
        int randomIndex = Random.Range(0, minigames.Count);

        Debug.Log("Minigame Index Chosen: " + randomIndex);

        // Turn on the chosen minigame
        GameObject selectedMinigame = minigames[randomIndex];
        if (selectedMinigame != null)
        {
            selectedMinigame.SetActive(true);
        }
    }

    private void DisableAll()
    {
        // Loop through every minigame in the list and turn them all off
        foreach (var minigame in minigames)
        {
            if (minigame != null)
            {
                minigame.SetActive(false);
            }
        }
    }
}