using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    private static bool isFacingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
        void Update()
        {
            if (control.parx > 0 && !isFacingRight)
            {
            Flip();
        }
            if (control.parx < 0 && isFacingRight)
            {
            Flip(); 
            }
        }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    }

