using TMPro;
using UnityEngine;
using YG;

public class PurchaseHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI balanceText;
    private void OnEnable()
    {
        YG2.onPurchaseSuccess += SuccessPurchased;
        YG2.onPurchaseFailed += FailedPurchased;
    }

    private void OnDisable()
    {
        YG2.onPurchaseSuccess -= SuccessPurchased;
        YG2.onPurchaseFailed -= FailedPurchased;
    }

    private void SuccessPurchased(string id)
    {
        int currentCoins = YG2.saves.money2;
        Debug.Log("вот стока денег в облаке:" + currentCoins);

        if (id == "1")
        {
            currentCoins += 2500;
            YG2.saves.money2 = currentCoins;
            YG2.SaveProgress();
            balanceText.text = currentCoins.ToString();

        }
        else if (id == "2")
        {
            currentCoins += 5200;
            YG2.saves.money2 = currentCoins;
            YG2.SaveProgress();
            balanceText.text = currentCoins.ToString();
        }

        Debug.Log("Покупка завершена: " + id);
    }

    private void FailedPurchased(string id)
    {
        Debug.Log("Покупка не удалась: " + id);
    }
}
