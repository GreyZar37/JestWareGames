using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    public float deathTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * 10 * Time.deltaTime);
        deathTimer -= Time.deltaTime;
        if(deathTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
