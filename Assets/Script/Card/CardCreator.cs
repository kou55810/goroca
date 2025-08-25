using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// カード生成クラス
/// </summary>
public class CardCreator : MonoBehaviour
{
    /// <summary>
    /// メンバーカードのprefab
    /// </summary>
    [SerializeField] MemberController memberPrefab;

    /// <summary>
    /// トレーナーカードのprefab
    /// </summary>
    [SerializeField] TrainersController trainersPrefab;

    /// <summary>
    /// シングルトン実装
    /// </summary>
    public static CardCreator instance;

    public List<int> memberIds = new List<int> { 1, 11, 12, 13 };
    public List<int> trainersIds = new List<int> { 2, 3, 4 };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        memberIds = new List<int>();
        for (int i = 1; i <= 31; i++)
        {
            memberIds.Add(i);
        }
        trainersIds = new List<int>();
        for (int i = 32; i <= 51; i++)
        {
            trainersIds.Add(i);
        }
    }

    /// <summary>
    /// カードの生成
    /// </summary>
    /// <param name="number">cardId</param>
    /// <param name="trans">生成場所</param>
    /// <param name="isFront">表面か</param>
    /// <param name="scale">サイズ</param>
    /// <returns></returns>
    public async Task<AbstractCardController> CreateCard(int number, Transform trans, bool isFront, bool isNormalPosition, bool isMovement, bool isAction, float scale = 1, Guid guid = default(Guid))
    {
        if (memberIds.Contains(number))
        {
            return await CreateMemberCard(number, trans, isFront, isNormalPosition, isMovement, isAction, scale, guid);
        }
        else if (trainersIds.Contains(number))
        {
            return await CreateTrainersCard(number, trans, isFront, isNormalPosition, isMovement, isAction, scale, guid);
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// メンバーカード生成
    /// </summary>
    /// <param name="number"></param>
    /// <param name="trans"></param>
    /// <param name="isFront"></param>
    /// <param name="isNormalPosition"></param>
    /// <param name="isMovement"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    private async Task<MemberController> CreateMemberCard(int number, Transform trans, bool isFront, bool isNormalPosition, bool isMovement, bool isAction, float scale = 1, Guid guid = default(Guid))
    {
        MemberController card = Instantiate(memberPrefab, trans);
        card.Init(number, guid);
        card.transform.localScale = new Vector2(scale, scale);
        card.transform.localPosition = Vector2.zero;
        // 道具を透過
        card.DisableBelongings();
        // 状態異常を透過
        card.ResetSpecialConditions();
        // energyを透過
        card.hideEnergy();

        if (isFront)
        {
            card.DisableBackside();
        }
        if (!isNormalPosition)
        {
            card.transform.transform.Rotate(0, 0, 180);
        }
        if (!isMovement)
        {
            Destroy(card.gameObject.GetComponent<CardMovement>());
        }
        if (trans.name.Equals("PlayerOffline") || trans.name.Equals("EnemyOffline") || trans.name.Equals("EnemyHand"))
        {
            card.DisableClickEvent();
        }
        if (trans.name.Equals("PlayerHand"))
        {
            card.DeleteCardDrop();
        }
        if (isAction)
        {
            await PutAction(card);
        }
        return card;
    }

    /// <summary>
    /// メンバーカード生成(modelから引き継ぐ)
    /// </summary>
    /// <param name="member"></param>
    /// <param name="trans"></param>
    /// <param name="isFront"></param>
    /// <param name="isNormalPosition"></param>
    /// <param name="isMovement"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public async Task<MemberController> CreateMemberCard(MemberModel model, Transform trans, bool isFront, bool isNormalPosition, bool isMovement, bool isAction, float scale = 1, Guid guid = default(Guid))
    {
        MemberController card = Instantiate(memberPrefab, trans);
        card.Init(model as MemberModel, guid);
        card.transform.localScale = new Vector2(scale, scale);
        card.transform.localPosition = Vector2.zero;
        // 道具
        if (model.belongings != 0)
        {
            card.SetBelongings(model.belongings);
        }
        else
        {
            card.DisableBelongings();
        }
        // 状態異常を透過
        card.ResetSpecialConditions();
        // energyを透過
        if (model.energyCount <= 0)
        {
            card.hideEnergy();
        }
        else
        {
            card.showEnergy();
        }

        if (isFront)
        {
            card.DisableBackside();
        }
        if (!isNormalPosition)
        {
            card.transform.transform.Rotate(0, 0, 180);
        }
        if (!isMovement)
        {
            Destroy(card.gameObject.GetComponent<CardMovement>());
        }
        if (trans.name.Equals("PlayerOffline") || trans.name.Equals("EnemyOffline") || trans.name.Equals("EnemyHand"))
        {
            card.DisableClickEvent();
        }
        if (trans.name.Equals("PlayerHand"))
        {
            card.DeleteCardDrop();
        }
        if (isAction)
        {
            await PutAction(card);
        }
        return card;
    }

    /// <summary>
    /// トレーナーカード生成
    /// </summary>
    /// <param name="number">id</param>
    /// <param name="trans">配置場所</param>
    /// <param name="isFront">表面か</param>
    /// <param name="isNormalPosition">正位置か</param>
    /// <param name="isMovement">D&D可能か</param>
    /// <param name="scale">サイズ</param>
    /// <returns></returns>
    private async Task<TrainersController> CreateTrainersCard(int number, Transform trans, bool isFront, bool isNormalPosition, bool isMovement, bool isAction, float scale = 1, Guid guid = default(Guid))
    {
        TrainersController card = Instantiate(trainersPrefab, trans);
        card.Init(number, guid);
        card.transform.localScale = new Vector2(scale, scale);
        card.transform.localPosition = Vector2.zero;

        if (isFront)
        {
            card.DisableBackside();
        }
        if (!isNormalPosition)
        {
            card.transform.transform.Rotate(0, 0, 180);
        }
        if (!isMovement)
        {
            Destroy(card.gameObject.GetComponent<CardMovement>());
        }
        if (trans.name.Equals("PlayerOffline") || trans.name.Equals("EnemyOffline") || trans.name.Equals("EnemyHand"))
        {
            card.DisableClickEvent();
        }
        if (trans.name.Equals("PlayerHand"))
        {
            card.DeleteCardDrop();
        }
        if (isAction)
        {
            await PutAction(card);
        }
        return card;
    }

    private async Task PutAction(AbstractCardController card)
    {
        int height = 20;
        float basePositionY = card.transform.position.y;
        card.transform.position = new Vector3(card.transform.position.x, basePositionY + height, card.transform.position.z);
        await Awaitable.WaitForSecondsAsync(1f);
        for (int i = height; i >= 0; i--)
        {
            card.transform.position = new Vector3(card.transform.position.x, basePositionY + i, card.transform.position.z);
            await Awaitable.WaitForSecondsAsync(0.001f);
        }
    }

    public string GetEnemyTransformName(string transName)
    {
        string name = "";
        switch (transName)
        {
            case "PlayerBattleField":
                name = "EnemyBattleField";
                break;
            case "PlayerBenchField1":
                name = "EnemyBenchField1";
                break;
            case "PlayerBenchField2":
                name = "EnemyBenchField2";
                break;
            case "PlayerBenchField3":
                name = "EnemyBenchField3";
                break;
            case "PlayerOffline":
                name = "EnemyOffline";
                break;
            case "PlayerHand":
                name = "EnemyHand";
                break;
        }
        return name;
    }
}
