using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class BlachGameManager : MonoBehaviour
{
    [SerializeField] Button dealButton, hitButton, standButton, betButton;

    
    public PlayerScript playerScript;
    public PlayerScript dealerScript;

    int standClicks = 0;

   [ SerializeField]
   TextMeshProUGUI standButtonText;

    // Start is called before the first frame update
    void Start()
    {
        
        dealButton.onClick.AddListener(() => DealClicked());
        hitButton.onClick.AddListener(() => hitClicked());
        standButton.onClick.AddListener(() => standClicked());




    }

    private void DealClicked()
    {
        GameObject.Find("DeckController").GetComponent<DeckScript>().Shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();
    }
    private void hitClicked()
    {
        if(playerScript.GetCard() <= 10)
        {
            playerScript.GetCard();
        }


    }
    private void standClicked()
    {
        standClicks++;
        if(standClicks > 1)
        {
            hitDealer();
            standButtonText.text = "Call";
        }
    }

    void hitDealer()
    {
        while (dealerScript.handValue < 16 && dealerScript.cardIndex < 10)
        {
            dealerScript.GetCard();
        }
    }



}
