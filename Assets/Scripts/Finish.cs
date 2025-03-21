using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PG;
using YG;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private List<GameObject> finishedCars = new List<GameObject>(); // Очередность машин на финише
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private GameObject winPanel;
    private int level;
    private int currentLevel;

    [SerializeField] private int rewardForLevel; // decide for 3
    [SerializeField] private TextMeshProUGUI rewardText;

    private int balance;

    private void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        balance = YG2.saves.money2;
        level = YG2.saves.level;
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
                    if(level <= currentLevel)
                    {
                        level = currentLevel;
                        YG2.saves.level = level;
                        YG2.SaveProgress();
                    }
   
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
