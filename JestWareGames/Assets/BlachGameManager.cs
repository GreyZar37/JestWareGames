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


    [SerializeField]
    TextMeshProUGUI scoreText, dealerScoreText, betsText, cashText, standButtonText, mainText;

    [SerializeField]
    GameObject hideCard;
    int pot = 0;


    // Start is called before the first frame update
    void Start()
    {
        
        dealButton.onClick.AddListener(() => DealClicked());
        hitButton.onClick.AddListener(() => hitClicked());
        standButton.onClick.AddListener(() => standClicked());

        betButton.onClick.AddListener(() => betClicked());



    }

    private void DealClicked()
    {
        playerScript.resetHand();
        dealerScript.resetHand();
        mainText.gameObject.SetActive(false);
        dealerScoreText.gameObject.SetActive(false);
        GameObject.Find("DeckController").GetComponent<DeckScript>().Shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();
        scoreText.text = "Hand: " + playerScript.handValue.ToString();
        dealerScoreText.text = "Hand: " + dealerScript.handValue.ToString();
        hideCard.GetComponent<Renderer>().enabled = true;
        dealButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);
        standButtonText.text = "Stand";
        pot = 40;
        playerScript.AdjustMoney(-20);
        cashText.text = "$" + playerScript.getMoney().ToString();
        betsText.text = "Bets: $" + pot.ToString();
    }
    private void hitClicked()
    {
        if(playerScript.cardIndex <= 10)
        {
            playerScript.GetCard();
            scoreText.text = "Hand: " + playerScript.handValue.ToString();
            if (playerScript.handValue > 20) RoundRover();
        }


    }
    private void standClicked()
    {
        standClicks++;
        if (standClicks > 1) RoundRover();
        hitDealer();
        standButtonText.text = "Call";
    }

    void hitDealer()
    {
        while (dealerScript.handValue < 16 && dealerScript.cardIndex < 10)
        {
            dealerScript.GetCard();
            dealerScoreText.text = "Hand: " + dealerScript.handValue.ToString();
            if (dealerScript.handValue > 20) RoundRover();
        }
    }


    void RoundRover()
    {
        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool player21 = playerScript.handValue == 21;
        bool dealer21 = dealerScript.handValue == 21;

        if (standClicks < 2 && !playerBust && !dealerBust && !player21 && !dealer21) return;

        bool roundOver = true;
       
        if(playerBust && dealerBust)
        {
            mainText.text = "All Bust: Bets returned";
            playerScript.AdjustMoney(pot / 2);
        }

        else if(playerBust || !playerBust && !dealerBust && dealerScript.handValue > playerScript.handValue)
        {
            mainText.text = "Dealer wins!";
        }
        else if(dealerBust || (!dealerBust && !playerBust && playerScript.handValue > dealerScript.handValue))
        {
            mainText.text = "You win!";
            playerScript.AdjustMoney(pot);
        }
        else if(playerScript.handValue == dealerScript.handValue)
        {
            mainText.text = "Push: Bets returned";
            playerScript.AdjustMoney(pot / 2);
        }
        else
        {
            roundOver = false;
        } 

        if (roundOver)
        {
            hitButton.gameObject.SetActive(false);
            standButton.gameObject.SetActive(false);
            dealButton.gameObject.SetActive(true); 
            mainText.gameObject.SetActive(true);
            dealerScoreText.gameObject.SetActive(true);
            hideCard.GetComponent<Renderer>().enabled = false;
            cashText.text = "$" + playerScript.getMoney().ToString();
            standClicks = 0;
        }
    }

    void betClicked()
    {
        TextMeshProUGUI newBet = betButton.GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        int intBet = int.Parse(newBet.text.ToString().Remove(0,1));
        playerScript.AdjustMoney(-intBet);
        cashText.text = "$" + playerScript.getMoney().ToString();
        pot += (intBet * 2);
        betsText.text = "Bets: $" + pot.ToString();
    }   
}
