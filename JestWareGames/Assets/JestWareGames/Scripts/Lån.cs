using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lån : MonoBehaviour
{
    [Header("Nikolaj")]
    public AudioSource audio_;

    [SerializeField]
    AudioClip skalBetale, skalDuHaveKvik, renter;

    [SerializeField]
    float lån;
    [SerializeField]

    float åop;

    [SerializeField]
    float timerLån;

    float timer;

    [SerializeField]
    TextMeshProUGUI åopText, lånText, timerText, buttonText;

    bool lånTaget;
    bool advardsel;

    string lånTagetString;
    public int lånId;

    private void Awake()
    {
        lånTagetString = PlayerPrefs.GetString("Lånt" + lånId.ToString(), "No"); ;
        timer = PlayerPrefs.GetFloat("Tid" + lånId.ToString(), timerLån * 60);



        buttonText.text = "Loan";
        åopText.text = "ÅOP: "+ (100 * åop).ToString()+ "%";
        lånText.text = "Loan: " + lån.ToString() + "kr.";


    }

    // Update is called once per frame
    void Update()
    {
      
        PlayerPrefs.SetFloat("Tid" + lånId.ToString(), timer);

        if (lånTagetString == "Yes")
        {
            lånTaget = true;
            buttonText.text = "Pay";

        }


        if (lånTaget)
        {
            timer -= Time.deltaTime;

        }


        if (timer > 0 && timer < 120 && advardsel == false)
        {
            audio_.Stop();

            audio_.PlayOneShot(skalBetale);

            advardsel = true;
        }
        else if(timer <= 0 && lånTaget == true)
        {
            BackAccount.konto -= lån * (åop * 2);
            buttonText.text = "Lån";
            timer = timerLån * 60;

            lånTaget = false;
            advardsel = false;
            lånTagetString = "No";
            PlayerPrefs.SetString("Lånt" + lånId.ToString(), lånTagetString);

        }


        if (timer > 60)
        {
            timerText.text = "Time: " + (timer / 60).ToString("F0") + "m";

        }
        else
        {
            timerText.text = "Time: " + timer.ToString("F1") + "s";
        }

    }

    public void takeLoanOrPay()
    {

        if(lånTaget == false)
        {
            audio_.Stop();
            audio_.PlayOneShot(renter);
            BackAccount.konto += lån;
            buttonText.text = "Pay";
            lånTaget = true;
            timer = timerLån * 60;
            lånTagetString = "Yes";

            PlayerPrefs.SetString("Lånt" + lånId.ToString(), lånTagetString);

        }
        else
        {
            BackAccount.konto -= lån * åop;
            buttonText.text = "Loan";
            lånTaget = false;

            lånTagetString = "No";
            PlayerPrefs.SetString("Lånt" + lånId.ToString(), lånTagetString);
        }
        advardsel = false;
        timer = timerLån * 60;
    }
}
