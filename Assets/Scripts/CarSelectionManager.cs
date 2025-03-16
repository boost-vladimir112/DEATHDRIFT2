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
    public GameObject selectText;
    public GameObject buyText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI balanceText;

    public int balance;

    private void Start()
    {
        // ���� ���� ������ (������ ������), ������� ������ ������
        if (YG2.saves.carOwned.Count == 0)
        {
            YG2.saves.carOwned.Add(0); // ������ ����������
            YG2.SaveProgress();
        }

        selectedCarIndex = YG2.saves.SelectedCar;
        balance = YG2.GetState("money");
        balanceText.text = balance.ToString();


        UpdateUI();
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
            // ������������� ��������� ������
            YG2.saves.SelectedCar = selectedCarIndex;
        }
        else
        {
            // ������� ������
            if (balance >= carPrices[selectedCarIndex])
            {
                balance -= carPrices[selectedCarIndex];
                balanceText.text = balance.ToString();
                YG2.saves.money = balance;
                YG2.SetState("money", balance);
                YG2.saves.carOwned.Add(selectedCarIndex);
                YG2.saves.SelectedCar = selectedCarIndex; // ����� ������� ����� �������
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

        // ���������, ����������� �� ������
        if (IsCarOwned(selectedCarIndex))
        {
            priceText.text = "";

            // ���������, ������� �� ���
            if (selectedCarIndex == YG2.saves.SelectedCar)
            {
                // ������ ������� � ������� ������
                actionButton.gameObject.SetActive(false);
            }
            else
            {
                // ������ �������, �� �� �������
                actionButton.gameObject.SetActive(true);
                selectText.SetActive(true);
                buyText.SetActive(false);
            }
        }
        else
        {
            // ������ �� �������
            actionButton.gameObject.SetActive(true);
            selectText.SetActive(false);
            buyText.SetActive(true);
            priceText.text = " " + carPrices[selectedCarIndex];
        }
    }
}
