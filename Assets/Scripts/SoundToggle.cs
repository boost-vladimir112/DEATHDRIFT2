using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    // ������ �� Toggle
    public Toggle soundToggle;

    private const string SoundPrefKey = "SoundEnabled";

    void Start()
    {
        // ���������, ���������� �� ���� � PlayerPrefs
        if (!PlayerPrefs.HasKey(SoundPrefKey))
        {
            PlayerPrefs.SetInt(SoundPrefKey, 1); // �� ��������� �������� ����
            PlayerPrefs.Save();
        }

        // ��������� ��������� ����� �� PlayerPrefs
        bool isSoundEnabled = PlayerPrefs.GetInt(SoundPrefKey) == 1;

        // ������������� ��������� Toggle � ����������� �� ������������ ��������
        soundToggle.isOn = isSoundEnabled;

        // �������� ��� ��������� ���� � ����������� �� ���������
        SetSound(isSoundEnabled);

        // ��������� ��������� �� ��������� ��������� Toggle
        soundToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // �����, ������� ����� ���������� ��� ��������� ��������� Toggle
    void OnToggleValueChanged(bool isOn)
    {
        SetSound(isOn);

        // ��������� ��������� ����� � PlayerPrefs
        PlayerPrefs.SetInt(SoundPrefKey, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    // ����� ��� ���������/���������� �����
    void SetSound(bool isEnabled)
    {
        AudioListener.pause = !isEnabled;
    }

    // ����������� ����� ��� ��������� ��������� �����
    public static bool IsSoundEnabled()
    {
        return !AudioListener.pause;
    }
}
