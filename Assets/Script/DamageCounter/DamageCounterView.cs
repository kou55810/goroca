using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ダメカンViewクラス
/// </summary>
public class DamageCounterView : MonoBehaviour
{
    /// <summary>
    /// ダメカンアイコン
    /// </summary>
    [SerializeField] public Image icon;

    public void Show(DamageCounterModel damageCounterModel) // cardModelのデータ取得と反映
    {
        icon.sprite = damageCounterModel.icon;
    }
}
