using UnityEngine;
using UnityEngine.SceneManagement;

public class MyLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject gamePanel;
    public void OpenPanel()
    {
        gamePanel.SetActive(true);
    }
    public void ClosePanel() 
    {
        gamePanel.SetActive(false);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }

}
