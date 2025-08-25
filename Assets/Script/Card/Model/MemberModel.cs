using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メンバーモデル
/// </summary>
public class MemberModel : CardModel
{
    /// <summary>
    /// HP
    /// </summary>
    public int hp;

    /// <summary>
    /// 残りHP
    /// </summary>
    public int restHP;

    /// <summary>
    /// エネルギー数
    /// </summary>
    public int energyCount;

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
    public SpecialConditions specialConditions = SpecialConditions.NONE;

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
    /// 進化定義
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

    public MemberModel(int cardId)
    {
        Debug.Log(cardId);
        MemberEntity cardEntity = Resources.Load<MemberEntity>("EntityList/Member/Card" + cardId);
        id = cardEntity.id;
        name = cardEntity.name;
        cardType = cardEntity.cardType;
        cardImage = cardEntity.cardImage;
        background = cardEntity.background;
        backside = cardEntity.backside;
        typeFrame = cardEntity.typeFrame;
        evolutionaryOrder = cardEntity.evolutionaryOrder;

        hp = cardEntity.hp;
        restHP = cardEntity.hp;
        clubType = cardEntity.clubType;
        attack1 = cardEntity.attack1;
        attack2 = cardEntity.attack2;
        ability = cardEntity.ability;
        dropEnergy = cardEntity.dropEnergy;
        dropEnergyImage = cardEntity.dropEnergyImage;
        weakness = cardEntity.weakness;
        belongings = cardEntity.belongings;
        memberType = cardEntity.memberType;
    }

    public MemberModel(MemberModel model)
    {
        MemberEntity cardEntity = Resources.Load<MemberEntity>("EntityList/Member/Card" + model.id);
        id = model.id;
        name = model.name;
        cardType = model.cardType;
        cardImage = cardEntity.cardImage;
        background = cardEntity.background;
        backside = cardEntity.backside;
        typeFrame = cardEntity.typeFrame;
        evolutionaryOrder = cardEntity.evolutionaryOrder;

        hp = model.hp;
        restHP = model.restHP;
        clubType = model.clubType;
        attack1 = cardEntity.attack1;
        attack2 = cardEntity.attack2;
        ability = cardEntity.ability;
        dropEnergy = model.dropEnergy;
        energyCount = model.energyCount;
        dropEnergyImage = cardEntity.dropEnergyImage;
        weakness = model.weakness;
        belongings = model.belongings;
        memberType = model.memberType;
    }
}
