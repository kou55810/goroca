using System.Collections.Generic;
using NUnit.Framework;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitRoom: MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI player1_name;
    [SerializeField] TextMeshProUGUI player2_name;
    [SerializeField] Button startButton;
    [SerializeField] Button backButton;
    bool isEnterRoom; // 部屋に入ってるかどうかのフラグ
    bool isMatching; // マッチング済みかどうかのフラグ
    // private Dictionary<string, MatchingPlayer> playerDict = new Dictionary<string, MatchingPlayer>();

    /// <summary>
    /// スタート
    /// </summary>
    void Start()
    {
        // if (!PhotonNetwork.InLobby)
        // {
        //     PhotonNetwork.JoinLobby();
        // }
        startButton.interactable = false;
    }

    public override void OnJoinedLobby()
    {
        // NetworkManager.instance.CreateAndJoinRoom(PhotonNetwork.LocalPlayer.NickName, 2);
    }

    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        if (isMatching) return;

        if (isEnterRoom)
        {
            if (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount)
            {
                isMatching = true;
                Debug.Log("マッチング成功");
                if (PhotonNetwork.IsMasterClient)
                {
                    startButton.interactable = true;
                }
            }
        }
    }

    /// <summary>
    /// 部屋に入った時の処理
    /// </summary>
    public override void OnJoinedRoom()
    {
        isEnterRoom = true;
        if (PhotonNetwork.IsMasterClient)
        {
            player1_name.text = PhotonNetwork.NickName;
        }
        else
        {
            player2_name.text = PhotonNetwork.NickName;
        }
    }

    /// <summary>
    /// 他プレイヤーが部屋から抜けたときの処理
    /// </summary>
    /// <param name="otherPlayer">他プレイヤー情報</param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.CurrentRoom.MaxPlayers > PhotonNetwork.CurrentRoom.PlayerCount)
        {
            startButton.interactable = false;
            isMatching = false;
        }
        Debug.Log($"OnPlayerLeftRoom: {otherPlayer.ActorNumber}");
        PlayerDestroy(otherPlayer.ActorNumber);
    }

    /// <summary>
    /// プレイヤーが入ってきたとき
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            startButton.interactable = true;
        }
        isEnterRoom = true;
        if (PhotonNetwork.IsMasterClient)
        {
            player2_name.text = newPlayer.NickName;
            photonView.RPC(nameof(RPCSetEnemyName), RpcTarget.OthersBuffered, PhotonNetwork.NickName);
        }
        else
        {
            player1_name.text = newPlayer.NickName;
        }
    }

    [PunRPC]
    private void RPCSetEnemyName(string playerName)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            player2_name.text = playerName;
        }
        else
        {
            player1_name.text = playerName;
        }
    }

    /// <summary>
    /// 戻るボタンの処理
    /// </summary>
    public void OnClick_BackButton()
    {
        PlayerDestroy(PhotonNetwork.LocalPlayer.ActorNumber);
        if (PhotonNetwork.InRoom)
        {
            // 退室
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LeaveLobby();
            // シングルトンインスタンスを通じてSEPlayerにアクセス
            if (SEPlayer.instance != null)
            {
                SEPlayer.instance.Play03ButtonClickSoundEffect();
            }
            SceneManager.LoadScene("TitleScene");
        }
    }

    /// <summary>
    /// プレイヤー情報の削除
    /// </summary>
    /// <param name="actorNumber">プレイヤーID</param>
    private void PlayerDestroy(int actorNumber)
    {
        // var deleteUser = playerDict[actorNumber.ToString()];
        // if (deleteUser is not null)
        // {
        //     Debug.Log($"{deleteUser.id}, {deleteUser.name}");
        //     playerDict.Remove(actorNumber.ToString());
        //     Destroy(deleteUser.player.gameObject);
        // }
    }

    public void OnClick_StartButton()
    {
        GrobalSettings.playerType = PlayerType.RED;
        // シングルトンインスタンスを通じてSEPlayerにアクセス
        if (SEPlayer.instance != null)
        {
            SEPlayer.instance.Play03ButtonClickSoundEffect();
        }
        photonView.RPC(nameof(RPCShowGameScene),
            RpcTarget.OthersBuffered);
            SceneManager.LoadScene("GameScene");
    }

    [PunRPC]
    public void RPCShowGameScene()
    {
        GrobalSettings.playerType = PlayerType.BLUE;
        ShowGameScene();
    }

    public void ShowGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
