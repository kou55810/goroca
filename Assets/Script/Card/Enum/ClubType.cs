using System.ComponentModel;
using UnityEngine;

/// <summary>
/// 部活
/// </summary>
public enum ClubType
{
    [Description("アニメ")]
    ANIME,
    [Description("ゲーム")]
    GAME,
    [Description("ミス研")]
    MYSTERY,
    [Description("映画")]
    MOVIE,
    [Description("ダイエット")]
    DIET,
    [Description("酒乱")]
    ALCOHOL,
    [Description("ボドゲ")]
    BOARD_GAME,
    [Description("なし")]
    NONE
}
