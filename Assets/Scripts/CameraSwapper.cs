using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject Camera_1; // Your main player camera
    public GameObject Camera_2; // Your "MinigameCamera1"
    public int Manager;

    public void ManageCamera()
    {
        if (Manager == 0)
        {
            Cam_2();
            Manager = 1;
        }
        else
        {
            Cam_1();
            Manager = 0;
        }
    }

    public void Cam_1()
    {
        Camera_1.SetActive(true);
        Camera_2.SetActive(false);

        // Lock mouse back to first-person view
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Cam_2()
    {
        Camera_1.SetActive(false);
        Camera_2.SetActive(true);

        // Unlock mouse so you can interact with the minigame UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}