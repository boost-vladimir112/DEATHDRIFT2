using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class GetBonusMoney : MonoBehaviour
{
    public string rewardID;

    [SerializeField] GameObject bonusPanel;
    [SerializeField] GameObject hidenTextTG;
    private int balance;
    private int bonusChecker;

    [SerializeField] private TextMeshProUGUI balanceText;
    [SerializeField] private TextMeshProUGUI timerText;

    private DateTime nextAvailableTime;
    private bool canShowReward = true;
    private const string NextRewardTimeKey = "NextRewardTime";
    public GameObject rewardButton;

    private void Start()
    {
        // Загружаем баланс и бонус
        bonusChecker = YG2.saves.tgbonus;
        balance = YG2.saves.money2;
        Debug.Log(balance);
        if (bonusChecker > 0)
        {
            hidenTextTG.SetActive(false);
        }

        // Загружаем сохраненное время рекламы
        if (PlayerPrefs.HasKey(NextRewardTimeKey))
        {
            long temp = Convert.ToInt64(PlayerPrefs.GetString(NextRewardTimeKey));
            nextAvailableTime = DateTime.FromBinary(temp);
            if (DateTime.Now >= nextAvailableTime)
            {
                canShowReward = true;
            }
            else
            {
                canShowReward = false;
            }
        }
        else
        {
            canShowReward = true;
        }

        UpdateTimerUI();
    }

    private void Update()
    {
        if (!canShowReward)
        {
            TimeSpan remaining = nextAvailableTime - DateTime.Now;
            if (remaining.TotalSeconds <= 0)
            {
                canShowReward = true;
                //timerText.text = "Бонус готов!";
                rewardButton.SetActive(true);
            }
            else
            {
                rewardButton.SetActive(false) ;
                timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", remaining.Hours, remaining.Minutes, remaining.Seconds);
            }
        }
    }

    public void OpenChannel()
    {
        Application.OpenURL("https://t.me/tsukuyomi05");
        if (bonusChecker == 0)
        {
            balance += 800;
            balanceText.text = balance.ToString();
            YG2.saves.money2 = balance;
            
            bonusChecker++;
            hidenTextTG.SetActive(false);

            YG2.saves.tgbonus = bonusChecker;
            YG2.SaveProgress();
            // YG2.SetState("tgbonus", bonusChecker);


        }
    }

    public void OpenPanel()
    {
        bonusPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        bonusPanel.SetActive(false);
    }

    public void MyRewardAdvShow()
    {
        if (canShowReward)
        {
            YG2.RewardedAdvShow(rewardID, () =>
            {
                // Получение вознаграждения
                balance += 300;
                balanceText.text = balance.ToString();
                YG2.saves.money2 = balance;
                YG2.SaveProgress();

                // Устанавливаем следующее доступное время (+1 час)
                nextAvailableTime = DateTime.Now.AddHours(1);
                PlayerPrefs.SetString(NextRewardTimeKey, nextAvailableTime.ToBinary().ToString());
                PlayerPrefs.Save();

                canShowReward = false;
                UpdateTimerUI();
            });
        }
        else
        {
            Debug.Log("Бонус пока недоступен!");
        }
    }

    private void UpdateTimerUI()
    {
        if (canShowReward)
        {
            //timerText.text = "Бонус готов!";
        }
        else
        {
            TimeSpan remaining = nextAvailableTime - DateTime.Now;
            if (remaining.TotalSeconds > 0)
            {
                timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", remaining.Hours, remaining.Minutes, remaining.Seconds);
            }
            else
            {
             //   timerText.text = "Бонус готов!";
            }
        }
    }
}
