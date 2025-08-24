using UnityEngine;

/// <summary>
/// ダメカンEntityクラス
/// </summary>
[CreateAssetMenu(fileName = "CardEntity", menuName = "Create DamageCounterEntity")]
public class DamageCounterEntity: ScriptableObject
{
    /// <summary>
    /// ダメージ数
    /// </summary>
    public int damage;
    /// <summary>
    /// ダメカンのアイコン
    /// </summary>
    [SerializeField] public Sprite icon;
}
