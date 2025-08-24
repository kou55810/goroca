using UnityEngine;
using UnityEngine.UI;

public class TrainersController : AbstractCardController
{
    private void Awake()
    {
        view = GetComponent<TrainersView>();
        movement = GetComponent<CardMovement>();
    }

    /// <summary>
    /// カード生成処理
    /// </summary>
    /// <param name="cardID">Card ID</param>
    public void Init(int cardID)
    {
        model = new TrainersModel(cardID); // カードデータを生成
        (view as TrainersView).Show(model as TrainersModel); // 表示
    }
}
