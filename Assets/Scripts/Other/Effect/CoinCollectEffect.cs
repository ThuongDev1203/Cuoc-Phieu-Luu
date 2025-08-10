using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class CoinCollectEffect : MonoBehaviour
{
    [Header("References")]
    public RectTransform uiCoinTarget;  // Vị trí icon coin trên UI
    public TMP_Text coinText;           // Text hiển thị số coin
    public GameObject coinPrefab;       // Prefab coin nhỏ để bay

    [Header("Settings")]
    public float flyDuration = 0.6f;    // Thời gian coin bay
    public int coinPerCollect = 1;      // Coin tăng mỗi lần bay
    public Ease flyEase = Ease.OutQuad; // Kiểu chuyển động

    private int currentCoin = 0;

    // Gọi khi nhặt coin trong game
    public void CollectCoin(Vector3 worldPosition)
    {
        // Tạo coin bay
        GameObject coin = Instantiate(coinPrefab, uiCoinTarget.parent);
        RectTransform coinRect = coin.GetComponent<RectTransform>();

        // Đổi vị trí world sang canvas
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiCoinTarget.parent as RectTransform,
            screenPos,
            null,
            out Vector2 localPos
        );
        coinRect.localPosition = localPos;

        // Coin bay đến icon UI
        coinRect.DOMove(uiCoinTarget.position, flyDuration)
            .SetEase(flyEase)
            .OnComplete(() =>
            {
                Destroy(coin);
                AnimateCoinText(currentCoin, currentCoin + coinPerCollect, 0.2f);
                currentCoin += coinPerCollect;
            });
    }

    // Hiệu ứng tăng số coin dần
    private void AnimateCoinText(int from, int to, float duration)
    {
        DOTween.To(() => from, x =>
        {
            from = x;
            coinText.text = from.ToString();
        }, to, duration).SetEase(Ease.Linear);
    }
}
