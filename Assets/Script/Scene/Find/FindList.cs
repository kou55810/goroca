using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FindList : MonoBehaviourPunCallbacks
{
    [SerializeField] RoomPanelContoroller roomPanelPrefab;
    [SerializeField] Transform roomListContent;
    // ルームリスト
    private List<RoomInfo> roomInfoList = new List<RoomInfo>();
    void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    /// <summary>
    /// 部屋情報が更新されたときの処理
    /// </summary>
    /// <param name="roomList">部屋のリスト</param>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");

        // 既存の部屋リストをクリア
        if (roomInfoList != null)
        {
            ClearRoomList();
        }

        // 新しいルームリストに更新
        roomInfoList = roomList;
        GetRoomList();
    }

    /// <summary>
    /// 部屋リストを取得する
    /// </summary>
    public void GetRoomList()
    {
        // 新しいルームリストに更新
        foreach (RoomInfo roomItem in roomInfoList)
        {
            if (roomItem.MaxPlayers > roomItem.PlayerCount) {
                RoomPanelContoroller roomPanel = Instantiate(roomPanelPrefab, roomListContent);
                roomPanel.Init(roomItem.Name);
            }
        }
    }

    /// <summary>
    /// 部屋リストを削除する
    /// </summary>
    public void ClearRoomList()
    {
        foreach (Transform transform in roomListContent.transform)
        {
            GameObject.Destroy(transform.gameObject);
        }
        roomInfoList.Clear();
    }
    
    /// <summary>
    /// 戻るボタンの処理
    /// </summary>
    public void OnClick_BackButton()
    {
        if (PhotonNetwork.InLobby)
        {
            // 退室
            PhotonNetwork.LeaveLobby();
            // シングルトンインスタンスを通じてSEPlayerにアクセス
            if (SEPlayer.instance != null)
            {
                SEPlayer.instance.Play03ButtonClickSoundEffect();
            }
            SceneManager.LoadScene("TitleScene");
        }
    }
}
