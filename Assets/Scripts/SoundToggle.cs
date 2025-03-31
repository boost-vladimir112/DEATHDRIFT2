using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    // Ссылка на Toggle
    public Toggle soundToggle;

    private const string SoundPrefKey = "SoundEnabled";

    void Start()
    {
        // Проверяем, существует ли ключ в PlayerPrefs
        if (!PlayerPrefs.HasKey(SoundPrefKey))
        {
            PlayerPrefs.SetInt(SoundPrefKey, 1); // По умолчанию включаем звук
            PlayerPrefs.Save();
        }

        // Загружаем состояние звука из PlayerPrefs
        bool isSoundEnabled = PlayerPrefs.GetInt(SoundPrefKey) == 1;

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
        AudioListener.pause = !isEnabled;
    }

    // Статический метод для получения состояния звука
    public static bool IsSoundEnabled()
    {
        return !AudioListener.pause;
    }
}
