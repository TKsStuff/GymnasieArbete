using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;


    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Rotate( _rotation * Time.deltaTime);
    }
}
