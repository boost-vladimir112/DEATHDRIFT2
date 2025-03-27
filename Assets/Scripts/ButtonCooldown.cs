using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCooldown : MonoBehaviour
{
    private Button button;
    private float cooldownTime = 300f; // 5 минут
    private string cooldownKey = "RewardCooldown";

    private void Start()
    {
        button = GetComponent<Button>();

        if (button == null)
        {
            Debug.LogError("ButtonCooldown: Кнопка не найдена на объекте!");
            return;
        }

        CheckCooldown();
        InvokeRepeating(nameof(CheckCooldown), 1f, 5f);
    }

    public void ActivateCooldown()
    {
        PlayerPrefs.SetString(cooldownKey, DateTime.UtcNow.ToString());
        PlayerPrefs.Save();
        CheckCooldown();
    }

    private void CheckCooldown()
    {
        if (!PlayerPrefs.HasKey(cooldownKey)) return;

        DateTime lastUseTime = DateTime.Parse(PlayerPrefs.GetString(cooldownKey));
        TimeSpan timePassed = DateTime.UtcNow - lastUseTime;

        bool isCooldown = timePassed.TotalSeconds < cooldownTime;
        button.gameObject.SetActive(!isCooldown);
    }
}
