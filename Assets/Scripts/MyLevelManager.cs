using UnityEngine;
using UnityEngine.SceneManagement;

public class MyLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject gamePanel = null;
    [SerializeField] private int level = 1;
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
        SceneManager.LoadScene(level);
    }

}
