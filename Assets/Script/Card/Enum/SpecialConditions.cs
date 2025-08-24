using System.ComponentModel;
using UnityEngine;

/// <summary>
/// 状態異常
/// </summary>
public enum SpecialConditions
{
    /// <summary>
    /// 毒
    /// </summary>
    [Description("poision")]
    POISION,

    /// <summary>
    /// ねむり
    /// </summary>
    [Description("sleep")]
    SLEEP,

    /// <summary>
    /// 毒眠り
    /// </summary>
    [Description("sleep_poision")]
    POISION_AND_SLEEP,

    /// <summary>
    /// なし
    /// </summary>
    NONE
}
