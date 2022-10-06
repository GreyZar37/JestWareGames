using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetMenuUI : MonoBehaviour
{
    public Animator anim;
    bool opened;

    public AudioSource audio_;
    public AudioClip analysere;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void openEllerLuck()
    {
        if(opened == false)
        {
            anim.SetTrigger("Open");
            opened = true;
            audio_.Stop();

            audio_.PlayOneShot(analysere);

        }
        else
        {
            anim.SetTrigger("Luck");
            opened = false;

        }
    }
  
}
