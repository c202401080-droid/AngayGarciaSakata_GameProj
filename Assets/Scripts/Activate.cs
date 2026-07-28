using UnityEngine;

public class ActivateTargetObject : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;

    void Start()
    {
        // Activates the assigned object in the inspector
        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
    }
}