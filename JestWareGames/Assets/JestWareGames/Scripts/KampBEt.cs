using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class KampBEt : MonoBehaviour
{

    [Header("OverAll")]
    [SerializeField]
    TMP_InputField bel�b;

    int holdSelected_;
    float bel�bSatInd;
    float timer;
    bool battleStarted;

    string[] holdNavne = {"TechMeat", "Family", "BATMANS", "Gorillas", "United" , "Rockins" , 
        "SIGMAS" ,"Betters"};


    [SerializeField]
    Sprite[] holdSprites;


    [SerializeField]
    TextMeshProUGUI ScoreText, timerText;


    [Header("Hold 1")]

    [SerializeField]
    TextMeshProUGUI Navn1Text, ods1Tekst;
    [SerializeField]
    Image holdBillede1;

    float odds1;
    bool bettet1;

    [Header("Hold 2")]
   

    [SerializeField]
    TextMeshProUGUI Navn2Text, ods2Tekst;
    [SerializeField]
    Image holdBillede2;

    float odds2;
    bool bettet2;
    bool betted;


    private void Awake()
    {
        randomize();

    }

    // Update is called once per frame
    void Update()
    {
        
        if(bel�b.text == "" || bel�b.text == ",")
        {
            bel�bSatInd = 0;

        }
        else
        {

            bel�bSatInd = float.Parse(bel�b.text);

            if (float.Parse(bel�b.text) > BackAccount.konto && battleStarted == false && betted == false)
            {
                if(BackAccount.konto > 0)
                {
                    bel�bSatInd = BackAccount.konto;

                }
                else
                {
                    bel�bSatInd = 0;
                }
                bel�b.text = bel�bSatInd.ToString();
            }
        }

        if (timer <= 0 && battleStarted == false)
        {
          
            bel�b.interactable = false;
            timer = 0;
            StartCoroutine(startBattle());
            battleStarted = true;
        }
        else if (battleStarted == false)
        {

            timer -= Time.deltaTime;
        }

        timerText.text = timer.ToString("f1") + "s";

       
    }

    IEnumerator startBattle()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(20, 80));
        int randomChance = UnityEngine.Random.Range(0, 101);

      

        if(odds1 < odds2)
        {

            if(randomChance < (100 / odds1))
            {
                if(holdSelected_ == 1)
                {
                    BackAccount.konto += (bel�bSatInd * odds1);
                }
              
                ScoreText.text = UnityEngine.Random.Range(5, 10).ToString() + " - " + UnityEngine.Random.Range(0, 4).ToString();

            }
            else
            {
                if (holdSelected_ == 2)
                {
                    BackAccount.konto += (bel�bSatInd * odds2);
                }
              
                ScoreText.text = UnityEngine.Random.Range(0, 4).ToString() + " - " + UnityEngine.Random.Range(5, 10).ToString();

            }
        }
        else
        {

            if (randomChance < (100 / odds2))
            {
                if (holdSelected_ == 2)
                {
                    BackAccount.konto += (bel�bSatInd * odds2);
                }

                ScoreText.text = UnityEngine.Random.Range(0, 4).ToString() + " - " + UnityEngine.Random.Range(5, 10).ToString();

            }
            else
            {
                if (holdSelected_ == 1)
                {
                    BackAccount.konto += (bel�bSatInd * odds1);
                }
             
                ScoreText.text = UnityEngine.Random.Range(5, 10).ToString() + " - " + UnityEngine.Random.Range(0, 4).ToString();

            }

        }

        if (holdSelected_ != 0)
        {
            BETMANAGER.notifikationNum++;
        }

        yield return new WaitForSeconds(20);

        randomize();



    }

    void randomize()
    {
        timer = UnityEngine.Random.Range(40, 300);
        odds1 = (float)Math.Round(UnityEngine.Random.Range(1.1f, 2.5f), 1);
        odds2 = (float)Math.Round(UnityEngine.Random.Range(1.1f, 3.5f), 1);

        ods1Tekst.text = "ODDS: " + odds1.ToString();
        ods2Tekst.text = "ODDS: " + odds2.ToString();


        holdBillede1.sprite = holdSprites[UnityEngine.Random.Range(0, holdSprites.Length)];
        Navn1Text.text = holdNavne[UnityEngine.Random.Range(0, holdNavne.Length)];

        do
        {
            holdBillede2.sprite = holdSprites[UnityEngine.Random.Range(0, holdSprites.Length)];
            print("same");

        } while (holdBillede1.sprite == holdBillede2.sprite);

        do
        {
            Navn2Text.text = holdNavne[UnityEngine.Random.Range(0, holdNavne.Length)];
            print("same");

        } while (Navn1Text.text == Navn2Text.text);

        if (holdSelected_ != 0 && bel�bSatInd != 0)
        {
            BETMANAGER.notifikationNum--;
        }

        battleStarted = false;
        bel�b.interactable = true;
        bel�b.text = "";
        holdSelected_ = 0;
        ScoreText.text = "0 - 0";
        betted = false;

        

    }

    public void holdSelected(int hold)
    {
        if(holdSelected_ == 0)
        {
            holdSelected_ = hold;
            betted = true;
            bel�b.interactable = false;
            BackAccount.konto -= float.Parse(bel�b.text);
        }


    }
}
