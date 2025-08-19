using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Manager;
using UIs;

public class UIStageItem : MonoBehaviour
{
    public TextMeshProUGUI textLevel;
    public Image[] stars;
    public GameObject lockIcon;
    public GameObject highlight;
    public Button button;

    private int level;

    public void Setup(int level, int starCount, bool isUnlocked, bool isCurrent)
    {
        this.level = level;

        // Hiện số level
        textLevel.text = level.ToString();

        // Hiện sao nếu mở
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].enabled = i < starCount;
        }

        // Lock
        lockIcon.SetActive(!isUnlocked);
        textLevel.gameObject.SetActive(isUnlocked);
        foreach (var star in stars)
        {
            star.gameObject.SetActive(isUnlocked);
        }

        // Highlight nếu là level hiện tại
        highlight.SetActive(isCurrent);

        // Button
        button.interactable = isUnlocked;
        button.onClick.RemoveAllListeners(); // tránh add nhiều lần
        if (isUnlocked)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    private void OnClick()
    {
        Debug.Log($"Click level {level}");

        SoundManager.Instance.PlayClickSound();

        GameManager.Instance.SaveCurrentLevel(level);

        // Ẩn UI chọn level trước khi vào game
        GameManager.Instance.uiManager.ShowPanel(GameManager.Instance.uiManager.uiGame);

        // Load game
        GameManager.Instance.LoadGame();
    }

}
