using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    // Ссылка на Toggle
    public Toggle soundToggle;

    private const string SoundPrefKey = "SoundEnabled";

    void Start()
    {
        // Загружаем состояние звука из PlayerPrefs
        bool isSoundEnabled = PlayerPrefs.GetInt(SoundPrefKey, 1) == 1;

        // Устанавливаем состояние Toggle в зависимости от сохраненного значения
        soundToggle.isOn = isSoundEnabled;

        // Включаем или выключаем звук в зависимости от состояния
        SetSound(isSoundEnabled);

        // Добавляем слушатель на изменение состояния Toggle
        soundToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // Метод, который будет вызываться при изменении состояния Toggle
    void OnToggleValueChanged(bool isOn)
    {
        SetSound(isOn);

        // Сохраняем состояние звука в PlayerPrefs
        PlayerPrefs.SetInt(SoundPrefKey, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Метод для включения/выключения звука
    void SetSound(bool isEnabled)
    {
        // Включаем/выключаем звук с помощью AudioListener.pause
        AudioListener.pause = !isEnabled;
    }

    // Статический метод для получения состояния звука
    public static bool IsSoundEnabled()
    {
        return !AudioListener.pause;  // Возвращаем, включен ли звук (false если звук выключен)
    }
}
