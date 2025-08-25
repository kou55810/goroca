using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckDropField :MonoBehaviourPunCallbacks, IDropHandler
{
    [SerializeField] DeckController deck;
    public void OnDrop(PointerEventData eventData)
    {
        string parentName = deck.transform.parent.name;
        if (eventData.pointerDrag.GetComponent<AbstractCardController>() is not null && parentName.Equals("PlayerDeck")) 
        {
            AbstractCardController card = eventData.pointerDrag.GetComponent<AbstractCardController>();
            deck.InsertBottomCard(card.model.id);
            card.DestroyCard();
        }
    }
}
