using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockCam : MonoBehaviour
{
    public Transform target;
    public float relativeHeigth = 15.0f;
    public float zDistance = 10.0f;
    public float dampSpeed = 2;
    public float xDistance = 10.0f;

    public float xRotate = 45.0f;
    public float yRotate = -45.0f;
    public float zRotate = 0;

    void fun()
    {
        Quaternion rotateThis = Quaternion.Euler(xRotate, yRotate, zRotate);
    }

    void Update()
    {


        fun();
    }



}
