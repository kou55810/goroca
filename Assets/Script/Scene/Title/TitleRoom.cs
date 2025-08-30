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

    public void PlaySound()
    {
        // シングルトンインスタンスを通じてSEPlayerにアクセス
        if (SEPlayer.instance != null)
        {
            SEPlayer.instance.Play03ButtonClickSoundEffect();
        }
    }
}
