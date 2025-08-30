using TMPro;
using UnityEngine;

public class PlayerShowTextField : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void SetText(bool isFirstPlayer)
    {
        if (isFirstPlayer)
        {
            text.text = "あなたが先行です";
        }
        else
        {
            text.text = "あなたが後攻です";
        }
        
    }
}
