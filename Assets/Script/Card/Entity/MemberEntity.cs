using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メンバーEntity
/// </summary>
[CreateAssetMenu(fileName = "MemberEntity", menuName = "Create MemberEntity")]
public class MemberEntity : CardEntity
{
    /// <summary>
    /// HP
    /// </summary>
    public int hp;

    /// <summary>
    /// 部活動
    /// </summary>
    public ClubType clubType;

    /// <summary>
    /// 攻撃1
    /// </summary>
    public AttackEntity attack1;

    /// <summary>
    /// 攻撃2
    /// </summary>
    public AttackEntity attack2;

    /// <summary>
    /// 特性
    /// </summary>
    public AbilityEntity ability;

    /// <summary>
    /// 状態異常
    /// </summary>
    public List<SpecialConditions> specialConditions = new List<SpecialConditions>();

    /// <summary>
    /// 落ちエネ
    /// </summary>
    public int dropEnergy;

    /// <summary>
    /// 弱点
    /// </summary>
    public ClubType weakness;

    /// <summary>
    /// 道具
    /// </summary>
    public int belongings;

    /// <summary>
    /// メンバーの種類
    /// </summary>
    public MemberType memberType;

    /// <summary>
    /// メンバーの進化順番
    /// </summary>
    public EvolutionaryOrderEntity evolutionaryOrder;

    /// <summary>
    /// 落ちエネ
    /// </summary>
    public Sprite dropEnergyImage;

    /// <summary>
    /// 道具
    /// </summary>
    public Sprite belongingsImage;

    /// <summary>
    /// 状態異常イメージ
    /// </summary>
    public Sprite specialConditionsImage;

    /// <summary>
    /// EXフラグ
    /// </summary>
    public bool isEX = false;
}
