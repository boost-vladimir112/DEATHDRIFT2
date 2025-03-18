using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PG;
using YG;

public class Finish : MonoBehaviour
{
    private List<GameObject> finishedCars = new List<GameObject>(); // Очередность машин на финише
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private GameObject winPanel;

    [SerializeField] private int rewardForLevel; // decide for 3
    [SerializeField] private TextMeshProUGUI rewardText;

    private int balance;

    private void Start()
    {
        balance = YG2.GetState("money2");
        Debug.Log(balance);
    }

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

                    int finalReward = rewardForLevel / position;
                    rewardText.text = finalReward.ToString();

                    balance += finalReward;
                    Debug.Log(balance);
                    YG2.saves.money2 = balance;
                    YG2.SaveProgress();

                    winPanel.SetActive(true);

                    AudioListener.pause = true;
                    Time.timeScale = 0f;



                    Debug.Log("Игрок финишировал на месте: " + position);
                }
            }
        }
    }
}
