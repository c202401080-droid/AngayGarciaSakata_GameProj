using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private SwitchCamera cameraSwitcher;

    public void OpenPanel2()
    {
        // If the SwitchCamera script is on the same object, or you can assign it via Inspector
        if (cameraSwitcher == null)
        {
            cameraSwitcher = GetComponent<SwitchCamera>();
        }

        if (cameraSwitcher != null)
        {
            cameraSwitcher.ManageCamera();
        }
        else
        {
            Debug.LogWarning("SwitchCamera script not found on the Electric Panel!", this);
        }
    }
}
