using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
       
    }
   public void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
