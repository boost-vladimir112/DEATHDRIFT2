using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarSelectionManager : MonoBehaviour
{
    public GameObject[] cars; // Массив машин
    public int[] carPrices = { 0, 500, 1000, 1500 }; // Цена машин (первая бесплатная)
    private int selectedCarIndex = 0;

    public Button leftButton;
    public Button rightButton;
    public Button actionButton;
    public TextMeshProUGUI actionButtonText;
    public TextMeshProUGUI priceText;
    

    private void Start()
    {
        selectedCarIndex = PlayerPrefs.GetInt("SelectedCar", 0);
        UpdateUI();
        
        PlayerPrefs.SetInt("Money", 5000);

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
            PlayerPrefs.SetInt("SelectedCar", selectedCarIndex);
        }
        else
        {
            int money = PlayerPrefs.GetInt("Money", 0);
            if (money >= carPrices[selectedCarIndex])
            {
                money -= carPrices[selectedCarIndex];
                PlayerPrefs.SetInt("Money", money);
                PlayerPrefs.SetInt("CarOwned_" + selectedCarIndex, 1);
            }
        }
        UpdateUI();
    }

    private bool IsCarOwned(int index)
    {
        return index == 0 || PlayerPrefs.GetInt("CarOwned_" + index, 0) == 1;
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
