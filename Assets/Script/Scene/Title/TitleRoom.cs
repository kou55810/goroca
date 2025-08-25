using Photon.Pun;
using UnityEngine;

public class TitleRoom : MonoBehaviourPunCallbacks
{
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public void CreateRoom()
    {
        NetworkManager.instance.CreateAndJoinRoom(PhotonNetwork.LocalPlayer.NickName, 2);
    }
}
