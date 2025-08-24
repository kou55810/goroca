using UnityEngine;

/// <summary>
/// 攻撃定義クラス
/// </summary>
[CreateAssetMenu(fileName = "AttackEntity", menuName = "Create AttackEntity")]
public class AttackEntity: ScriptableObject
{
    public int energy;
    public int damage;
    public string attackName;
    public string attackEffect;
}
