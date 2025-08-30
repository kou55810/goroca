using System.Threading.Tasks;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// カードビュー表示領域
/// </summary>
public class CardViewDropField : MonoBehaviourPunCallbacks, IDropHandler
{
    /// <summary>
    /// カードのサイズ
    /// </summary>
    public float cardMagnification = 1f;
    /// <summary>
    /// カードを表で表示するか
    /// </summary>
    public bool isFront = true;
    /// <summary>
    /// カードの向きは正位置か
    /// </summary>
    public bool isNormalPosition = true;
    public bool isCardTypeMember = true;
    public bool isCardTypeTool = true;
    public bool isCardTypeEvent = true;
    public bool isCardTypeBelongings = true;
    public bool isMovement = true;

    public bool isAction = false;

    public async void OnDrop(PointerEventData eventData)
    {
        if (this.name.Contains("Enemy")){
            // 敵の場にはカードを置けない
            return;
        }
        if (eventData.pointerDrag.GetComponent<AbstractCardController>() is null)
        {
            // 道具を捨てた場合
            if (eventData.pointerDrag.name.Equals("belongings") && this.name.Equals("PlayerOffline"))
            {
                BelongingsView belonging = eventData.pointerDrag.GetComponent<BelongingsView>();
                GameObject playerOffline = GameObject.Find("PlayerOffline");
                CardViewDropField playerOfflineField = playerOffline.GetComponent<CardViewDropField>();
                AbstractCardController dropCard = await CardCreator.instance.CreateCard(belonging.id, playerOffline.transform, playerOfflineField.isFront, playerOfflineField.isNormalPosition, playerOfflineField.isMovement, isAction, playerOfflineField.cardMagnification);
                GameManager.instance.CreateEnemyCard(belonging.id, playerOffline.transform, playerOfflineField.isFront, playerOfflineField.isNormalPosition, playerOfflineField.isMovement, isAction, playerOfflineField.cardMagnification, dropCard.guid);
                belonging.DisableBelongings();
                GameManager.instance.DisableBelongings(belonging.guid);
                // Destroy(belonging.gameObject);
            }
            return;
        }
        if (!isMovement)
        {

        }

        AbstractCardController card = eventData.pointerDrag.GetComponent<AbstractCardController>(); // ドラッグしてきた情報からCardControllerを取得
        if (!CheckCardType(card))
        {
            return;
        }
        if (card.movement != null) // もしカードがあれば、
        {
            if (card is MemberController)
            {
                MemberController member = card as MemberController;
                bool frontCheck = GameManager.instance.IsStunby() && !this.name.Equals("PlayerHand") ? false : isFront;
                // 手札から出た場合は新規
                if (card.IsParentTrans("PlayerHand"))
                {
                    // 手札から出る場合は新人でなれけばならない
                    if ((this.name.Equals("PlayerBattleField") || this.name.Contains("PlayerBenchField")) &&
                    !(member.model as MemberModel).memberType.Equals(MemberType.NEW_FACE))
                    {
                        return;
                    }
                    // カードを生成
                    AbstractCardController dropCard = await CardCreator.instance.CreateCard(card.model.id, this.transform, frontCheck, isNormalPosition, isMovement, isAction, cardMagnification);
                    GameManager.instance.CreateEnemyCard(card.model.id, this.transform, frontCheck, isNormalPosition, !isMovement, isAction, cardMagnification, dropCard.guid);
                    if ((this.name.Equals("PlayerBattleField") || this.name.Contains("PlayerBenchField")))
                    {
                        GameManager.instance.ActiveCardSE();
                    }
                    CardSE();
                }
                // オフラインから出る場合も新規
                else if (card.IsParentTrans("PlayerOffline"))
                {
                    AbstractCardController dropCard = await CardCreator.instance.CreateCard(card.model.id, this.transform, frontCheck, isNormalPosition, isMovement, isAction, cardMagnification);
                    GameManager.instance.CreateEnemyCard(card.model.id, this.transform, frontCheck, isNormalPosition, !isMovement, isAction, cardMagnification, dropCard.guid);
                    CardSE();
                }
                // それ以外はHPや道具を引き継ぐ
                else
                {
                    // 手札、オフラインの場合は状態を引き継がない
                    if (this.name.Equals("PlayerOffline") || this.name.Equals("PlayerHand"))
                    {
                        card.DestroyCard();
                        AbstractCardController dropCard = await CardCreator.instance.CreateCard(member.model.id, this.transform, isFront, isNormalPosition, isMovement, isAction, cardMagnification);
                        GameManager.instance.CreateEnemyCard(member.model.id, this.transform, isFront, isNormalPosition, isMovement, isAction, cardMagnification, dropCard.guid);
                        return;
                    }
                    else
                    {
                        card.DestroyCard();
                        MemberController memCard = await CardCreator.instance.CreateMemberCard(member.model as MemberModel, this.transform, frontCheck, isNormalPosition, isMovement, isAction, cardMagnification);
                        GameManager.instance.CreateEnemyMemberCard(member.model as MemberModel, this.transform, frontCheck, isNormalPosition, !isMovement, isAction, cardMagnification, memCard.guid);
                        return;
                    }
                }
            }
            else
            {
                if (this.name.Equals("Background"))
                {
                    GameManager.instance.UserCardSE();
                    // 飴を使った場合
                    if (card.model.id == 34)
                    {
                        GameManager.instance.ChangeUsedCandy(true);
                    }
                    ShowTrainers(card);
                    GameObject playerOffline = GameObject.Find("PlayerOffline");
                    CardViewDropField playerOfflineField = playerOffline.GetComponent<CardViewDropField>();
                    AbstractCardController dropCard = await CardCreator.instance.CreateCard(card.model.id, playerOffline.transform, playerOfflineField.isFront, playerOfflineField.isNormalPosition, playerOfflineField.isMovement, isAction, playerOfflineField.cardMagnification);
                    GameManager.instance.CreateEnemyCard(card.model.id, playerOffline.transform, playerOfflineField.isFront, playerOfflineField.isNormalPosition, playerOfflineField.isMovement, isAction, playerOfflineField.cardMagnification, dropCard.guid);
                }
                else
                {
                    AbstractCardController dropCard = await CardCreator.instance.CreateCard(card.model.id, this.transform, isFront, isNormalPosition, isMovement, isAction, cardMagnification);
                    GameManager.instance.CreateEnemyCard(card.model.id, this.transform, isFront, isNormalPosition, isMovement, isAction, cardMagnification, dropCard.guid);
                }
            }

            // カードオブジェクトを破壊する
            card.DestroyCard();
        }
    }

    /// <summary>
    /// カードタイプが正常か確認
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    private bool CheckCardType(AbstractCardController card)
    {
        if (card is MemberController && isCardTypeMember)
        {
            return true;
        }
        else if (card is TrainersController)
        {
            if (((card as TrainersController).model as TrainersModel).trainersType.Equals(TrainersType.TOOL) && isCardTypeTool)
            {
                return true;
            }
            else if (((card as TrainersController).model as TrainersModel).trainersType.Equals(TrainersType.EVENT) && isCardTypeEvent)
            {
                return true;
            }
            else if (((card as TrainersController).model as TrainersModel).trainersType.Equals(TrainersType.BELONGINGS) && isCardTypeBelongings)
            {
                return true;
            }
        }
        return false;
    }

    private void CardSE()
    {
        if ((this.name.Equals("PlayerBattleField") || this.name.Contains("PlayerBenchField")))
        {
            GameManager.instance.ActiveCardSE();
        }
    }

    private async Task ShowTrainers(AbstractCardController card)
    {
        card.ShowCard();
        await Awaitable.WaitForSecondsAsync(1f);
        card.HideCard();
    }
}
