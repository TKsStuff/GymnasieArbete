using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgunScript : MonoBehaviour
{


    private bool isShooting;
    public Animation shotgunRecoil;
    private BoxCollider gunTrigger;
    public float range =  20f;
    public float vRange = 20f;


    // Start is called before the first frame update
    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1,vRange,range);
        gunTrigger.center = new Vector3(0,0, range * 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    void shoot()
    {
        if(Input.GetKey(KeyCode.Mouse0)) 
        {
            Debug.Log("shooting");
        }


    }

    void shotgunAnim()
    {

    }
}
