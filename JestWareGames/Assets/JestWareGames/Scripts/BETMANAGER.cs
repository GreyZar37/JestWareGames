using UnityEngine;
using TMPro;
using System.Collections;
public class BETMANAGER : MonoBehaviour
{
    public TextMeshProUGUI betNotifikation;
    public static int notifikationNum;
    public PlayFabManager fabManager;

    int odds;
    public AudioSource audiosource;
    public AudioClip bet_;
    public AudioClip koKlokke;
    public AudioClip quit_;
    public AudioClip viErOverAlt;
    public AudioClip skalBet;
    public AudioClip nuHolderDu;


    public AudioClip skalDuHaveKvik;
    public Animator kvikPanel;
    public Animator leaderBoard;


    public Sprite[] betDude;
    public SpriteRenderer spriteRend;

    public Animator dudeAnimator;

    bool alowedToWithdraw = false;
    public GameObject dipositDisplay;

    public ParticleSystem betParticle;

    public GameObject textPrefab;
    TextMeshProUGUI moneyShowText;
    bool vandt;

    float pengeManFik = 0;

    bool jackPot;
    [SerializeField]
    GameObject jackpotMenu;
    [SerializeField]
    TextMeshProUGUI jackPotWinTxt;
    [SerializeField]
    GameObject[] winJackbot;
    float jackPotTimer = 5;

    float clickTimer;

    void Start()
    {
        dudeAnimator.SetBool("IsBetting", true);
    }

    void Update()
    {
        if(clickTimer > 0)
        {
            clickTimer -= Time.deltaTime;

        }


        if(jackPot == true)
        {
          
            jackPotTimer -= Time.deltaTime;

            if(jackPotTimer <= 0)
            {
               
                jackpotMenu.SetActive(false);

                jackPot = false;
                winJackbot[0].SetActive(false);
                winJackbot[1].SetActive(false);
                jackPotTimer = 5;
            }

        }
        
        betNotifikation.text = notifikationNum.ToString();

    }

    public void Bet()
    {
        odds = Random.Range(1, 105);
        if (!(BackAccount.penge > 20) || !(clickTimer <= 0)) return;
        audiosource.Stop();
        audiosource.PlayOneShot(koKlokke, 0.2f);
        alowedToWithdraw = true;

        betParticle.Play();
        ScreenShake.isShaking = true;

        switch (odds)
        {
            case > 103:
                jackPot = true;
                audiosource.Stop();
                audiosource.PlayOneShot(nuHolderDu);
                pengeManFik = BackAccount.penge * 9.5f - BackAccount.penge;
                BackAccount.penge *= 9.5f;
                spriteRend.sprite = betDude[2];
                JackPotShow();

                vandt = true;
                break;
            case > 97:
                pengeManFik = BackAccount.penge * 1.95f - BackAccount.penge;
                BackAccount.penge *= 2.25f;
                spriteRend.sprite = betDude[0];
                vandt = true;
                break;
            case > 85:
                pengeManFik = BackAccount.penge * 1.55f - BackAccount.penge;
                BackAccount.penge *= 1.95f;
                spriteRend.sprite = betDude[0];
                vandt = true;
                break;
            case > 65:
                pengeManFik = BackAccount.penge * 1.256f - BackAccount.penge;
                BackAccount.penge *= 1.256f;
                spriteRend.sprite = betDude[0];

                vandt = true;
                break;
            case > 40:
                pengeManFik = BackAccount.penge * 0.85f - BackAccount.penge;

                BackAccount.penge *= 0.75f;
                spriteRend.sprite = betDude[1];

                vandt = false;
                break;
            case > 10:
                pengeManFik = BackAccount.penge * 0.75f - BackAccount.penge;

                BackAccount.penge *= 0.70f;
                spriteRend.sprite = betDude[1];

                vandt = false;
                break;
            default:
                pengeManFik = BackAccount.penge * 0.70f - BackAccount.penge;

                BackAccount.penge *= 0.60f;
                spriteRend.sprite = betDude[1];

                vandt = false;
                break;
        }

        SpawnWinningSalary();
        clickTimer = 0.3f;
    }
    
    public void Deposit(int penge)
    {
        dudeAnimator.SetBool("IsBetting", true);

        if(BackAccount.konto >= penge)
        {
            BackAccount.penge += penge;
            BackAccount.konto -= penge;
            alowedToWithdraw = false;

        }
    }
    public void AllDeposit()
    {

        if(BackAccount.konto > 0)
        {
            dudeAnimator.SetBool("IsBetting", true);

            BackAccount.penge += BackAccount.konto;
            BackAccount.konto = 0;


            alowedToWithdraw = false;
        }
    

    }

    public void Quit()
    {
        SaveData.instance.saveGame();
        Application.Quit();
        
    }
    public void WithDraw()
    {
        if(alowedToWithdraw == true)
        {
            dudeAnimator.SetBool("IsBetting", false);

            spriteRend.sprite = betDude[0];

            BackAccount.konto += BackAccount.penge;
            BackAccount.penge = 0;
            audiosource.Stop();

            audiosource.PlayOneShot(viErOverAlt);
        }
        else
        {
            spriteRend.sprite = betDude[0];

            audiosource.Stop();

            audiosource.PlayOneShot(skalBet);
        }
       

    }

    public void CloseDepositOrOpen()
    {
        audiosource.Stop();

        audiosource.PlayOneShot(bet_);
        if (dipositDisplay.activeInHierarchy)
        {
            dipositDisplay.SetActive(false);
        }
        else
        {
            dipositDisplay.SetActive(true);

        }
    }


    public void SpawnWinningSalary()
    {
        
        
         

        GameObject spawnedText = Instantiate(textPrefab, new Vector3(Random.Range(4, 9), 5, -5), Quaternion.Euler(0, 0, Random.Range(-15, 0)));
        TextMeshPro text = spawnedText.GetComponent<TextMeshPro>();

        if (vandt == false)
        {
            text.color = new Color(255, 0, 0);
        }
        else
        {
            text.color = new Color(0, 255, 0);

        }

        text.text = pengeManFik switch
        {
            >= 1000 and < 1000000 => (pengeManFik / 1000).ToString("f1") + "k " + "kr.",
            >= 1000000 and < 100000000000 => (pengeManFik / 1000000).ToString("f1") + "m " + "kr.",
            >= 100000000000 => (pengeManFik / 100000000000).ToString("f1") + "b " + "kr.",
            <= -1000 and > -1000000 => (pengeManFik / 1000).ToString("f1") + "k " + "kr.",
            <= -1000000 and > -100000000000 => (pengeManFik / 1000000).ToString("f1") + "m " + "kr.",
            <= -100000000000 => (pengeManFik / 100000000000).ToString("f1") + "b " + "kr.",
            _ => (pengeManFik).ToString("f1") + "kr."
        };
    }

    public void OpenKvik()
    {
        kvikPanel.SetBool("Open", true);
       
        audiosource.Stop();
        audiosource.PlayOneShot(skalDuHaveKvik);

    }
    public void OpenLeaderBoard()
    {

        leaderBoard.SetBool("Open", true);
        fabManager.sendLeaderBoard(BackAccount.konto + BackAccount.penge);
        fabManager.GetLeaderboard();
        
        audiosource.Stop();
        audiosource.PlayOneShot(quit_);

    }
    public void LuckKvik()
    {
        
        kvikPanel.SetBool("Open", false);
        leaderBoard.SetBool("Open", false);

    }



    void JackPotShow()
    {
        jackpotMenu.SetActive(true);
        winJackbot[0].SetActive(true);
        winJackbot[1].SetActive(true);

        jackPotWinTxt.text = pengeManFik switch
        {
            >= 1000 and < 1000000 => (pengeManFik / 1000).ToString("f1") + "k " + "kr.",
            >= 1000000 and < 100000000000 => (pengeManFik / 1000000).ToString("f1") + "m " + "kr.",
            >= 100000000000 => (pengeManFik / 100000000000).ToString("f1") + "b " + "kr.",
            _ => (pengeManFik).ToString("f2") + "kr."
        };
    }
    



}
