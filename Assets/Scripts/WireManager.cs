using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzleManager : MonoBehaviour
{
    [Header("SuccessTag")]
    [SerializeField] private string lightTag = "LightOn";

    [Header("Electric Panel")]
    [SerializeField] private GameObject electricPanelObject;

    [Header("EndMinigame")]
    [SerializeField] private GameObject endMinigameObject;

    [Header("Submarine")]
    [SerializeField] private GameObject submarineObject;

    private bool puzzleCompleted = false;

    
    void OnEnable()
    {
        ResetPuzzleState();
    }

    void Update()
    {
        
        if (puzzleCompleted) return;

        // Checks 4 lights are active. If so it passes
        if (CheckTaggedLightsActive())
        {
            puzzleCompleted = true;
            ConcludeMinigame();
        }
    }

    private bool CheckTaggedLightsActive()
    {
        
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(lightTag);

        if (taggedObjects == null || taggedObjects.Length < 4)
        {
            return false;
        }

        
        int activeCount = 0;
        foreach (GameObject obj in taggedObjects)
        {
            if (obj != null && obj.activeSelf)
            {
                activeCount++;
            }
        }

        return activeCount >= 4;
    }

    private void ConcludeMinigame()
    {
        Debug.Log("Pass");

        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
        if (electricPanelObject == null)
        {
            electricPanelObject = GameObject.Find("Electric Panel");
        }

        if (electricPanelObject != null)
        {
            
            electricPanelObject.SetActive(true);

            
            SwitchCamera camSwitcher = electricPanelObject.GetComponent<SwitchCamera>();
            if (camSwitcher != null)
            {
                camSwitcher.enabled = true;

                camSwitcher.Cam_1();
            }
            else
            {
                Debug.LogWarning("SwitchCamera script missing from the Electric Panel object!");
            }

        }
        else
        {
            Debug.LogWarning("Electric Panel object could not be found in the scene!");
        }

        if (endMinigameObject == null)

        {

            endMinigameObject = GameObject.Find("EndMinigame");

        }

        if (endMinigameObject != null)
        {

            endMinigameObject.SetActive(true);

            ReplaceMinigame MinigameRep = endMinigameObject.GetComponent<ReplaceMinigame>();
            if (MinigameRep != null)
            {
                MinigameRep.enabled = true;
                MinigameRep.SwapMinigame();
            }
            else
            {
                Debug.LogWarning("ReplaceMinigame script missing from the Electric Panel object!");
            }

        }

        if (submarineObject == null)
        {
            submarineObject = GameObject.Find("Level Manager");
        }

        if (submarineObject != null)
        {
            
            Submarine submarineHP = submarineObject.GetComponent<Submarine>();
            if (submarineHP != null)
            {
                submarineHP.SetHealth(25f);
                Debug.Log("+25");
            }
            else
            {
                Debug.LogWarning("Player (HP) script missing from the Submarine object!");
            }
        }
        else
        {
            Debug.LogWarning("Submarine object could not be found in the scene!");
        }

        ResetPuzzleState();


    }

    public void ResetPuzzleState()
    {
        puzzleCompleted = false;
    }
}