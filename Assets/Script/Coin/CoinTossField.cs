using UnityEngine;

public class CoinTossField : MonoBehaviour
{
    [SerializeField] CoinController coinPrefab;
    [SerializeField] public Transform coinField;
    CoinController coin = null;

    void Start()
    {
        coin = Instantiate(coinPrefab, coinField);
        coin.ChangeFrontAndBack(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void OnClick_CloseButton()
    {
        Hide();
    }
}
