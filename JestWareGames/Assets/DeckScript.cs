using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{

    public Sprite[] cardSprites;

    int[] cardvalues = new int[53];
    int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetCardValues();
    }

    // Update is called once per frame
    void GetCardValues()
    {
        int num = 0;
        for (int i = 0; i < cardSprites.Length; i++)
        {
            num = i;
            num %= 13;
            if(num > 10 || num == 0)
            {
                num = 10;
            }
            cardvalues[i] = num++;
        }
        currentIndex = 1;

    }
    public void Shuffle()
    {

        for (int i = cardSprites.Length - 1; i > 0; --i)
        {
            int j = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * cardSprites.Length - 1) + 1;
            Sprite face = cardSprites[i];

            cardSprites[i] = cardSprites[j];
            cardSprites[j] = face;

            int value = cardvalues[i];
            cardvalues[i] = cardvalues[j];
            cardvalues[j] = value;


        }


    }

    public int DealCard(CardScript cardScript)
    {
        cardScript.SetSprite(cardSprites[currentIndex]);
        cardScript.SetValue(cardvalues[currentIndex++]);
        return cardScript.GetValueCard();
    }

    public Sprite GetCardBack()
    {
        return cardSprites[0];
    }
}
