using UnityEngine;

public class RoomPanelContoroller: MonoBehaviour
{
    /// <summary>
    /// ルームパネルのビュー
    /// </summary>
    public RoomPanelView view;

    private void Awake()
    {
        view = GetComponent<RoomPanelView>();
    }

    /// <summary>
    /// パネルの生成処理
    /// </summary>
    /// <param name="name">部屋名</param>
    public void Init(string name) // カードを生成した時に呼ばれる関数
    {
        view.Show(name); // 表示
    }
}
