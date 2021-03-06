﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 1;
    public Rigidbody2D rb;
   
    // Start is called before the first frame update
    void Start()
    {
        
       rb.velocity = transform.right * speed ;
    }

    
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
       Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        //Debug.Log(hitInfo.name);
        Destroy(gameObject);
    }

   
}
