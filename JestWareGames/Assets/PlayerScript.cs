using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    

    public CardScript cardScript;
    public DeckScript deckScript;
    
    public int handValue;

    float money;

    public GameObject[] hand;

    public int cardIndex = 0;

    List<CardScript> aceList = new List<CardScript>();

    private void Start()
    {
        money = BackAccount.konto;
    }

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
        AceCheck();
        cardIndex++;
        return handValue;
    }

    public void AdjustMoney(int amount)
    {
        money += amount;
    }
    public float getMoney()
    {
        return money;
    }
    public void AceCheck()
    {
        foreach(CardScript ace in aceList)
        {
            if(handValue + 10 < 22 && ace.GetValueCard() == 1)
            {
                ace.SetValue(11);
                handValue += 10;
            }
            else if(handValue < 21 && ace.GetValueCard() == 11)
            {
                ace.SetValue(1);
                handValue -= 10;
            }
        }
    }

    public void resetHand()
    {
        for (int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }
        cardIndex = 0;
        handValue = 0;
        aceList = new List<CardScript> ();
    }
}
