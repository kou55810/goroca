using TMPro;
using UnityEngine;

public class RoomPanelView : MonoBehaviour
{
    /// <summary>
    /// 部屋名
    /// </summary>
    [SerializeField] TextMeshProUGUI roomName;
    
        public void Show(string nameText)
    {
        roomName.text = nameText;
    }
}
