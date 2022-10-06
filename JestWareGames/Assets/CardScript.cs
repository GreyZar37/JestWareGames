using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public int value;


    private void Start()
    {
        GetValueCard();
    }

    public int GetValueCard()
    {
        return value;
    }
    public void  SetValue(int Value)
    {
        value = Value;
    }
    public string GetSpriteName()
    {
        return GetComponent<SpriteRenderer>().sprite.name;
    }

    public void SetSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }


    public void ResetCard()
    {
        Sprite back = GameObject.Find("DeckController").GetComponent<DeckScript>().GetCardBack();
        gameObject.GetComponent<SpriteRenderer>().sprite = back;
        value = 0;
    }
}
