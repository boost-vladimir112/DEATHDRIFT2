using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class GetBonusMoney : MonoBehaviour
{
    [SerializeField] GameObject bonusPanel;
    [SerializeField] GameObject hidenTextTG;
    private int balance;
    private int bonusChecker;
    
    [SerializeField] private TextMeshProUGUI balanceText;

    private void Start()
    {
        bonusChecker = YG2.GetState("tgbonus");
        balance = YG2.GetState("money");
        if(bonusChecker > 0)
        {
            hidenTextTG.SetActive(false);
        }
    }

    public void OpenChannel()
    {
        Application.OpenURL("https://t.me/tsukuyomi05");
        if(bonusChecker == 0 )
        {
            balance += 800;
            balanceText.text = balance.ToString();
            YG2.SetState("money", balance);
            bonusChecker++;
            hidenTextTG.SetActive(false);
            YG2.SetState("tgbonus", bonusChecker);
            YG2.SaveProgress();
        }
        
    }
}
