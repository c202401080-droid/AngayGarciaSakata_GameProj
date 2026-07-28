using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmongUsSwitch : MonoBehaviour
{
    [Header("Visual Elements")]
    public GameObject switchUpObj;
    public GameObject switchDownObj;
    public GameObject lightOnObj;
    public GameObject lightOffObj;

    [Header("State")]
    public bool isOn = false;

    public static List<AmongUsSwitch> allSwitches = new List<AmongUsSwitch>();

    private void OnEnable()
    {
        if (!allSwitches.Contains(this))
            allSwitches.Add(this);
    }

    private void OnDisable()
    {
        if (allSwitches.Contains(this))
            allSwitches.Remove(this);
    }

    void Start()
    {
        UpdateVisuals();
    }

    void Update()
    {
        // Listen for left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to 2D world space
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            // Cast a tiny ray exactly where the mouse is
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            // If the ray hits THIS object's collider, toggle the switch
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                ToggleSwitch();
            }
        }
    }

    public void ToggleSwitch()
    {
        isOn = !isOn;
        UpdateVisuals();
        CheckCompletion();
    }

    void UpdateVisuals()
    {
        if (switchUpObj != null) switchUpObj.SetActive(isOn);
        if (switchDownObj != null) switchDownObj.SetActive(!isOn);

        if (lightOnObj != null) lightOnObj.SetActive(isOn);
        if (lightOffObj != null) lightOffObj.SetActive(!isOn);
    }

    void CheckCompletion()
    {
        int activeCount = 0;
        foreach (var sw in allSwitches)
        {
            if (sw != null && sw.isOn)
            {
                activeCount++;
            }
        }

        if (activeCount >= 5)
        {
            Debug.Log("Task Complete! All switches are active.");
        }
    }
}