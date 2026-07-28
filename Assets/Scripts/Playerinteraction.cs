using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactDistance = 3f;

    private GameObject currentTarget;

    void Update()
    {
        FindInteractableTarget();
        HandleInputs();
    }

    private void FindInteractableTarget()
    {
        Ray ray = (Cursor.lockState == CursorLockMode.Locked) ?
                playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)) :
                playerCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            // Simply capture whatever interactable object we are looking at
            currentTarget = hit.collider.gameObject;
        }
        else
        {
            currentTarget = null;
        }
    }

    private void HandleInputs()
    {
        // Only check for clicks if we are currently looking at a valid interactable object
        if (currentTarget != null)
        {
            // Left Mouse Button Click to trigger the interaction
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("LMB Clicked on: " + currentTarget.name, currentTarget);

                // Check if the target has an ElectricPanel component and trigger it
                ElectricPanel panel = currentTarget.GetComponentInParent<ElectricPanel>();
                if (panel != null)
                {
                    panel.OpenPanel();
                }

                // Check if the target has a Lever component and trigger it
                Lever lever = currentTarget.GetComponentInParent<Lever>();
                if (lever != null)
                {
                    lever.OpenPanel2();
                }
            }
        }
    }
}