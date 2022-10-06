using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayFabManager : MonoBehaviour
{

    public GameObject rowPrefab;
    public Transform rowParrent;


    // Start is called before the first frame update
    void Start()
    {
        login();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }

        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSucces, OnError);
    }

    void OnSucces(LoginResult result)
    {

        Debug.Log("Succesful login/account create!");
        string name = null;
        if (result.InfoResultPayload != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
           

        }

    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account");
        Debug.Log(error.GenerateErrorReport());
    }

    public void sendLeaderBoard(float score)
    {
        submit();

        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Sigma Reputation",
                    Value = (int)score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);


    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {

        Debug.Log("Successfull sent");

    }

    private void OnApplicationQuit()
    {
        SaveData.instance.saveGame();

        sendLeaderBoard(BackAccount.konto + BackAccount.penge);
        submit();

    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Sigma Reputation",
            StartPosition = 0,
            MaxResultsCount = 10

        };


        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardGet, OnError);


    }

    void OnLeaderBoardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in rowParrent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject newGameObject = Instantiate(rowPrefab, rowParrent);
            TextMeshProUGUI[] texts = newGameObject.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = item.StatValue.ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = (item.Position + 1).ToString();


            Debug.Log(string.Format("PLACE: {0} | ID: {1} | VALUE: {2}", item.Position, item.DisplayName, item.StatValue));
        }
    }

    public void submit()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = LoginScript.playerName
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }


    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated Name");
        
    }



}
