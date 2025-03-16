using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PG;

public class Finish : MonoBehaviour
{
    private List<GameObject> finishedCars = new List<GameObject>(); // ����������� ����� �� ������
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private GameObject winPanel;
   [SerializeField] GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCar") || other.CompareTag("Car")) // ��������� � ������, � AI
        {
            if (!finishedCars.Contains(other.gameObject)) // ���������, �� ���������� �� ������ ����� ������
            {
                finishedCars.Add(other.gameObject); // ��������� ������ � ������

                if (other.CompareTag("PlayerCar"))
                {
                    int position = finishedCars.Count; // ���������� ����� ������
                    winText.text = position.ToString();
                    winPanel.SetActive(true);

                    AudioListener.volume = 0;
                    Time.timeScale = 0f;



                    Debug.Log("����� ����������� �� �����: " + position);
                }
            }
        }
    }
}
