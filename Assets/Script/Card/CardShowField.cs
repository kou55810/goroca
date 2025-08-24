using System.Threading.Tasks;
using UnityEngine;

public class CardShowField : MonoBehaviour
{
    [SerializeField] public Transform cardField;
    AbstractCardController card = null;

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public async Task Show(int id)
    {
        this.gameObject.SetActive(true);
        if (card is not null) {
            Destroy(card.gameObject);
        }
        card = await CardCreator.instance.CreateCard(id, cardField, true, true, false, false);
    }

    public void OnClick_CloseButton()
    {
        if (card is not null)
        {
            Destroy(card.gameObject);
            card = null;
        }
        Hide();
    }
}
