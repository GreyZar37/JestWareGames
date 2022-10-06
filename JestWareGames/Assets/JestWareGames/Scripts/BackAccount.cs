using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackAccount : MonoBehaviour
{

    public static float penge = 100;
    public static float konto = 300;

    public TextMeshProUGUI pengeTekst;
    public TextMeshProUGUI kontoTeskt;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            konto *= 10;
        }
       
        if (penge >= 1000 && penge < 1000000)
        {
            pengeTekst.text = "GAIN: " + "<color=white>" + (penge / 1000).ToString("f1")+ "k " + " </color>" + "kr.";

        }
        else if(penge >= 1000000 && penge < 100000000000)
        {
            pengeTekst.text = "GAIN: " + "<color=white>" + (penge / 1000000).ToString("f1") + "m " + " </color>" + "kr.";

        }
        else if (penge >= 100000000000)
        {
            pengeTekst.text = "GAIN: " + "<color=white>" + (penge / 100000000000).ToString("f1") + "b " + " </color>" + "kr.";

        }
        else
        {
            pengeTekst.text = "GAIN: " + "<color=white>" + penge.ToString("f2") + " </color>" + "kr.";

        }



        if (konto >= 1000 && konto < 1000000)
        {
            kontoTeskt.text = "ACCOUNT: " + "<color=white>" + (konto / 1000).ToString("f1") + "k " + " </color>" + "kr.";

        }
        else if (konto >= 1000000 && konto < 100000000000)
        {
            kontoTeskt.text = "ACCOUNT: " + "<color=white>" + (konto / 1000000).ToString("f1") + "m " + " </color>" + "kr.";

        }
        else if (konto >= 100000000000)
        {
            kontoTeskt.text = "ACCOUNT: " + "<color=white>" + (konto / 100000000000).ToString("f1") + "b " + " </color>" + "kr.";

        }
        else
        {
            kontoTeskt.text = "ACCOUNT: " + "<color=white>" + konto.ToString("f2") + " </color>" + "kr.";

        }

    }
}
