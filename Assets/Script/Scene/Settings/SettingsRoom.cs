using Photon.Pun;
using TMPro;
using UnityEngine;

public class SettingsRoom : MonoBehaviourPun
{
    public readonly int MAX_LENGTH = 7;
    [SerializeField] TMP_InputField playerName;
    [SerializeField] TMP_Dropdown dropdown;
    void Start()
    {
        if (string.IsNullOrEmpty(PhotonNetwork.LocalPlayer.NickName))
        {
            playerName.text = "Player";
        }
        else
        {
            playerName.text = PhotonNetwork.LocalPlayer.NickName;
            GrobalSettings.name = PhotonNetwork.LocalPlayer.NickName;
        }
        dropdown.value = GrobalSettings.deckId - 1;
        int selectId = GrobalSettings.deckId;
        PhotonNetwork.LocalPlayer.NickName = playerName.text;
        GrobalSettings.name = playerName.text;
    }
    public void CheckTextCount()
    {
        Debug.Log(playerName.text.Length);

        if (playerName.text.Length > MAX_LENGTH)
        {
            playerName.text = playerName.text[..MAX_LENGTH];
        }
    }

    public void OnClick_OKButton()
    {
        PhotonNetwork.LocalPlayer.NickName = playerName.text;
        int selectedDeck = dropdown.value + 1;
        GrobalSettings.deckId = selectedDeck;
        // シングルトンインスタンスを通じてSEPlayerにアクセス
        if (SEPlayer.instance != null)
        {
            SEPlayer.instance.Play03ButtonClickSoundEffect();
        }
    }
}
