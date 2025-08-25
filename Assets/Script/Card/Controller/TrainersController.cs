using System;
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
    public void Init(int cardID, Guid guid = default(Guid))
    {
        if (guid.Equals(Guid.Empty))
        {
            this.guid = Guid.NewGuid();
        }
        else
        {
            this.guid = guid;
        }
        model = new TrainersModel(cardID); // カードデータを生成
        (view as TrainersView).Show(model as TrainersModel); // 表示
    }
}
