using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BETPenge : MonoBehaviour
{
    [SerializeField]
    float pris;
    [SerializeField]
    float pengeAtGive;

    [SerializeField]
    float cooldownTimer;
    float currentTimer;

    [SerializeField]
    Slider timeSlider;

    [SerializeField]
    TextMeshProUGUI prisText;
    [SerializeField]
    TextMeshProUGUI pengeAtGiveText;

    bool bought;
    float multiplier = 1;
    public float addMultiplier;

    string bought_;
    public string name_;


    // Start is called before the first frame update
    void Start()
    {
        pengeAtGiveText.text = (pengeAtGive).ToString("F2") + "kr.";
        multiplier = PlayerPrefs.GetFloat("Multiplier" + name_, 1);
        pris = PlayerPrefs.GetFloat("Pris" + name_, pris);
        bought_ = PlayerPrefs.GetString("Bought" + name_, "No");

    }

    // Update is called once per frame
    void Update()
    {
        if(bought_ == "Yes")
        {
            bought = true;
        }

        timeSlider.value = currentTimer;
        timeSlider.maxValue = cooldownTimer;
       
        if (pris == 0 && bought == false)
        {
            prisText.text = "Free";

        }
        else
        {

            if (pris >= 1000 && pris < 1000000)
            {
                prisText.text = (pris / 1000).ToString("F2") + "k " + "kr.";

            }
            else if (pris >= 1000000 && pris < 100000000000)
            {
                prisText.text = (pris / 1000000).ToString("F2") + "m " + "kr.";

            }
            else if (pris >= 100000000000)
            {
                prisText.text = (pris / 100000000000).ToString("F2") + "b " + "kr.";

            }
            else
            {
                prisText.text = (pris).ToString("F2") + "kr.";

            }

        }

        if (bought == true)
        {
            currentTimer += Time.deltaTime;

            if (pengeAtGive * multiplier >= 1000 && pengeAtGive * multiplier < 1000000)
            {
                pengeAtGiveText.text = (pengeAtGive * multiplier / 1000).ToString("F2") + "k " + "kr.";

            }
            else if (pengeAtGive * multiplier >= 1000000 && pengeAtGive * multiplier < 100000000000)
            {
                pengeAtGiveText.text = (pengeAtGive * multiplier / 1000000).ToString("F2") + "m " + "kr.";

            }
            else if (pengeAtGive * multiplier >= 100000000000)
            {
                pengeAtGiveText.text = (pengeAtGive * multiplier / 100000000000).ToString("F2") + "b " + "kr.";

            }
            else
            {
                pengeAtGiveText.text = (pengeAtGive * multiplier).ToString("F2") + "kr.";

            }



        }

        if (currentTimer >= cooldownTimer && bought == true)
        {
            BackAccount.konto += pengeAtGive * multiplier;
            currentTimer = 0;
        }
    }

    public void k�b()
    {
        if (pris == 0)
        {
            pris = 100;
            bought = true;

        }
        else if(BackAccount.konto >= pris)
        {
            if(bought == false)
            {
                BackAccount.konto -= pris;
                pris *= 1.15f;

            }
            else
            {
                BackAccount.konto -= pris;

                multiplier += addMultiplier;
                pris *= 1.15f;
            }
          
            bought = true;

            PlayerPrefs.SetFloat("Pris" + name_, pris);
            PlayerPrefs.SetFloat("Multiplier" + name_, multiplier);
            PlayerPrefs.SetString("Bought" + name_, "Yes");
        }
    }
}
