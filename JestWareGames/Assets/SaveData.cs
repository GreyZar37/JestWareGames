using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData instance;
    float penge, konto;
   public static bool  forceShutDown;



    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        loadGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
            forceShutDown = true;
            Application.Quit();
        }
    }

    public void saveGame()
    {
        if (forceShutDown == false)
        {
            PlayerPrefs.SetFloat("GEVINST", BackAccount.penge);
            PlayerPrefs.SetFloat("KONTO", BackAccount.konto);
        }
     

    }
    public void loadGame()
    {
       BackAccount.penge = PlayerPrefs.GetFloat("GEVINST", 100f);
       BackAccount.konto = PlayerPrefs.GetFloat("KONTO", 300f);
    }
}
