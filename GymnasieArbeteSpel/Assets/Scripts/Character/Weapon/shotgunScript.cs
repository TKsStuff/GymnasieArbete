using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgunScript : MonoBehaviour
{


    private BoxCollider gunTrigger;
    public float range;
    public float vRange;
    public EnemyManager enemyManager;
   

    // Start is called before the first frame update
    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, vRange, range);
        gunTrigger.center = new Vector3(0, 0, range * 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        if (enemy) {
        
            enemyManager.AddEnemy(enemy);
        
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        if (enemy)
        {

            enemyManager.RemoveEnemy(enemy);
        }
    }





}