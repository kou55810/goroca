using System.ComponentModel;
using UnityEngine;

/// <summary>
/// メンバーの種類
/// </summary>
public enum MemberType
{
    /// <summary>
    /// 新人
    /// </summary>
    [Description("新人")]
    NEW_FACE,

    /// <summary>
    /// 常連
    /// </summary>
    [Description("常連")]
    REGULAR,

    /// <summary>
    /// 古参
    /// </summary>
    [Description("古参")]
    VETERAN
}
