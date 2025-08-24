using System;
using UnityEngine;

/// <summary>
/// ダメカンModelクラス
/// </summary>
public class DamageCounterModel
{
    /// <summary>
    /// ダメージ数
    /// </summary>
    public int damage;
    /// <summary>
    /// ダメカンのアイコン
    /// </summary>
    public Sprite icon;

    public DamageCounterModel(int damage)
    {
        string str = damage > 0 ? "damage" : "heal";
        this.damage = damage;
        DamageCounterEntity entity = Resources.Load<DamageCounterEntity>($"EntityList/DamageCounter/{str}_{Math.Abs(damage)}");
        icon = entity.icon;
    }
}
