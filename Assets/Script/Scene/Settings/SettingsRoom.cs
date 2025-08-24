using TMPro;
using UnityEngine;

public class SettingsRoom : MonoBehaviour
{
    public readonly int MAX_LENGTH = 7;
    [SerializeField] TMP_InputField playerName;
    
    public void CheckTextCount()
    {
        Debug.Log(playerName.text.Length);

        if (playerName.text.Length > MAX_LENGTH)
        {
            playerName.text = playerName.text[..MAX_LENGTH];
        }
    }
}
