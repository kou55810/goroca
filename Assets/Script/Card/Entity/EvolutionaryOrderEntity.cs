using UnityEngine;

/// <summary>
/// 進化の定義Entity
/// </summary>
[CreateAssetMenu(fileName = "EvolutionaryOrderEntity", menuName = "Create EvolutionaryOrderEntity")]
public class EvolutionaryOrderEntity : ScriptableObject
{
    /// <summary>
    /// メンバーの種類
    /// </summary>
    public MemberType memberType;

    /// <summary>
    /// 進化後のメンバーID
    /// </summary>
    public int afterMemberId;

    /// <summary>
    /// 進化前のメンバーID
    /// </summary>
    public int beforeMemberId;
}
