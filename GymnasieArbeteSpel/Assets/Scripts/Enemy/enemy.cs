using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float enemyHP = 10f;
    public EnemyManager enemyManager;


    void Start()
    {
        
    }

   
    void Update()
    {
        enemyDeath();
    }


    public void enemyDeath()
    {
        if(enemyHP <= 0)
        {
            enemyManager.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
    public void damageTaken(float damage)
    {
        enemyHP -= damage;
    }
}
