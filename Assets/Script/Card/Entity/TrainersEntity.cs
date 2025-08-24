using UnityEngine;

/// <summary>
/// トレーナーズEntity
/// </summary>
[CreateAssetMenu(fileName = "TrainersEntity", menuName = "Create TrainersEntity")]
public class TrainersEntity: CardEntity
{
    /// <summary>
    /// 効果
    /// </summary>
    public string effect;

    /// <summary>
    /// トレーナーズの種類
    /// </summary>
    public TrainersType trainersType;
}
