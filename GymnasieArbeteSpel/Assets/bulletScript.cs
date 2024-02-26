using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    //Assingables
    public Rigidbody rb;
    public LayerMask whatIsEnemies;
    public GameObject explosion;

    //Stats
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;



    //Damagevalues
    public int damage;
    public float explosionRadius;


    //Lifetime
    public int maxCollisions;
    public float maxLifeTime;
    public bool explodeOnTouch = true;

    int collisions;
    PhysicMaterial physic_Mat;



    private void Setup()
    {

        physic_Mat = new PhysicMaterial();
        physic_Mat.bounciness = bounciness;
        physic_Mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physic_Mat.bounceCombine = PhysicMaterialCombine.Maximum;
        GetComponent<SphereCollider>().material = physic_Mat;
        rb.useGravity = useGravity;
    }


    
    void Start()
    {
        
    }

   
    void Update()
    {
        //
        if (collisions > maxCollisions) Explode();

        maxLifeTime -= Time.deltaTime;
        if (maxLifeTime <= 0) Explode();


    }


    private void Explode()
    {
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRadius, whatIsEnemies);
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<enemyAi>().TakeDamage(damage);

        }
        Invoke("Delay", 0.05f);

    }


    private void Delay()
    {
        Destroy(gameObject);


    }


    private void OnCollisionEnter(Collision collision)
    {
        collisions++;
        if (collision.collider.CompareTag("Enemy") && explodeOnTouch) Explode();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
