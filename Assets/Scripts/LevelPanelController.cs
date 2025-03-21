using UnityEngine;
using YG; // Подключаем Yandex Games SDK

public class LevelsOpener : MonoBehaviour
{
    public GameObject[] levelPanels; // Массив панелей уровней (по порядку!)

    void Start()
    {
        int savedLevel = YG2.saves.level;
        Debug.Log(savedLevel);

        for (int i = 0; i < levelPanels.Length; i++)
        {
            if (i < savedLevel)
            {
                levelPanels[i].SetActive(false); // Открытая панель
            }
            else
            {
                levelPanels[i].SetActive(true); // Закрытая панель
            }
        }
    }
}
