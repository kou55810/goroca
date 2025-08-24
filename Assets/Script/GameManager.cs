using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly Color COLOR_RED = new Color(0.92549f, 0.31765f, 0.31765f, 1.0f);
    private readonly Color COLOR_BLUE = new Color(0.31765f, 0.48235f, 0.92549f, 1.0f);
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

    /// <summary>
    /// シングルトン実装
    /// </summary>
    public static GameManager instance;

    private void Start()
    {
        PlayerEntity player = new PlayerEntity();
        player.deckId = 1;
        playerData.Init(player);
        playerData.AddPoint();
        playerData.ChangeBackgroundColor(COLOR_RED);
        playerData.Draw();
        playerData.Draw();
        playerData.Draw();
        playerData.Draw();

        PlayerEntity enemy = new PlayerEntity();
        enemy.deckId = 1;
        enemyData.Init(enemy);
        enemyData.ChangeBackgroundColor(COLOR_BLUE);
        enemyData.Draw();
        enemyData.Draw();
        enemyData.Draw();
        enemyData.Draw();

        EnergyController energy = Instantiate(energyPrefab, energyField);
        MinusEnergyController minusEnergy = Instantiate(minusEnergyPrefab, minusEnergyField);
        MarkerController poisionMarker = Instantiate(poisionMarkerPrefab, poisionMarkerField);
        MarkerController sleepMarker = Instantiate(sleepMarkerPrefab, sleepMarkerField);

        DamageCounterCreator.instance.CreateDamegeCounter(10, damageCounter10Field);
        DamageCounterCreator.instance.CreateDamegeCounter(50, damageCounter50Field);
        DamageCounterCreator.instance.CreateDamegeCounter(100, damageCounter100Field);
        DamageCounterCreator.instance.CreateDamegeCounter(-10, healCounter10Field);
        DamageCounterCreator.instance.CreateDamegeCounter(-50, healCounter50Field);
        DamageCounterCreator.instance.CreateDamegeCounter(-100, healCounter100Field);
        // CoinController coin = Instantiate(coinPrefab, coinField);
        // coin.ChangeFrontAndBack(true);
        // coin.DisableClickEvent();
        // // cardPrefabをPlayerHandに生成する
        // Instantiate(cardPrefab, playerHand);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        coinTossField.Hide();
        cardShowField.Hide();
        cardListField.Hide();
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
}
