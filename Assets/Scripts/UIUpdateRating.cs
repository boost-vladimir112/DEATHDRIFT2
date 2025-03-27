
using TMPro;
using UnityEngine;
using YG;

public class UIUpdateRating : MonoBehaviour
{
    private int rating;
    [SerializeField] private TextMeshProUGUI ratingTextCounter;
    void Start()
    {
        UpdateText();
    }
    public void UpdateText()
    {
        rating = YG2.saves.playerRating;
        ratingTextCounter.text = rating.ToString();
    }
}
