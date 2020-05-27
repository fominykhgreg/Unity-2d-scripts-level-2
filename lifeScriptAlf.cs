using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifeScriptAlf : MonoBehaviour
{
    public static int lifeValue = 100;
    Text life;
    // Start is called before the first frame update
    void Start()
    {
        life = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeValue > 100)
        {
            lifeValue = 100;
        }
        life.text = "Lives: " + lifeValue;
        

    }
}
