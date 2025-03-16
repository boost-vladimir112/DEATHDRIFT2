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
        selectedCarIndex = PlayerPrefs.GetInt("SelectedCar", 0);
        UpdateUI();

        YG2.SetState("money", 5000);
        balance = YG2.GetState("money");
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
            PlayerPrefs.SetInt("SelectedCar", selectedCarIndex);
        }
        else
        {
          
            if (balance >= carPrices[selectedCarIndex])
            {
                balance -= carPrices[selectedCarIndex];
                YG2.SetState("money", balance);
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
