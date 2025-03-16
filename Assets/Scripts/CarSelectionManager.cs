using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public class CarSelectionManager : MonoBehaviour
{
    public GameObject[] cars; // Массив машин
    public int[] carPrices; // Цена машин (первая бесплатная)
    private int selectedCarIndex = 0;

    public Button leftButton;
    public Button rightButton;
    public Button actionButton;
    public TextMeshProUGUI actionButtonText;
    public TextMeshProUGUI priceText;

    public int balance;


    private void Start()
    {
        selectedCarIndex = YG2.GetState("SelectedCar");
        balance = YG2.GetState("money");

        // Проверка и инициализация массива купленных машин
        if (YG2.GetState<int[]>("carsOwned") == null || YG2.GetState<int[]>("carsOwned").Length != cars.Length)
        {
            int[] initialCarsOwned = new int[cars.Length];
            initialCarsOwned[0] = 1; // Первая машина бесплатная
            YG2.SetState("carsOwned", initialCarsOwned);
        }

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
            YG2.SetState("SelectedCar", selectedCarIndex);
        }
        else
        {
            if (balance >= carPrices[selectedCarIndex])
            {
                balance -= carPrices[selectedCarIndex];
                YG2.SetState("money", balance);

                // Отмечаем машину как купленную
                int[] ownedCars = YG2.GetState<int[]>("carsOwned");
                ownedCars[selectedCarIndex] = 1;
                YG2.SetState("carsOwned", ownedCars);
            }
        }
        UpdateUI();
    }

    private bool IsCarOwned(int index)
    {
        int[] ownedCars = YG2.GetState<int[]>("carsOwned");
        return ownedCars != null && ownedCars[index] == 1;
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
            priceText.text = "Цена: " + carPrices[selectedCarIndex];
        }
    }
}
