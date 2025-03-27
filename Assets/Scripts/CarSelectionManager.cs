using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public class CarSelectionManager : MonoBehaviour
{
    public GameObject[] cars;
    public int[] carPrices;
    private int selectedCarIndex = 0;

    public Button leftButton;
    public Button rightButton;
    public Button actionButton;
    public Button tgSubscribeButton; // Кнопка подписки
    public GameObject selectText;
    public TextMeshProUGUI priceText;
    public GameObject pricePanel;
    public TextMeshProUGUI balanceText;

    public int balance;
    public int carFromTGIndex = 3; // Индекс машины за подписку
    public UIUpdateRating uiUpdateRating = null;

    private int playerRating;

    private void Start()
    {
        playerRating = YG2.saves.playerRating;
        if (YG2.saves.carOwned.Count == 0)
        {
            YG2.saves.carOwned.Add(0);
            YG2.SaveProgress();
        }

        selectedCarIndex = YG2.saves.SelectedCar;
        balance = YG2.saves.money2;
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

    public void UpdateBalance()
    {
        balance = YG2.saves.money2;
        balanceText.text = balance.ToString();
    }

    public void OnActionButtonClick()
    {
        if (IsCarOwned(selectedCarIndex) || IsCarFromTG(selectedCarIndex))
        {
            YG2.saves.SelectedCar = selectedCarIndex;
        }
        else
        {
            if (balance >= carPrices[selectedCarIndex])
            {
                balance -= carPrices[selectedCarIndex];
                balanceText.text = balance.ToString();

                playerRating += 10+selectedCarIndex;
                YG2.saves.playerRating = playerRating;
                YG2.SetLeaderboard("rating", playerRating);
                uiUpdateRating.UpdateText();

                YG2.saves.money2 = balance;
                YG2.saves.carOwned.Add(selectedCarIndex);
                YG2.saves.SelectedCar = selectedCarIndex;
            }
        }
        YG2.SaveProgress();
        UpdateUI();
    }

    private bool IsCarOwned(int index)
    {
        return YG2.saves.carOwned.Contains(index);
    }

    private bool IsCarFromTG(int index)
    {
        return index == carFromTGIndex && YG2.saves.carFromTG;
    }

    private void UpdateUI()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(i == selectedCarIndex);
        }

        if (IsCarOwned(selectedCarIndex) || IsCarFromTG(selectedCarIndex))
        {
            priceText.text = "";
            pricePanel.SetActive(false);
            actionButton.gameObject.SetActive(selectedCarIndex != YG2.saves.SelectedCar);
            selectText.SetActive(selectedCarIndex != YG2.saves.SelectedCar);
            tgSubscribeButton.gameObject.SetActive(false); // Прячем кнопку подписки
        }
        else
        {
            pricePanel.SetActive(true);
            actionButton.gameObject.SetActive(true);
            selectText.SetActive(false);
            priceText.text = carPrices[selectedCarIndex].ToString();

            // Если выбрана машина за Telegram, показываем кнопку подписки
            tgSubscribeButton.gameObject.SetActive(selectedCarIndex == carFromTGIndex);
        }
    }

    public void UnlockCarFromTG()
    {
        YG2.saves.carFromTG = true;
        YG2.SaveProgress();
        UpdateUI();
    }

    public void OpenTelegramLink()
    {
        Application.OpenURL("https://t.me/tsukuyomi05"); // Ссылка на ваш канал
    }
}
