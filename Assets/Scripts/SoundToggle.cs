using UnityEngine;
using UnityEngine.UI;
using YG;

public class SoundToggle : MonoBehaviour
{
    public Toggle soundToggle;

    void Start()
    {
        // ��������� ��������� ����� �� ����������
        bool isSoundEnabled = YG2.saves.soundEnabled;
        soundToggle.isOn = isSoundEnabled;
        SetSound(isSoundEnabled);

        // ��������� ���������
        soundToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }
    public static void SetSound(bool isEnabled)
    {
        AudioListener.pause = !isEnabled;
    }
    void OnToggleValueChanged(bool isOn)
    {
        SetSound(isOn);

        // ��������� ��������� ����� ����� YG2
        YG2.saves.soundEnabled = isOn;
        YG2.SaveProgress();
    }

   

    public static bool IsSoundEnabled()
    {
        return !AudioListener.pause;
    }
}
