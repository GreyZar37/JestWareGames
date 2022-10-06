using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lost : MonoBehaviour
{

    [SerializeField]
    Animator gunAnim;

    [SerializeField]
    AudioSource audioSource_;
    [SerializeField]
    AudioClip gunSound;
    [SerializeField]
    GameObject[] removeObjects;
    [SerializeField]

    SpriteRenderer Christian, gun;
    [SerializeField]
    GameObject block;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(BackAccount.konto < -1000)
        {
            block.SetActive(true);
            gunAnim.SetTrigger("Lost");
            for (int i = 0; i < removeObjects.Length; i++)
            {
                removeObjects[i].SetActive(false);
            }
            Christian.enabled = true;
            gun.enabled = true;
        }
    }

    public void gunShot()
    {
        audioSource_.PlayOneShot(gunSound);
    }
    public void startOver()
    {
        BackAccount.penge = 100;
        BackAccount.konto = 300;

        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene(0);
    }
}
