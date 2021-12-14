using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathBar : MonoBehaviour
{
   private Transform cam;

   private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

  
   private void FixedUpdate()
    {
        transform.LookAt(cam);
    }
}
