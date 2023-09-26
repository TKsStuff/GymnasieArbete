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
    public float fireRate;
    private float timeToFire;
    public float damage = 10f;
   

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
        if(Input.GetMouseButtonDown(0)&& Time.time > timeToFire)
        {
            shoot();
        }
    }



    void shoot() 
    {
     foreach(var enemy in enemyManager.enemiesInTrigger)
        {
            enemy.damageTaken(damage);
        }
        
        
        
        timeToFire = Time.time + fireRate;
    
    
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