using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform playerHand;
    private void Start()
    {
        CardCreator.instance.CreateMemberCard(1, playerHand, true, 0.25f);
        CardCreator.instance.CreateMemberCard(1, playerHand, false, 0.25f);
        // // cardPrefabをPlayerHandに生成する
        // Instantiate(cardPrefab, playerHand);
    }
}
