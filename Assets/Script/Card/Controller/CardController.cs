using UnityEngine;

public class CardController : MonoBehaviour
{
    /// <summary>
    /// カードビュー
    /// </summary>
    public CardView view; // カードの見た目の処理

    /// <summary>
    /// カードのモデル
    /// </summary>
    public CardModel model; // カードのデータを処理

    private void Awake()
    {
        view = GetComponent<CardView>();
    }
    
    /// <summary>
    /// カードの生成処理
    /// </summary>
    /// <param name="cardID">Card ID</param>
    public void Init(int cardID) // カードを生成した時に呼ばれる関数
    {
        model = new CardModel(cardID); // カードデータを生成
        view.Show(model); // 表示
    }
}
