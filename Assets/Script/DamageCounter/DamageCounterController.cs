using UnityEngine;

/// <summary>
/// ダメカンControllerクラス
/// </summary>
public class DamageCounterController : MonoBehaviour
{
    /// <summary>
    /// ダメカンビュー
    /// </summary>
    public DamageCounterView view;

    /// <summary>
    /// ダメカンのモデル
    /// </summary>
    public DamageCounterModel model;

    public DamageCounterMovement movement;

    private void Awake()
    {
        view = GetComponent<DamageCounterView>();
        movement = GetComponent<DamageCounterMovement>();
    }

    /// <summary>
    /// ダメカン生成処理
    /// </summary>
    /// <param name="cardID">Card ID</param>
    public void Init(int damage)
    {
        model = new DamageCounterModel(damage);
        view.Show(model); // 表示
    }
}
