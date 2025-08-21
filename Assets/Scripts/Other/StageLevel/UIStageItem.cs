using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Manager;
using UIs;

public class UIStageItem : MonoBehaviour
{
    public TextMeshProUGUI textLevel;
    public GameObject lockIcon;
    public GameObject unLockIcon;
    public GameObject highlight;
    public Button button;

    private int level;

    public void Setup(int level, bool isUnlocked, bool isCurrent)
    {
        this.level = level;

        // Hiện số level
        textLevel.text = level.ToString();

        // Lock / Unlock
        lockIcon.SetActive(!isUnlocked);
        unLockIcon.SetActive(isUnlocked);
        textLevel.gameObject.SetActive(isUnlocked);

        // Highlight nếu là level hiện tại
        highlight.SetActive(isUnlocked && isCurrent);

        // Button
        button.interactable = isUnlocked;
        button.onClick.RemoveAllListeners();
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
