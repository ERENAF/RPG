

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject panelUI;
    private bool isPanelUIActive;

    void Update()
    {
        SetActivePanelUI(false);
    }

    public void SetActivePanelUI(bool isButton)
    {
        if (panelUI != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || isButton)
            {
                isPanelUIActive = !isPanelUIActive;
                panelUI.SetActive(isPanelUIActive);
                if (isPanelUIActive)
                {
                    Time.timeScale = 0f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
