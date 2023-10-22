using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyHP = 10f;
    public EnemyManager enemyManager;
    public Transform Player;


    void Start()
    {

    }

   
    void Update()
    {
        this.gameObject.transform.LookAt(Player);
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
