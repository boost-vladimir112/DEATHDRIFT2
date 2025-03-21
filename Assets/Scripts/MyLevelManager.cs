using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

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
        YG2.InterstitialAdvShow();
        SceneManager.LoadScene(level);
    }

}
