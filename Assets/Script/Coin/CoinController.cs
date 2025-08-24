using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    public readonly float ROLL_COIN_SPEED = 0.00001f;
    public readonly float COIN_TOSS_SPEED = 0.0001f;
    public readonly int COIN_TOSS_HEIGHT = 600;
    bool isCoinToss = false;
    bool isFront = true;
    public CoinView view;
    private void Awake()
    {
        view = GetComponent<CoinView>();
    }

    public void Init()
    {
        view.Show();
        ChangeFrontAndBack(isFront);
    }

    /// <summary>
    /// コインの裏表を変更する
    /// </summary>
    /// <param name="isFront"></param>
    public void ChangeFrontAndBack(bool isFront)
    {
        this.isFront = isFront;
        if (this.isFront)
        {
            Transform front = this.transform.Find("Front");
            front.GetComponent<CanvasGroup>().alpha = 100;
            Transform back = this.transform.Find("Back");
            back.GetComponent<CanvasGroup>().alpha = 0;
        }
        else
        {
            Transform front = this.transform.Find("Front");
            front.GetComponent<CanvasGroup>().alpha = 0;
            Transform back = this.transform.Find("Back");
            back.GetComponent<CanvasGroup>().alpha = 100;
        }
    }

    /// <summary>
    /// コイントスを実行する
    /// </summary>
    public async Task CoinToss()
    {
        float randomValue = Random.Range(0.0f, 1.0f);
        if (randomValue < 0.5f)
        {
            isFront = true;
        }
        else
        {
            isFront = false;
        }

        ChangeFrontAndBack(isFront);
        KeepRollCoin();
        int distance = 10;
        for (int i = 0; i <= COIN_TOSS_HEIGHT; i += distance)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + distance, this.transform.position.z);
            await Awaitable.WaitForSecondsAsync(COIN_TOSS_SPEED);
        }

        await Awaitable.WaitForSecondsAsync(0.2f);

        for (int i = 0; i <= COIN_TOSS_HEIGHT; i += distance)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - distance, this.transform.position.z);
            await Awaitable.WaitForSecondsAsync(COIN_TOSS_SPEED);
        }
        isCoinToss = false;
    }

    /// <summary>
    /// コインを回転し続ける
    /// </summary>
    /// <returns></returns>
    public async Task KeepRollCoin()
    {
        isCoinToss = true;
        while (isCoinToss)
        {
            await RollCoin();
        }
    }

    /// <summary>
    /// コインを1回転させる
    /// </summary>
    /// <returns></returns>
    public async Task RollCoin()
    {
        int y = 0;
        for (int i = 0; i <= 180; i += 30)
        {
            this.transform.eulerAngles = new Vector3(i - y, 0, 0);

            if (i == 90)
            {
                isFront = !isFront;
                ChangeFrontAndBack(isFront);
                y = 180;
            }
            await Awaitable.WaitForSecondsAsync(ROLL_COIN_SPEED);
        }
        this.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// コインのクリックイベント
    /// </summary>
    public void OnClick_Coin()
    {
        CoinToss();
    }
}
