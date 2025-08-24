using System.ComponentModel;
using UnityEngine;

/// <summary>
/// トレーナーズの種類
/// </summary>
public enum TrainersType
{
    /// <summary>
    /// 道具
    /// </summary>
    [Description("どうぐ")]
    TOOL,

    /// <summary>
    /// 持ち物
    /// </summary>
    [Description("もちもの")]
    BELONGINGS,

    /// <summary>
    /// イベント
    /// </summary>
    [Description("イベント")]
    EVENT,
}
