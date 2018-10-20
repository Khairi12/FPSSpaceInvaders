using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public float uFOKilled = 0f;
    public float alienKilled = 0f;
    public float enemiesToKill = 0f;

    public void ChangeLevel(int level)
    {
        SceneManager.LoadScene(level);

        if (level == 1)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // -----------------------------------------------------------------
    // PRIVATE
    // -----------------------------------------------------------------

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddUFOKill()
    {
        uFOKilled += 1f;

        if (uFOKilled >= enemiesToKill)
        {
            ChangeLevel(2);
        }
    }

    public void AddAlienKill()
    {
        alienKilled += 1f;
    }
}
