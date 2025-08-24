using UnityEngine;
using UnityEngine.UI;

public class CoinView : MonoBehaviour
{
    /// <summary>
    /// コインの表面
    /// </summary>
    [SerializeField] public Image front;

    /// <summary>
    /// コインの裏面
    /// </summary>
    [SerializeField] public Image back;
    
    public void Show() // cardModelのデータ取得と反映
    {
        front.sprite = Resources.Load<Sprite>($"Images/Coin/coin_front");
        back.sprite = Resources.Load<Sprite>($"Images/Coin/coin_back");
    }
}
