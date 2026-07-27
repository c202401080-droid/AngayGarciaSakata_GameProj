using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactDistance = 3f;

    private GameObject currentTarget;
    private float resetTimer = 0f;
    private float gracePeriod = 0.15f;

    void Update()
    {
        HandleHover();
        HandleInputs();
    }

    private void HandleHover()
    {
        Ray ray = (Cursor.lockState == CursorLockMode.Locked) ?
                playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)) :
                playerCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            Outline foundOutline = hit.collider.GetComponentInParent<Outline>();
            if (foundOutline != null)
            {
                GameObject hitObject = foundOutline.gameObject;
                if (hitObject != currentTarget)
                {
                    RemoveHighlight();
                    currentTarget = hitObject;
                }
                resetTimer = gracePeriod;
                AddHighlight();
            }
            else { HandleMissingTarget(); }
        }
        else { HandleMissingTarget(); }
    }

    private void HandleInputs()
    {
        // Only check for clicks if we are currently looking at a valid interactable object
        if (currentTarget != null)
        {
            // Left Mouse Button Click to trigger the Electric Panel minigame/interaction
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("LMB Clicked on: " + currentTarget.name, currentTarget);

                // Check if the target has an ElectricPanel component and trigger it
                ElectricPanel panel = currentTarget.GetComponentInParent<ElectricPanel>();
                if (panel != null)
                {
                    panel.OpenPanel();
                }
            }
        }
    }

    private void HandleMissingTarget()
    {
        if (currentTarget != null)
        {
            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0f) { RemoveHighlight(); }
        }
    }

    private void AddHighlight()
    {
        if (currentTarget == null) return;
        Outline outline = currentTarget.GetComponentInChildren<Outline>();
        if (outline != null) outline.OutlineWidth = 5f;
    }

    private void RemoveHighlight()
    {
        if (currentTarget != null)
        {
            Outline outline = currentTarget.GetComponentInChildren<Outline>();
            if (outline != null) outline.OutlineWidth = 0f;
        }
        currentTarget = null;
    }
}