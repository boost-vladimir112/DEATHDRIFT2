using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PG;
using YG;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public string rewardID = "multiply";
    private List<GameObject> finishedCars = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private GameObject winPanel;
    private int playerRating;
    private int level;
    private int currentLevel;
    private int position;
    private int finalReward;

    [SerializeField] private int rewardForLevel;
    [SerializeField] private int rewardRating;
    [SerializeField] private TextMeshProUGUI rewardRatingText;
    [SerializeField] private TextMeshProUGUI rewardText;

    private int balance;

    private void Start()
    {
        if (PlayerPrefs.GetInt("SoundEnabled") == 1)
            AudioListener.pause = false;
        else
            AudioListener.pause = true;

        currentLevel = SceneManager.GetActiveScene().buildIndex;
        balance = YG2.saves.money2;
        level = YG2.saves.level;
        playerRating = YG2.saves.playerRating;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCar") || other.CompareTag("Car"))
        {
            if (!finishedCars.Contains(other.gameObject))
            {
                finishedCars.Add(other.gameObject);

                if (other.CompareTag("PlayerCar"))
                {
                    position = finishedCars.Count;
                    winText.text = position.ToString();

                    NewRatingRecord();
                    NewBalance();

                    if (level <= currentLevel)
                    {
                        level = currentLevel;
                        YG2.saves.level = level;
                        YG2.SaveProgress();
                    }

                    YG2.SaveProgress();
                    winPanel.SetActive(true);
                    AudioListener.pause = true;
                    Time.timeScale = 0f;
                }
            }
        }
    }

    private void NewRatingRecord()
    {
        int ratingIncrease = rewardRating / position; // Считаем, сколько дадим рейтинга
        playerRating += ratingIncrease; // Обновляем рейтинг игрока в памяти

        YG2.saves.playerRating = playerRating;
        YG2.SetLeaderboard("rating", playerRating);

        StartCoroutine(AnimateRatingIncrease(ratingIncrease)); // Запускаем анимацию
    }


    private void NewBalance()
    {
        finalReward = rewardForLevel / position;
        rewardText.text = finalReward.ToString();

        YG2.saves.money2 = finalReward;
        YG2.SaveProgress();
        StartCoroutine(AnimateMoneyIncrease(finalReward)); // Запускаем анимацию увеличения денег
    }

    public void RewardMultiply()
    {
        if (IsCooldownActive()) return; // Если кулдаун активен, выходим

        YG2.RewardedAdvShow(rewardID, () =>
        {
            StartCoroutine(AnimateMoneyIncrease(finalReward*2)); // Анимируем деньги при умножении
            YG2.SaveProgress();

            // Находим скрипт ButtonCooldown и запускаем кулдаун
            ButtonCooldown cooldownScript = FindObjectOfType<ButtonCooldown>();
            if (cooldownScript != null)
            {
                cooldownScript.ActivateCooldown();
            }
        });
    }

    private bool IsCooldownActive()
    {
        if (!PlayerPrefs.HasKey("RewardCooldown")) return false;

        DateTime lastUseTime = DateTime.Parse(PlayerPrefs.GetString("RewardCooldown"));
        TimeSpan timePassed = DateTime.UtcNow - lastUseTime;

        return timePassed.TotalSeconds < 300; // 5 минут кулдаун
    }

    private IEnumerator AnimateMoneyIncrease(int amount)
    {
        int startAmount = 0; // Награда начинается с 0
        int targetAmount = amount; // Анимируем до полной награды
        float duration = 1f; // Время анимации в секундах
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime; // Используем unscaledDeltaTime
            int displayedAmount = (int)Mathf.Lerp(startAmount, targetAmount, elapsedTime / duration);
            rewardText.text = displayedAmount.ToString(); // Обновляем UI награды
            yield return null;
        }

        rewardText.text = targetAmount.ToString(); // Финальное обновление UI
    }
    private IEnumerator AnimateRatingIncrease(int amount)
    {
        int startAmount = 0;
        int targetAmount = amount;
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            int displayedAmount = (int)Mathf.Lerp(startAmount, targetAmount, elapsedTime / duration);
            rewardRatingText.text = displayedAmount.ToString();
            yield return null;
        }

        rewardRatingText.text = targetAmount.ToString();
    }


}
