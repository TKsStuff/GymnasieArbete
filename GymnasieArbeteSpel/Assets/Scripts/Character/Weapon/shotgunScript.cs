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
    public float Cdamage = 10f;
    public float Ldamage = 5f;
    public LayerMask raycastLayerMask;

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
            var dir = enemy.transform.position - transform.position;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, range * 1.5f, raycastLayerMask)) {

                float dist = Vector3.Distance(enemy.transform.position, transform.position);

                if (hit.transform == enemy.transform) {

                    if(dist > range * 0.5f)
                    {
                        enemy.damageTaken(Ldamage);
                        


                    }
                    else
                    {
                        enemy.damageTaken(Cdamage);
                       
                    }
                  
                   


                }
            
            
            }



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