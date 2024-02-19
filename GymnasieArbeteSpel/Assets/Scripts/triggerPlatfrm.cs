using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerPlatfrm : MonoBehaviour
{

    hiss platform;


    void Start()
    {
        platform = GetComponent<hiss>();
    }
    private void OnTriggerEnter(Collider other)
    {
        platform.canMove = true;
    }
}
