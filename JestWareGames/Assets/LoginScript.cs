using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class LoginScript : MonoBehaviour
{

    public TMP_InputField loginInput;
    public static string playerName;



    // Start is called before the first frame update
    void Start()
    {
        playerName = PlayerPrefs.GetString("PlayerName", "");
        loginInput.text = playerName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void login()
    {
     

        if(loginInput.text != "")
        {
            playerName = loginInput.text;
            PlayerPrefs.SetString("PlayerName", playerName);
            SceneManager.LoadScene(1);

        }
    }
}
