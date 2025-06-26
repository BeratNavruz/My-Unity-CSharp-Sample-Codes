using UnityEngine;
using UnityEngine.EventSystems;

public class Card3 : MonoBehaviour, IPointerDownHandler
{
    public string cardType;
    private bool isOpen;
    public bool isOpenAnimFinish;
    public Sprite Icon;
    public CardFace3 CardBack;
    public CardFace3 CardFront;

    public LevelControllerCardMatching3 levelControllerCardMatching3;

    public bool IsOpen
    {
        get { return isOpen; }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isOpen && levelControllerCardMatching3.Clickability)
        {
            OpenCard();
        }
    }

    public void OpenCard()
    {
        levelControllerCardMatching3.CardOpened(this);
        isOpen = true;
        CardBack.Rotate(90, .25f, CardBackRotated_Opening);
    }

    public void CloseCard()
    {
        CardFront.Rotate(90, .25f, CardFrontRotated_Closing);
    }

    void CardBackRotated_Opening()
    {
        CardFront.Rotate(-90, .25f, CardFrontRotated_Opening);
    }

    void CardBackRotated_Closing()
    {
        isOpen = false;
        isOpenAnimFinish = false;
    }

    void CardFrontRotated_Opening()
    {
        isOpenAnimFinish = true;
        if (levelControllerCardMatching3.Time == 0 && !levelControllerCardMatching3.IsCorrect)
        {
            CloseCard();
            if (levelControllerCardMatching3.OpenCard1 != null)
                levelControllerCardMatching3.OpenCard1 = null;

            if (levelControllerCardMatching3.OpenCard2 != null)
                levelControllerCardMatching3.OpenCard2 = null;
        }
    }

    void CardFrontRotated_Closing()
    {
        CardBack.Rotate(-90, .25f, CardBackRotated_Closing);
    }
}
