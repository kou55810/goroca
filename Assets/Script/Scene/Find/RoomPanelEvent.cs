using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomPanelEvent: MonoBehaviour
{
    /// <summary>
    /// ルーム名
    /// </summary>
    [SerializeField] TextMeshProUGUI roomName;
    /// <summary>
    /// ルームパネルを押したときの処理
    /// </summary>
    public void OnClick_RoomPanel()
    {
        if (NetworkManager.instance.JoinRoom(roomName.text))
        {
            SceneManager.LoadScene("WaitScene");
        }
    }
}
