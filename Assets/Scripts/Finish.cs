using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Finish : MonoBehaviour
{
    private List<GameObject> finishedCars = new List<GameObject>(); // ����������� ����� �� ������
    [SerializeField] private TextMeshProUGUI winText;

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

                    Debug.Log("����� ����������� �� �����: " + position);
                }
            }
        }
    }
}
