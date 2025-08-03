using UnityEngine;

public class MemberController: AbstractCardController
{
    /// <summary>
    /// カードの見た目の処理
    /// </summary>
    public new MemberView view;

    /// <summary>
    /// カードのデータを処理
    /// </summary>
    public new MemberModel model;

    private void Awake()
    {
        view = GetComponent<MemberView>();
    }

    /// <summary>
    /// カード生成処理
    /// </summary>
    /// <param name="cardID">Card ID</param>
    public void Init(int cardID)
    {
        model = new MemberModel(cardID); // カードデータを生成
        view.Show(model); // 表示
    }
}
