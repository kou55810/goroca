using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberController : AbstractCardController
{
    private void Awake()
    {
        view = GetComponent<MemberView>();
        movement = GetComponent<CardMovement>();
    }

    /// <summary>
    /// カード生成処理
    /// </summary>
    /// <param name="cardID">Card ID</param>
    public void Init(int cardID, Guid guid = default(Guid))
    {
        if (guid.Equals(Guid.Empty))
        {
            this.guid = Guid.NewGuid();
        }
        else
        {
            this.guid = guid;
        }
        model = new MemberModel(cardID); // カードデータを生成
        (view as MemberView).Show(model as MemberModel); // 表示
    }

    /// <summary>
    /// カード生成処理(modelから引き継ぎ)
    /// </summary>
    /// <param name="member"></param>
    public void Init(MemberModel member, Guid guid = default(Guid))
    {
        if (guid.Equals(Guid.Empty))
        {
            this.guid = Guid.NewGuid();
        }
        else
        {
            this.guid = guid;
        }
        model = new MemberModel(member);
        (view as MemberView).Show(model as MemberModel); // 表示
    }

    /// <summary>
    /// 進化情報の取得
    /// </summary>
    /// <returns></returns>
    public EvolutionaryOrderEntity GetEvolutionData()
    {
        return (model as MemberModel).evolutionaryOrder;
    }

    /// <summary>
    /// ダメージの加算・減算を行う
    /// </summary>
    /// <param name="damage"></param>
    public void DamageCount(int damage)
    {
        int restHP = (this.model as MemberModel).restHP - damage;
        if (restHP > (this.model as MemberModel).hp)
        {
            restHP = (this.model as MemberModel).hp;
        }
        else if (0 > restHP)
        {
            restHP = 0;
        }
        (this.model as MemberModel).restHP = restHP;
        (view as MemberView).Show(model as MemberModel);
    }

    /// <summary>
    /// 道具を表示
    /// </summary>
    public void ShowBelongings()
    {
        if ((this.model as MemberModel).belongings != 0)
        {
            GameManager.instance.ShowCardView((this.model as MemberModel).belongings);
        }
    }

    /// <summary>
    /// 道具をセットする
    /// </summary>
    /// <param name="belongingsNumber"></param>
    public void SetBelongings(int belongingsNumber)
    {
        (this.model as MemberModel).belongings = belongingsNumber;
        (view as MemberView).SetBelongingsImage(belongingsNumber);
        Transform belongings = this.transform.Find("belongings");
        // 伝説の装備ならHP+20
        if (belongingsNumber == 49)
        {
            (this.model as MemberModel).hp += 20;
            DamageCount(-20);
        }
    }

    /// <summary>
    /// 道具を無効化
    /// </summary>
    public void DisableBelongings()
    {
        if ((this.model as MemberModel).belongings == 49)
        {
            (this.model as MemberModel).hp -= 20;
            DamageCount(20);
        }
        (view as MemberView).DisableBelongings();
    }

    /// <summary>
    /// エネルギーの加算を行う
    /// </summary>
    /// <param name="addCount"></param>
    public void AddEnergy(int addCount)
    {
        showEnergy();
        (this.model as MemberModel).energyCount += addCount;
        (view as MemberView).Show(model as MemberModel);
    }

    /// <summary>
    /// エネルギーの減算を行う
    /// </summary>
    /// <param name="minusCount"></param>
    public void MinusEnergy(int minusCount)
    {
        (this.model as MemberModel).energyCount -= minusCount;
        if ((this.model as MemberModel).energyCount <= 0)
        {
            (this.model as MemberModel).energyCount = 0;
            hideEnergy();
        }
        (view as MemberView).Show(model as MemberModel);
    }

    /// <summary>
    /// 状態異常をセットする
    /// </summary>
    /// <param name="specialConditions"></param>
    public void AddSpecialConditions(SpecialConditions specialConditions)
    {
        switch ((this.model as MemberModel).specialConditions)
        {
            case SpecialConditions.POISION:
                if (specialConditions.Equals(SpecialConditions.POISION) || specialConditions.Equals(SpecialConditions.NONE))
                {
                    (this.model as MemberModel).specialConditions = SpecialConditions.POISION;
                    (view as MemberView).SetSpecialConditions(SpecialConditions.POISION);
                }
                else if (specialConditions.Equals(SpecialConditions.SLEEP) || specialConditions.Equals(SpecialConditions.POISION_AND_SLEEP))
                {
                    (this.model as MemberModel).specialConditions = SpecialConditions.POISION_AND_SLEEP;
                    (view as MemberView).SetSpecialConditions(SpecialConditions.POISION_AND_SLEEP);
                }
                break;
            case SpecialConditions.SLEEP:
                if (specialConditions.Equals(SpecialConditions.SLEEP) || specialConditions.Equals(SpecialConditions.NONE))
                {
                    (this.model as MemberModel).specialConditions = SpecialConditions.SLEEP;
                    (view as MemberView).SetSpecialConditions(SpecialConditions.SLEEP);
                }
                else if (specialConditions.Equals(SpecialConditions.POISION) || specialConditions.Equals(SpecialConditions.POISION_AND_SLEEP))
                {
                    (this.model as MemberModel).specialConditions = SpecialConditions.POISION_AND_SLEEP;
                    (view as MemberView).SetSpecialConditions(SpecialConditions.POISION_AND_SLEEP);
                }
                break;
            case SpecialConditions.NONE:
                (this.model as MemberModel).specialConditions = specialConditions;
                (view as MemberView).SetSpecialConditions(specialConditions);
                break;
            case SpecialConditions.POISION_AND_SLEEP:
                break;
        }
        
    }

    public void OnClick_SpecialConditions()
    {
        if (this.transform.parent.name.Contains("Enemy")) {
            return;
        }
        ResetSpecialConditions();
        GameManager.instance.RemoveSpecialConditions(guid);
    }

    /// <summary>
    /// 状態異常をセットする
    /// </summary>
    /// <param name="specialConditions"></param>
    public void ResetSpecialConditions()
    {
        (this.model as MemberModel).specialConditions = SpecialConditions.NONE;
        (this.view as MemberView).DisableSpecialConditions();
    }

    public void showEnergy()
    {
        Transform energy = this.transform.Find("Energy");
        energy.GetComponent<CanvasGroup>().alpha = 100;
    }
    public void hideEnergy()
    {
        Transform energy = this.transform.Find("Energy");
        energy.GetComponent<CanvasGroup>().alpha = 0;
    }
}
