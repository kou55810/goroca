using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メンバーモデル
/// </summary>
public class MemberModel: CardModel
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
    public string attack1;

    /// <summary>
    /// 攻撃2
    /// </summary>
    public string attack2;

    /// <summary>
    /// 特性
    /// </summary>
    public string ability;

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
    public CardEntity belongings;

    /// <summary>
    /// メンバーの種類
    /// </summary>
    public MemberType memberType;
    public MemberModel(int cardId)
    {
        MemberEntity cardEntity = Resources.Load<MemberEntity>("EntityList/Member/Card" + cardId);
        id = cardEntity.id;
        name = cardEntity.name;
        cardType = cardEntity.cardType;
        background = cardEntity.background;
        backside = cardEntity.backside;
        typeFrame = cardEntity.typeFrame;

        hp = cardEntity.hp;
        clubType = cardEntity.clubType;
        attack1 = cardEntity.attack1;
        attack2 = cardEntity.attack2;
        ability = cardEntity.ability;
        dropEnergy = cardEntity.dropEnergy;
        weakness = cardEntity.weakness;
        belongings = cardEntity.belongings;
        memberType = cardEntity.memberType;
    }
}
