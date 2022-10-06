using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update


    public CardScript cardScript;
    public DeckScript deckScript;

    public int handValue;

    int money = 1000;

    public GameObject[] hand;

    public int cardIndex = 0;

    List<CardScript> aceList = new List<CardScript>();

    public void StartHand()
    {
        GetCard();

        GetCard();
    }

    public int GetCard()
    {
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        handValue += cardValue;
        if(cardValue == 1)
        {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());
        }
        cardIndex++;
        return handValue;
    }

}
