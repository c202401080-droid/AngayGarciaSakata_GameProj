using UnityEngine;

public class ReplaceMinigame : MonoBehaviour
{
    [Header("The new prefab from your Assets folder")]
    [SerializeField] private GameObject newMinigamePrefab;

    // Call this method to trigger the swap
    public void SwapMinigame()
    {
        // 1. Find the active minigame in the scene using the tag
        GameObject oldMinigame = GameObject.FindGameObjectWithTag("Minigame1");

        if (oldMinigame != null)
        {
            // Save the exact position and rotation of the old minigame
            Vector3 spawnPosition = oldMinigame.transform.position;
            Quaternion spawnRotation = oldMinigame.transform.rotation;

            // 2. Destroy the old minigame from the hierarchy
            Destroy(oldMinigame);

            // 3. Spawn the new assigned prefab in its exact place
            if (newMinigamePrefab != null)
            {
                Instantiate(newMinigamePrefab, spawnPosition, spawnRotation);
                Debug.Log("Old minigame destroyed. New prefab spawned!");
            }
            else
            {
                Debug.LogWarning("Missing Prefab! Please assign the new prefab in the Inspector.");
            }
        }
        else
        {
            Debug.LogWarning("Could not find any object tagged 'minigame1' in the scene.");
        }
    }
}