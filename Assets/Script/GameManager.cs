using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public Guid firstPlayerId;
    public Guid turnPlayerId;
    public int turnCount = 0;
    public int readyCount = 0;
    public bool usedCandy = false;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] EnergyController energyPrefab;
    [SerializeField] MinusEnergyController minusEnergyPrefab;
    [SerializeField] MarkerController poisionMarkerPrefab;
    [SerializeField] MarkerController sleepMarkerPrefab;
    [SerializeField] Transform energyField;
    [SerializeField] Transform minusEnergyField;
    [SerializeField] Transform poisionMarkerField;
    [SerializeField] Transform sleepMarkerField;
    [SerializeField] Transform damageCounter10Field;
    [SerializeField] Transform damageCounter50Field;
    [SerializeField] Transform damageCounter100Field;
    [SerializeField] Transform healCounter10Field;
    [SerializeField] Transform healCounter50Field;
    [SerializeField] Transform healCounter100Field;
    [SerializeField] CoinTossField coinTossField;
    [SerializeField] CardShowField cardShowField;
    [SerializeField] CardListField cardListField;
    [SerializeField] PlayerData playerData;
    [SerializeField] PlayerData enemyData;

    public Dictionary<Guid, AbstractCardController> enemyCards = new Dictionary<Guid, AbstractCardController>();

    /// <summary>
    /// シングルトン実装
    /// </summary>
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            PhotonNetwork.AutomaticallySyncScene = false;
        }
        coinTossField.Hide();
        cardShowField.Hide();
        cardListField.Hide();
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            DecisionFirstPlayer();
        }
        Ready();
    }

    /// <summary>
    /// 先行プレイヤーを決める
    /// </summary>
    public void DecisionFirstPlayer()
    {
        photonView.RPC(nameof(RPCSetFirstPlayerId), RpcTarget.All, PhotonNetwork.PlayerList[UnityEngine.Random.Range(0, 2)].UserId);
    }

    /// <summary>
    /// 先行プレイヤーのIDセット
    /// </summary>
    /// <param name="userId"></param>
    [PunRPC]
    public void RPCSetFirstPlayerId(string userId)
    {
        firstPlayerId = Guid.Parse(userId);
        turnPlayerId = Guid.Parse(userId);
    }

    /// <summary>
    /// standbyフェーズか
    /// </summary>
    /// <returns></returns>
    public bool IsStunby()
    {
        return turnCount == 0;
    }

    public bool RamdamBool()
    {
        return (UnityEngine.Random.Range(0, 2) == 0);
    }

    public void Ready()
    {
        SetDamageCounter();
        SetEnergy();
        SetMarker();

        PlayerEntity playerEntity = GetPlayerSetting();
        InitPlayer(playerEntity, playerData);
        // 初期の手札
        for (int i = 0; i < 4; i++)
        {
            playerData.Draw();
        }
        photonView.RPC(nameof(RPCInitEnemy), RpcTarget.Others, playerEntity.id.ToString(), playerEntity.name, playerEntity.deckId, playerEntity.playerType.GetDescription());
    }

    ///////////////////////////
    ///////////////////////////
    /////////   RPC   /////////
    ///////////////////////////
    ///////////////////////////

    /// <summary>
    /// プレイヤーのEntity生成
    /// </summary>
    /// <returns></returns>
    public PlayerEntity GetPlayerSetting()
    {
        PlayerEntity player = new PlayerEntity();
        player.name = GrobalSettings.name;
        player.deckId = GrobalSettings.deckId;
        player.id = Guid.Parse(PhotonNetwork.LocalPlayer.UserId);
        player.playerType = GrobalSettings.playerType;
        return player;
    }

    /// <summary>
    /// プレイヤーの初期化
    /// </summary>
    /// <param name="playerEntity"></param>
    /// <param name="playerData"></param>
    public void InitPlayer(PlayerEntity playerEntity, PlayerData playerData)
    {
        playerData.Init(playerEntity);
    }

    /// <summary>
    /// 敵の初期化
    /// </summary>
    /// <param name="playerEntity"></param>
    [PunRPC]
    public void RPCInitEnemy(string id, string name, int deckId, string playerType)
    {
        PlayerEntity enemy = new PlayerEntity();
        enemy.id = Guid.Parse(id);
        enemy.name = name;
        enemy.deckId = deckId;
        enemy.playerType = playerType == "red" ? PlayerType.RED : PlayerType.BLUE;
        InitPlayer(enemy, enemyData);
        if (turnPlayerId.Equals(Guid.Parse(PhotonNetwork.LocalPlayer.UserId)))
        {
            playerData.TurnStart();
            enemyData.TurnEnd();
        }
        else
        {
            playerData.TurnEnd();
            enemyData.TurnStart();
        }
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(GameStart());
        }
    }

    public System.Collections.IEnumerator GameStart()
    {
        yield return new WaitUntil(() => readyCount == 2);
        photonView.RPC(nameof(RPCOpenCards), RpcTarget.All);
        photonView.RPC(nameof(RPCTurnStart), RpcTarget.All);
    }
    [PunRPC]
    public void RPCOpenCards()
    {
        Transform playerBattleField = GameObject.Find("PlayerBattleField").transform;
        OpenCard(playerBattleField);
        Transform enemyBattleField = GameObject.Find("EnemyBattleField").transform;
        OpenCard(enemyBattleField);
        Transform playerBenchField1 = GameObject.Find("PlayerBenchField1").transform;
        OpenCard(playerBenchField1);
        Transform playerBenchField2 = GameObject.Find("PlayerBenchField2").transform;
        OpenCard(playerBenchField2);
        Transform playerBenchField3 = GameObject.Find("PlayerBenchField3").transform;
        OpenCard(playerBenchField3);
        Transform enemyBenchField1 = GameObject.Find("EnemyBenchField1").transform;
        OpenCard(enemyBenchField1);
        Transform enemyBenchField2 = GameObject.Find("EnemyBenchField2").transform;
        OpenCard(enemyBenchField2);
        Transform enemyBenchField3 = GameObject.Find("EnemyBenchField3").transform;
        OpenCard(enemyBenchField3);
    }

    public void OpenCard(Transform trans)
    {
        for (int i = 0; i < trans.childCount; i++)
        {
            // インデックスを使って子Transformを取得
            Transform childTransform = trans.GetChild(i);
            MemberController card = childTransform.GetComponent<MemberController>();
            card.DisableBackside();
        }
    }

    public void SetEnergy()
    {
        EnergyController energy = Instantiate(energyPrefab, energyField);
        MinusEnergyController minusEnergy = Instantiate(minusEnergyPrefab, minusEnergyField);
    }

    public void SetMarker()
    {
        MarkerController poisionMarker = Instantiate(poisionMarkerPrefab, poisionMarkerField);
        MarkerController sleepMarker = Instantiate(sleepMarkerPrefab, sleepMarkerField);
    }

    public void SetDamageCounter()
    {
        DamageCounterCreator.instance.CreateDamegeCounter(10, damageCounter10Field);
        DamageCounterCreator.instance.CreateDamegeCounter(50, damageCounter50Field);
        DamageCounterCreator.instance.CreateDamegeCounter(100, damageCounter100Field);
        DamageCounterCreator.instance.CreateDamegeCounter(-10, healCounter10Field);
        DamageCounterCreator.instance.CreateDamegeCounter(-50, healCounter50Field);
        DamageCounterCreator.instance.CreateDamegeCounter(-100, healCounter100Field);
    }

    /// <summary>
    /// コインをクリックしたときの処理
    /// </summary>
    public void OnClick_Coin()
    {
        coinTossField.Show();
    }

    /// <summary>
    /// 自分のオフラインをクリックしたときの処理
    /// </summary>
    public void OnClick_Offline()
    {
        cardListField.Show(playerData.GetOfflineCards(), CardListType.PLAYER_OFFLINE);
    }

    /// <summary>
    /// 敵のオフラインをクリックしたときの処理
    /// </summary>
    public void OnClick_EnemyOffline()
    {
        cardListField.Show(enemyData.GetOfflineCards(), CardListType.ENEMY_OFFLINE);
    }

    /// <summary>
    /// プレイヤーデッキをクリックしたときの処理
    /// </summary>
    public void OnClick_PlayerDeck()
    {
        cardListField.Show(playerData.GetDeckCards(), CardListType.PLAYER_DECK);
    }

    /// <summary>
    /// 自分のオフラインを初期化する
    /// </summary>
    /// <param name="cards"></param>
    public void ResetOffline(List<int> cards)
    {
        playerData.ResetOffline(cards);
    }

    /// <summary>
    /// デッキにカードをセットする
    /// </summary>
    /// <param name="cards"></param>
    public void SetCardIds(List<int> cards)
    {
        playerData.SetDeckCards(cards);
    }

    /// <summary>
    /// カードを見る
    /// </summary>
    /// <param name="model"></param>
    public void ShowCardView(int id)
    {
        cardShowField.Show(id);
    }

    /// <summary>
    /// カードビューを隠す
    /// </summary>
    public void HideCardView()
    {
        cardShowField.OnClick_CloseButton();
    }

    /// <summary>
    /// 自分の手札にカードを追加する
    /// </summary>
    /// <param name="id"></param>
    public void AddHandForPlayer(int id)
    {
        playerData.AddHand(id);
    }

    /// <summary>
    /// 敵の手札にカードを追加する
    /// </summary>
    /// <param name="id"></param>
    public void AddHandForEnemy(int id)
    {
        enemyData.AddHand(id);
    }

    /// <summary>
    /// 飴を使ったフラグ変更
    /// </summary>
    /// <param name="used"></param>
    public void ChangeUsedCandy(bool used)
    {
        usedCandy = used;
    }

    public void OnClick_EndButton()
    {
        if (IsStunby())
        {
            GameObject battleField = GameObject.Find("PlayerBattleField");
            if (battleField.transform.childCount != 0)
            {
                turnCount = 1;
                ReadyCount();
            }
        }
        else
        {
            // ターン終了処理
            if (turnPlayerId.Equals(Guid.Parse(PhotonNetwork.LocalPlayer.UserId)))
            {
                photonView.RPC(nameof(RPCTurnEnd), RpcTarget.All, enemyData.playerId.ToString());
            }
        }
    }

    [PunRPC]
    public void RPCTurnStart() {

        turnCount++; ;
        if (turnPlayerId.Equals(Guid.Parse(PhotonNetwork.LocalPlayer.UserId)))
        {
            playerData.TurnStart();
        }
        else
        {
            enemyData.TurnStart();
        }
    }
    [PunRPC]
    public void RPCTurnEnd(string nextPlayerId)
    {
        turnPlayerId = Guid.Parse(nextPlayerId);

        if (!turnPlayerId.Equals(Guid.Parse(PhotonNetwork.LocalPlayer.UserId)))
        {
            playerData.TurnEnd();
        }
        else
        {
            enemyData.TurnEnd();
        }
        photonView.RPC(nameof(RPCTurnStart), RpcTarget.All);
    }

    [PunRPC]
    public void RPCReadyCount()
    {
        readyCount++;
    }

    public void ReadyCount()
    {
        photonView.RPC(nameof(RPCReadyCount), RpcTarget.MasterClient);
    }

    /// <summary>
    /// 敵側のカードを生成
    /// </summary>
    public void CreateEnemyCard(int number, Transform trans, bool isFront, bool isNormalPosition, bool isMovement, bool isAction, float scale = 1, Guid guid = default(Guid))
    {
        string transName = CardCreator.instance.GetEnemyTransformName(trans.name);
        photonView.RPC(nameof(RPCCreateCard), RpcTarget.Others, number, transName, isFront, !isNormalPosition, isMovement, isAction, scale, guid.ToString());
    }
    [PunRPC]
    public async Task RPCCreateCard(int number, string transName, bool isFront, bool isNormalPosition, bool isMovement, bool isAction, float scale = 1, string guid = "")
    {
        GameObject enemyTrans = GameObject.Find(transName);
        if (transName.Equals("EnemyHand"))
        {
            isFront = false;
        }
        AbstractCardController card = await CardCreator.instance.CreateCard(number, enemyTrans.transform, isFront, isNormalPosition, isMovement, isAction, scale, Guid.Parse(guid));
        enemyCards.Add(Guid.Parse(guid), card);
    }

    public void CreateEnemyMemberCard(MemberModel model, Transform trans, bool isFront, bool isNormalPosition, bool isMovement, bool isAction, float scale = 1, Guid guid = default(Guid))
    {
        string jsonString = JsonUtility.ToJson(model);
        string transName = CardCreator.instance.GetEnemyTransformName(trans.name);
        photonView.RPC(nameof(RPCCreateMemberCard), RpcTarget.Others, jsonString, transName, isFront, !isNormalPosition, isMovement, isAction, scale, guid.ToString());
    }
    [PunRPC]
    public async Task RPCCreateMemberCard(string modelJson, string transName, bool isFront, bool isNormalPosition, bool isMovement, bool isAction, float scale = 1, string guid = "")
    {
        MemberModel model = JsonUtility.FromJson<MemberModel>(modelJson);
        GameObject enemyTrans = GameObject.Find(transName);
        if (transName.Equals("EnemyHand"))
        {
            isFront = false;
        }
        AbstractCardController card = await CardCreator.instance.CreateMemberCard(model, enemyTrans.transform, isFront, isNormalPosition, isMovement, isAction, scale, Guid.Parse(guid));
        enemyCards.Add(Guid.Parse(guid), card);
    }

    public void DestroyCard(Guid guid)
    {
        photonView.RPC(nameof(RPCDestroyCard), RpcTarget.Others, guid.ToString());
    }

    [PunRPC]
    public void RPCDestroyCard(string guid)
    {
        if (enemyCards.ContainsKey(Guid.Parse(guid)))
        {
            Destroy(enemyCards[Guid.Parse(guid)].gameObject);
        }
    }

    public void AddEnergyCard(Guid guid, int count)
    {
        photonView.RPC(nameof(RPCAddEnergyCard), RpcTarget.Others, guid.ToString(), count);
    }
    [PunRPC]
    public void RPCAddEnergyCard(string guid, int count)
    {
        if (enemyCards.ContainsKey(Guid.Parse(guid)))
        {
            (enemyCards[Guid.Parse(guid)] as MemberController).AddEnergy(count);
        }
    }

    public void RemoveEnergyCard(Guid guid, int count)
    {
        photonView.RPC(nameof(RPCRemoveEnergyCard), RpcTarget.Others, guid.ToString(), count);
    }
    [PunRPC]
    public void RPCRemoveEnergyCard(string guid, int count)
    {
        if (enemyCards.ContainsKey(Guid.Parse(guid)))
        {
            (enemyCards[Guid.Parse(guid)] as MemberController).MinusEnergy(count);
        }
    }

    public void AddSpecialConditions(Guid guid, SpecialConditions specialConditions)
    {
        photonView.RPC(nameof(RPCAddSpecialConditions), RpcTarget.Others, guid.ToString(), specialConditions.GetDescription());
    }
    [PunRPC]
    public void RPCAddSpecialConditions(string guid, string specialConditions)
    {
        SpecialConditions conditions = SpecialConditions.NONE;
        switch (specialConditions)
        {
            case "poision":
                conditions = SpecialConditions.POISION;
                break;
            case "sleep":
                conditions = SpecialConditions.SLEEP;
                break;
            case "sleep_poision":
                conditions = SpecialConditions.POISION_AND_SLEEP;
                break;
            default:
                break;
        }
        if (enemyCards.ContainsKey(Guid.Parse(guid)))
        {
            (enemyCards[Guid.Parse(guid)] as MemberController).AddSpecialConditions(conditions);
        }
    }

    public void RemoveSpecialConditions(Guid guid)
    {
        photonView.RPC(nameof(RPCRemoveSpecialConditions), RpcTarget.Others, guid.ToString());
    }
    [PunRPC]
    public void RPCRemoveSpecialConditions(string guid)
    {
        if (enemyCards.ContainsKey(Guid.Parse(guid)))
        {
            (enemyCards[Guid.Parse(guid)] as MemberController).ResetSpecialConditions();
        }
    }

    public void DamageCount(Guid guid, int damege)
    {
        photonView.RPC(nameof(RPCDamageCount), RpcTarget.Others, guid.ToString(), damege);
    }

    [PunRPC]
    public void RPCDamageCount(string guid, int damege)
    {
        if (enemyCards.ContainsKey(Guid.Parse(guid)))
        {
            (enemyCards[Guid.Parse(guid)] as MemberController).DamageCount(damege);
        }
    }

    public void DeckShuffle()
    {
        playerData.DeckShuffle();
    }

    public void PointChanged(int point)
    {
        photonView.RPC(nameof(RPCPointChanged), RpcTarget.Others, point);
    }
    [PunRPC]
    public void RPCPointChanged(int point)
    {
        enemyData.PointChanged(point);
    }
}
