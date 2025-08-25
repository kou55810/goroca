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
    [SerializeField] Image playerIcon;

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

    public PlayerType playerType;

    void Start()
    {
        point1.GetComponent<CanvasGroup>().alpha = 0;
        point2.GetComponent<CanvasGroup>().alpha = 0;
        point3.GetComponent<CanvasGroup>().alpha = 0;
    }
    public void SetName(string name)
    {
        playerName.text = name;
    }
    public void SetPlayerIcon(string color)
    {
        playerIcon.sprite = Resources.Load<Sprite>($"Images/Field/player_icon_{color}");
    }

    public void OnClick_Point()
    {
        point++;
        if (point > 3)
        {
            point = 0;
        }
        ChangePointIcon();
        GameManager.instance.PointChanged(point);
    }

    public void PointChanged(int point)
    {
        this.point = point;
        ChangePointIcon();
    }

    public void AddPoint()
    {
        point++;
        if (point < 0)
        {
            point = 0;
        }
        else if (point > 3)
        {
            point = 3;
        }
        ChangePointIcon();
        GameManager.instance.PointChanged(point);
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
        GameManager.instance.PointChanged(point);
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

    public void SetSupport(bool used)
    {
        if (used)
        {
            supportText.alpha = 0;
        }
        else
        {
            supportText.alpha = 100;
        }
    }
    public void SetEnegry(bool used)
    {
        if (used)
        {
            energyText.alpha = 0;
        }
        else
        {
            energyText.alpha = 100;
        }
    }
}
