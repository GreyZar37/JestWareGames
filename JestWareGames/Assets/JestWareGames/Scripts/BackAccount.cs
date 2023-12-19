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

        pengeTekst.text = penge switch
        {
            >= 1000 and < 1000000 => "GAIN: " + "<color=white>" + (penge / 1000).ToString("f1") + "k " + " </color>" +
                                     "kr.",
            >= 1000000 and < 100000000000 => "GAIN: " + "<color=white>" + (penge / 1000000).ToString("f1") + "m " +
                                             " </color>" + "kr.",
            >= 100000000000 => "GAIN: " + "<color=white>" + (penge / 100000000000).ToString("f1") + "b " + " </color>" +
                               "kr.",
            _ => "GAIN: " + "<color=white>" + penge.ToString("f2") + " </color>" + "kr."
        };

        kontoTeskt.text = konto switch
        {
            >= 1000 and < 1000000 => "ACCOUNT: " + "<color=white>" + (konto / 1000).ToString("f1") + "k " +
                                     " </color>" + "kr.",
            >= 1000000 and < 100000000000 => "ACCOUNT: " + "<color=white>" + (konto / 1000000).ToString("f1") + "m " +
                                             " </color>" + "kr.",
            >= 100000000000 => "ACCOUNT: " + "<color=white>" + (konto / 100000000000).ToString("f1") + "b " +
                               " </color>" + "kr.",
            _ => "ACCOUNT: " + "<color=white>" + konto.ToString("f2") + " </color>" + "kr."
        };
    }
}
