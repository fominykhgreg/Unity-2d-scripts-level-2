using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunduk : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {

        Player = GameObject.FindWithTag("Player");
    }
    // Update is called once per frame

    void OnCollisionEnter2D(Collision2D variable)
    {
        if (variable.gameObject == Player)
        {
            scoreScriptAlf.scoreValue += 300;
            
            Destroy(gameObject);
        }
    }
}