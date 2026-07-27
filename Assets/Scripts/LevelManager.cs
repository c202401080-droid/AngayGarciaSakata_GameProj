using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Submarine submarineHP;
    [SerializeField] private Timer gameTimer;

    [Header("UI Canvases")]
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject winCanvas;

    private bool isGameOver = false;
    private bool isGameWon = false;

    void Update()
    {
        if (isGameOver || isGameWon) return;

        // Checks Sub HP
        if (submarineHP != null)
        {
            if (submarineHP.Health <= 0)
            {
                TriggerGameOver();
            }
        }

        // Checks Timer 
        if (gameTimer != null)
        {
            if (gameTimer.RemainingTime <= 0)
            {
                TriggerWin();
            }
        }
    }

    private void TriggerGameOver()
    {
        isGameOver = true;

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }

       
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Submarine health is 0. Game Over canvas activated!");
    }

    private void TriggerWin()
    {
        isGameWon = true;

        if (winCanvas != null)
        {
            winCanvas.SetActive(true);
        }

        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Timer reached 0. Win canvas activated!");
    }
}
