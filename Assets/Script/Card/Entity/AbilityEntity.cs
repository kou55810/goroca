using UnityEngine;

/// <summary>
/// 特性クラス
/// </summary>
[CreateAssetMenu(fileName = "AbilityEntity", menuName = "Create AbilityEntity")]
public class AbilityEntity: ScriptableObject
{
    public bool isUsed;
    public string abilityName;
    public string abilityEffect;
}
