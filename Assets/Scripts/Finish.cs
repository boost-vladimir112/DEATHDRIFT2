using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private List<GameObject> finishedCars = new List<GameObject>(); // Очередность машин на финише

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCar") || other.CompareTag("Car")) // Проверяем и игрока, и AI
        {
            if (!finishedCars.Contains(other.gameObject)) // Проверяем, не пересекала ли машина финиш раньше
            {
                finishedCars.Add(other.gameObject); // Добавляем машину в список

                if (other.CompareTag("PlayerCar"))
                {
                    int position = finishedCars.Count; // Определяем место игрока
                    Debug.Log("Игрок финишировал на месте: " + position);
                }
            }
        }
    }
}
