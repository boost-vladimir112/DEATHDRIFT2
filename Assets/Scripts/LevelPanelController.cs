using UnityEngine;
using YG; // ���������� Yandex Games SDK

public class LevelsOpener : MonoBehaviour
{
    public GameObject[] levelPanels; // ������ ������� ������� (�� �������!)

    void Start()
    {
        int savedLevel = YG2.saves.level;
        Debug.Log(savedLevel);

        for (int i = 0; i < levelPanels.Length; i++)
        {
            if (i < savedLevel)
            {
                levelPanels[i].SetActive(false); // �������� ������
            }
            else
            {
                levelPanels[i].SetActive(true); // �������� ������
            }
        }
    }
}
