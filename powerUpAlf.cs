using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpAlf : MonoBehaviour
{

    public GameObject Player;






    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }


    void OnCollisionEnter2D(Collision2D variable)
    {
        if (variable.gameObject == Player)
        {
            lifeScriptAlf.lifeValue += 10;
            Destroy(gameObject);

        }

    }
}

