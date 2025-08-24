using UnityEngine;

/// <summary>
/// ダメカン生成クラス
/// </summary>
public class DamageCounterCreator : MonoBehaviour
{
    /// <summary>
    /// ダメカンのprefab
    /// </summary>
    [SerializeField] DamageCounterController dameconPrefab;

    /// <summary>
    /// シングルトン実装
    /// </summary>
    public static DamageCounterCreator instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// ダメカンの生成
    /// </summary>
    /// <param name="damage">ダメージ数</param>
    /// <param name="trans">表示場所</param>
    /// <returns></returns>
    public DamageCounterController CreateDamegeCounter(int damage, Transform trans)
    {
        DamageCounterController damecon = Instantiate(dameconPrefab, trans);
        damecon.Init(damage);
        return damecon;
    }
}
