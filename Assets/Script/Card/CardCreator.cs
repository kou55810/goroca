using UnityEngine;

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
    /// エネルギーカードのprefab
    /// </summary>
    [SerializeField] EnergyController energyPrefab;

    /// <summary>
    /// シングルトン実装
    /// </summary>
    public static CardCreator instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// メンバーカードの生成
    /// </summary>
    /// <param name="number">cardId</param>
    /// <param name="trans">生成場所</param>
    /// <param name="isFront">表面か</param>
    /// <param name="scale">サイズ</param>
    /// <returns></returns>
    public MemberController CreateMemberCard(int number, Transform trans, bool isFront, float scale = 1)
    {
        MemberController card = Instantiate(memberPrefab, trans);
        card.Init(number);
        card.transform.localScale = new Vector2(scale, scale);
        card.transform.localPosition = Vector2.zero;
        if (isFront)
        {
            Transform child = card.transform.Find("Backside");
            child.GetComponent<CanvasGroup>().alpha = 0;
        }
        return card;
    }
}
