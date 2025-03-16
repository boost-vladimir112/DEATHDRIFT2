using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PG;

public class Finish : MonoBehaviour
{
    private List<GameObject> finishedCars = new List<GameObject>(); // Очередность машин на финише
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private GameObject winPanel;
   [SerializeField] GameController gameController;

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
                    winText.text = position.ToString();
                    winPanel.SetActive(true);

                    AudioListener.volume = 0;
                    Time.timeScale = 0f;



                    Debug.Log("Игрок финишировал на месте: " + position);
                }
            }
        }
    }
}
