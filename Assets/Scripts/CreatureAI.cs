using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CreatureAI : MonoBehaviour
{
    [Header("AI Shit")]
    [SerializeField] private float checkInterval = 8f;
    [SerializeField] private Submarine submarineHP;
    [SerializeField] private Camera targetCamera;
    public MinigameManager minigameManager;

    private float timer;
    private bool isPaused = false;
    private GameObject playerCamObject;
    private bool previousPauseState = false; // Tracks state changes for clean debugging

    void Start()
    {
        timer = checkInterval;
        FindPlayerCam();
    }

    void Update()
    {
        // Continuously check if the PlayerCam object still exists or if its state changed
        if (playerCamObject == null)
        {
            FindPlayerCam();
        }
        else
        {
            // If the camera object is inactive, pause the AI
            bool camIsActive = playerCamObject.activeInHierarchy;
            isPaused = !camIsActive;
        }

        // Check if our pause state just changed and log it cleanly
        if (isPaused != previousPauseState)
        {
            if (isPaused)
            {
                Debug.Log("Creature AI: Paused (PlayerCam is disabled)");
            }
            else
            {
                Debug.Log("Creature AI: Unpaused (PlayerCam is active)");
            }

            previousPauseState = isPaused;
        }

        // If paused, don't tick down the timer
        if (isPaused) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            PerformAIMove();
            timer = checkInterval;
        }
    }

    private void FindPlayerCam()
    {
        GameObject camObj = GameObject.FindWithTag("PlayerCam");
        if (camObj != null)
        {
            playerCamObject = camObj;
        }
    }

    private void PerformAIMove()
    {
        int roll = Random.Range(1, 11);
        Debug.Log("Creature AI rolled a: " + roll);

        if (roll >= 6)
        {
            TriggerAttack();
        }
        else
        {
            Debug.Log("Miss");
        }
    }

    private void TriggerAttack()
    {
        // -20 hp
        if (submarineHP != null)
        {
            submarineHP.SetHealth(-20f);
        }

        if (minigameManager != null)
        {
            minigameManager.ActivateRandomMinigame();
        }

        // Triggers cam shake
        if (targetCamera != null)
        {
            if (EZCameraShake.CameraShaker.Instance != null)
            {
                EZCameraShake.CameraShaker.Instance.ShakeOnce(4f, 10f, 0.1f, 1.5f);
            }
        }

        // Activates cam shake for 1 sec then deactivates
        if (targetCamera != null)
        {
            CameraShaker camScript = targetCamera.GetComponent<CameraShaker>();
            if (camScript != null)
            {
                StartCoroutine(FlashScriptRoutine(camScript, 1f));
            }
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        Debug.Log("Hit");
    }

    // Turns on and off shake script
    private IEnumerator FlashScriptRoutine(MonoBehaviour scriptToFlash, float duration)
    {
        scriptToFlash.enabled = true;
        yield return new WaitForSeconds(duration);
        scriptToFlash.enabled = false;
    }
}