using UnityEngine;

public class CoinModel
{
    bool isFront = true;

    /// <summary>
    /// コインの表面
    /// </summary>
    [SerializeField] public Sprite front;

    /// <summary>
    /// コインの裏面
    /// </summary>
    [SerializeField] public Sprite back;
    public CoinModel(bool isFront)
    {
        this.isFront = isFront;
    }
}
