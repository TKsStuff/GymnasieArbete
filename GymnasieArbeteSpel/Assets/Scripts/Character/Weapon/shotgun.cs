using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class shotgun : MonoBehaviour
{
    //Bullet variable
    public GameObject bullet;

    public float shootForce, upwardForce;

    //Ammo UI
    public TextMeshProUGUI ammoCount;

    //recoil

    public Rigidbody playerRB;
    public float recoilForce;

    //Gun Stats
    public float fireRate, spread, reloadTime, timeBetweenBullets;
    public int magazineSize, bulletPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletShot;

    bool shooting, readyToShoot, reloading;

    //Refrence points
    public Camera fpscamera;
    public Transform attackPoint;

    public bool allowInvoke = true;


    public void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;

    }



    void Start()
    {

    }

    void Update()
    {
        fire();
        ammoCounter();

    }

    private void ammoCounter()
    {

        if (ammoCount != null)
        {

            ammoCount.SetText(bulletsLeft / bulletPerTap + " / " + magazineSize / bulletPerTap);
        }


    }

    private void fire()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.K);
        else shooting = Input.GetKeyDown(KeyCode.K);

        //shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletShot = 0;
            Shoot();
        }

        //reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

    }

    private void Shoot()
    {
        readyToShoot = false;
        bulletsLeft--;
        bulletShot++;

        //Find hitpoint of bullet, middle of screen
        Ray ray = fpscamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //Check for raycast hit

        Vector3 targetPoint = ray.GetPoint(5f);
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        //calculate direction from gun to targetPoint.
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Bullet Spead
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        float z = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, z, y);

        //Instantiate bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        currentBullet.transform.forward = directionWithoutSpread.normalized;

        //adds force to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
       // currentBullet.GetComponent<Rigidbody>().AddForce(fpscamera.transform.up * shootForce, ForceMode.Impulse);


        //Invokes resetShot if not allready invoked
        if (allowInvoke)
        {
            Invoke("resetShot", fireRate);
            allowInvoke = false;
            playerRB.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);

        }


        //if more bullets per tap, ex : shotgun
        if (bulletShot < bulletPerTap && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenBullets);

        }

    }


    private void resetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);

    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;

    }
}
