using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private List<GameObject> finishedCars = new List<GameObject>(); // ����������� ����� �� ������

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
                    Debug.Log("����� ����������� �� �����: " + position);
                }
            }
        }
    }
}
