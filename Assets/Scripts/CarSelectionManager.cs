using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;
using System.Collections.Generic;

public class CarSelectionManager : MonoBehaviour
{
    public GameObject[] cars;
    public int[] carPrices;
    private int selectedCarIndex = 0;

    public Button leftButton;
    public Button rightButton;
    public Button actionButton;
    public TextMeshProUGUI actionButtonText;
    public TextMeshProUGUI priceText;

    public int balance;

    private void Start()
    {
        // ���� ���� ������ (������ ������), ������� ������ ������
        if (YG2.saves.carOwned.Count == 0)
        {
            YG2.saves.carOwned.Add(0); // ������ ����������
            YG2.SaveProgress();
        }
        //YG2.SetState("money", 50000);
        selectedCarIndex = YG2.saves.SelectedCar;
        balance = YG2.GetState("money");

        UpdateUI();
    }
    private void Update()
    {
        Debug.Log(balance);
    }

    public void NextCar()
    {
        selectedCarIndex = (selectedCarIndex + 1) % cars.Length;
        UpdateUI();
    }

    public void PreviousCar()
    {
        selectedCarIndex--;
        if (selectedCarIndex < 0) selectedCarIndex = cars.Length - 1;
        UpdateUI();
    }

    public void OnActionButtonClick()
    {
        if (IsCarOwned(selectedCarIndex))
        {
            YG2.saves.SelectedCar = selectedCarIndex;
        }
        else
        {
            if (balance >= carPrices[selectedCarIndex])
            {
                balance -= carPrices[selectedCarIndex];
                YG2.saves.money = balance;
                YG2.saves.carOwned.Add(selectedCarIndex);
            }
        }
        YG2.SaveProgress();
        UpdateUI();
    }

    private bool IsCarOwned(int index)
    {
        return YG2.saves.carOwned.Contains(index);
    }

    private void UpdateUI()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(i == selectedCarIndex);
        }

        if (IsCarOwned(selectedCarIndex))
        {
            actionButtonText.text = "Select";
            priceText.text = "";
        }
        else
        {
            actionButtonText.text = "Buy";
            priceText.text = "����: " + carPrices[selectedCarIndex];
        }
    }
}
