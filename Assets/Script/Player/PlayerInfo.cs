using TMPro;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    /// <summary>
    /// プレイヤー名
    /// </summary>
    [SerializeField] TextMeshProUGUI playerName;

    /// <summary>
    /// 勝利点マーク1
    /// </summary>
    [SerializeField] Transform point1;

    /// <summary>
    /// 勝利点マーク2
    /// </summary>
    [SerializeField] Transform point2;

    /// <summary>
    /// 勝利点マーク3
    /// </summary>
    [SerializeField] Transform point3;

    /// <summary>
    /// 背景
    /// </summary>
    [SerializeField] Image background;

    /// <summary>
    /// サポートテキスト
    /// </summary>
    [SerializeField] TextMeshProUGUI supportText;

    /// <summary>
    /// エネルギーテキスト
    /// </summary>
    [SerializeField] TextMeshProUGUI energyText;
    /// <summary>
    /// 勝利点
    /// </summary>
    public int point = 0;

    /// <summary>
    /// サポートを使ったか
    /// </summary>
    public bool usedSupport = false;

    /// <summary>
    /// エネルギーをつけたか
    /// </summary>
    public bool usedEnergy = false;

    void Start()
    {
        point1.GetComponent<CanvasGroup>().alpha = 0;
        point2.GetComponent<CanvasGroup>().alpha = 0;
        point3.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void AddPoint()
    {
        point++;
        ChangePointIcon();
    }

    public void RemovePoint()
    {
        point--;
        if (point < 0)
        {
            point = 0;
        }
        else if (point > 3)
        {
            point = 3;
        }
        ChangePointIcon();
    }

    public void ChangePointIcon()
    {
        if (point == 3)
        {
            point1.GetComponent<CanvasGroup>().alpha = 100;
            point2.GetComponent<CanvasGroup>().alpha = 100;
            point3.GetComponent<CanvasGroup>().alpha = 100;
        }
        else if (point == 2)
        {
            point1.GetComponent<CanvasGroup>().alpha = 100;
            point2.GetComponent<CanvasGroup>().alpha = 100;
            point3.GetComponent<CanvasGroup>().alpha = 0;
        }
        else if (point == 1)
        {
            point1.GetComponent<CanvasGroup>().alpha = 100;
            point2.GetComponent<CanvasGroup>().alpha = 0;
            point3.GetComponent<CanvasGroup>().alpha = 0;
        }
        else if (point == 0)
        {
            point1.GetComponent<CanvasGroup>().alpha = 0;
            point2.GetComponent<CanvasGroup>().alpha = 0;
            point3.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    public void ChangeBackgroundColor(Color color)
    {
        background.color = color;
    }
}
