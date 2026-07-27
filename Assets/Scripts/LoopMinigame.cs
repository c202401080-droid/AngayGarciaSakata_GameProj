using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameSpawnerManager : MonoBehaviour
{
    [Header("Prefab Reference")]
    [SerializeField] private GameObject minigamePrefab; // Drag your Wire Minigame prefab here

    [Header("Target Tag to Replace")]
    [SerializeField] private string targetTag = "Minigame1";

    // Call this method to delete existing objects with the tag and instantiate the new prefab
    public void ReplaceMinigame()
    {
        // 1. Find and destroy any existing objects in the scene with the specified tag
        GameObject[] existingMinigames = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject obj in existingMinigames)
        {
            Destroy(obj);
        }

        // 2. Instantiate the new minigame prefab at default scene coordinates
        if (minigamePrefab != null)
        {
            GameObject newInstance = Instantiate(minigamePrefab);
            newInstance.SetActive(true);

            Debug.Log("Game successfully cloned");
        }
        else
        {
            Debug.LogError("MinigamePrefab is not assigned in the Spawner Manager!");
        }
    }
}