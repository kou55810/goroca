using Photon.Pun;
using UnityEngine;

public class CoinTossField : MonoBehaviourPun
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
        if (PhotonNetwork.LocalPlayer.UserId.Equals(GameManager.instance.turnPlayerId.ToString()))
        {
            GameManager.instance.Hide_Coin();
        }
    }
    
    public void GoCoinToss(bool coinResult)
    {
        if (coin != null) {
            coin.CoinToss(coinResult);
        }
    }
}
