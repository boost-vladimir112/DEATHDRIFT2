using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PG;
using YG;

public class Finish : MonoBehaviour
{
    private List<GameObject> finishedCars = new List<GameObject>(); // ����������� ����� �� ������
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
        if (other.CompareTag("PlayerCar") || other.CompareTag("Car")) // ��������� � ������, � AI
        {
            if (!finishedCars.Contains(other.gameObject)) // ���������, �� ���������� �� ������ ����� ������
            {
                finishedCars.Add(other.gameObject); // ��������� ������ � ������

                if (other.CompareTag("PlayerCar"))
                {
                    int position = finishedCars.Count; // ���������� ����� ������
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



                    Debug.Log("����� ����������� �� �����: " + position);
                }
            }
        }
    }
}
