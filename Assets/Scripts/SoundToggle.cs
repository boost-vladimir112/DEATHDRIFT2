using UnityEngine;
using UnityEngine.UI;
using YG;

public class SoundToggle : MonoBehaviour
{
    public Toggle soundToggle;

    void Start()
    {
        // Загружаем состояние звука из сохранений
        bool isSoundEnabled = YG2.saves.soundEnabled;
        soundToggle.isOn = isSoundEnabled;
        SetSound(isSoundEnabled);

        // Добавляем слушатель
        soundToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    void OnToggleValueChanged(bool isOn)
    {
        SetSound(isOn);

        // Сохраняем состояние звука через YG2
        YG2.saves.soundEnabled = isOn;
        YG2.SaveProgress();
    }

    void SetSound(bool isEnabled)
    {
        AudioListener.pause = !isEnabled;
    }

    public static bool IsSoundEnabled()
    {
        return !AudioListener.pause;
    }
}
