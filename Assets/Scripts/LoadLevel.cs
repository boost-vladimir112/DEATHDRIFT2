using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public int level;
   public void LoadThisLevel()
    {
        SceneManager.LoadScene(level);
    }
}
