using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class BlachGameManager : MonoBehaviour
{
    [SerializeField] Button dealButton, hitButton, standButton, betButton;

    [SerializeField]
    PlayerScript playerScript;
    [SerializeField]
    PlayerScript dealerScript;

    // Start is called before the first frame update
    void Start()
    {
        
        dealButton.onClick.AddListener(() => DealClicked());
        hitButton.onClick.AddListener(() => hitClicked());
        standButton.onClick.AddListener(() => standClicked());




    }

    private void DealClicked()
    {
        playerScript.StartHand();
    }
    private void hitClicked()
    {
        throw new NotImplementedException();
    }
    private void standClicked()
    {
        throw new NotImplementedException();
    }




}
