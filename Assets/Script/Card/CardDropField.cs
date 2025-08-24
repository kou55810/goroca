using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// カードへのドロップ処理
/// </summary>
public class CardDropField : MonoBehaviour, IDropHandler
{
    /// <summary>
    /// ドロップ時の処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        MemberController baseCard = this.GetComponent<MemberController>();
        if (baseCard is null)
        {
            return;
        }
        if (baseCard.IsParentTrans("PlayerHand"))
        {
            return;
        }
        // ダメカンをドロップしたとき
        if (eventData.pointerDrag.GetComponent<DamageCounterController>() is not null)
        {
            DamageCounterController damecon = eventData.pointerDrag.GetComponent<DamageCounterController>(); // ドラッグしてきた情報からCardControllerを取得
            if (damecon.movement != null) // もしカードがあれば、
            {
                AbstractCardController card = this.GetComponent<AbstractCardController>();
                if (card is MemberController)
                {
                    DamageCount(card as MemberController, damecon.model.damage);
                }
            }
        }
        // 状態異常をドロップしたとき
        else if (eventData.pointerDrag.GetComponent<MarkerController>() is not null)
        {
            MarkerController marker = eventData.pointerDrag.GetComponent<MarkerController>();
            AbstractCardController card = this.GetComponent<AbstractCardController>();
            if (card is MemberController)
            {
                (card as MemberController).AddSpecialConditions(marker.specialConditions);
            }
        }
        // エネルギーをドロップしたとき
        else if (eventData.pointerDrag.GetComponent<EnergyController>() is not null)
        {
            AbstractCardController card = this.GetComponent<AbstractCardController>();
            if (card is MemberController)
            {
                AddEnergy(card as MemberController);
            }
        }
        else if (eventData.pointerDrag.GetComponent<MinusEnergyController>() is not null)
        {
            AbstractCardController card = this.GetComponent<AbstractCardController>();
            if (card is MemberController)
            {
                MinusEnergy(card as MemberController);
            }
        }
        // カードがドロップしたとき
        else if (eventData.pointerDrag.GetComponent<AbstractCardController>() is not null)
        {
            AbstractCardController putCard = eventData.pointerDrag.GetComponent<AbstractCardController>();
            if (putCard is MemberController)
            {
                MemberController putMember = putCard as MemberController;
                if (putMember.IsParentTrans("PlayerHand"))
                {
                    // 飴進化
                    if (GameManager.instance.usedCandy && (baseCard.GetEvolutionData().afterMemberId ==
                    putMember.GetEvolutionData().beforeMemberId))
                    {
                        Evolution(baseCard, putMember);
                        GameManager.instance.ChangeUsedCandy(false);
                    }
                    // 通常進化
                    else if (baseCard.GetEvolutionData().afterMemberId == putCard.model.id &&
                    putMember.GetEvolutionData().beforeMemberId == baseCard.model.id)
                    {
                        Evolution(baseCard, putMember);
                    }
                    else
                    {
                        return;
                    }
                }
                else if (putMember.IsParentTrans("PlayerBattleField") && baseCard.IsParentTrans("PlayerBenchField"))
                {
                    BattleMember(baseCard, putMember);
                }
            }
            else if (putCard is TrainersController)
            {
                // 持ち物の場合  
                if (((putCard as TrainersController).model as TrainersModel).trainersType.Equals(TrainersType.BELONGINGS))
                {
                    (baseCard as MemberController).SetBelongings(putCard.model.id);
                    Destroy(putCard.gameObject);
                }
            }
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// メンバーを進化させる
    /// </summary>
    /// <param name="baseCard"></param>
    /// <param name="putMember"></param>
    private async Task Evolution(MemberController baseCard, MemberController putMember)
    {
        Transform baseParent = baseCard.transform.parent.transform;
        CardViewDropField baseDropField = baseParent.GetComponent<CardViewDropField>();
        putMember.DamageCount((baseCard.model as MemberModel).hp - (baseCard.model as MemberModel).restHP);
        putMember.SetBelongings((baseCard.model as MemberModel).belongings);
        putMember.AddEnergy((baseCard.model as MemberModel).energyCount);
        Destroy(putMember.gameObject);
        await CardCreator.instance.CreateMemberCard(putMember, baseParent, baseDropField.isFront, baseDropField.isNormalPosition, baseDropField.isMovement, true, baseDropField.cardMagnification);

        // 進化前はトラッシュに送る
        GameObject playerOffline = GameObject.Find("PlayerOffline");
        CardViewDropField playerOfflineField = playerOffline.GetComponent<CardViewDropField>();
        CardCreator.instance.CreateCard(baseCard.model.id, playerOffline.transform, playerOfflineField.isFront, playerOfflineField.isNormalPosition, playerOfflineField.isMovement, false, playerOfflineField.cardMagnification);
        Destroy(baseCard.gameObject);
    }

    /// <summary>
    /// バトル場と入れ替え
    /// </summary>
    /// <param name="baseCard"></param>
    /// <param name="putMember"></param>
    private void BattleMember(MemberController baseCard, MemberController putMember)
    {
        Transform baseParent = baseCard.transform.parent.transform;
        CardViewDropField baseDropField = baseParent.GetComponent<CardViewDropField>();
        Transform putParent = putMember.movement.cardParent.transform;
        CardViewDropField putDropField = putParent.GetComponent<CardViewDropField>();
        CardCreator.instance.CreateMemberCard(putMember, baseParent, baseDropField.isFront, baseDropField.isNormalPosition, baseDropField.isMovement, false, baseDropField.cardMagnification);
        CardCreator.instance.CreateMemberCard(baseCard, putParent, putDropField.isFront, putDropField.isNormalPosition, putDropField.isMovement, false, putDropField.cardMagnification);
        Destroy(baseCard.gameObject);
        Destroy(putMember.gameObject);
    }

    /// <summary>
    /// ダメージ計算を行う
    /// </summary>
    /// <param name="member">対象のメンバーカード</param>
    /// <param name="damage">ダメージ</param>
    private void DamageCount(MemberController member, int damage)
    {
        member.DamageCount(damage);
    }

    private void AddEnergy(MemberController member)
    {
        member.AddEnergy(1);
    }

    private void MinusEnergy(MemberController member)
    {
        member.MinusEnergy(1);
    }
}
