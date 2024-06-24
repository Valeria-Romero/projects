using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootingWater : MonoBehaviour
{
    [SerializeField] ParticleSystem waterParticles;
    
    
    //Gun Stats
    public float reloadTime;
    public int magazineSize;

    //Reference
    public Camera fpsCam;
    public Transform shootingPoint;

    //Graphics
    public TextMeshProUGUI ammunitionDisplay;

    private int numberOfWaterUsed;
    public float camOffset = 10f;

    //Gun Stats
    public float timeBetweenShooting;

    //Stats in case we ever use magazines
    public int bulletsLeft;

    //bools
    public bool shooting, readyToShoot, reloading;

    

    //bug fixing
    public bool allowInvoke = true;

    private void Awake()
    {
        readyToShoot = true;
    }

    void Update()
    {
        //numberOfWaterUsed = waterParticles.particleCount;
        numberOfWaterUsed = waterParticles.emission.burstCount;
        //Debug.Log("Water used: " + numberOfWaterUsed);

        //Set ammo display, if it exists
        if (ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletsLeft + " / " + magazineSize);
        }

        transform.rotation = Quaternion.Euler(fpsCam.transform.eulerAngles.x-camOffset, fpsCam.transform.eulerAngles.y, fpsCam.transform.eulerAngles.z);
    }

    public void MyInput(bool shooting, bool reloadbutton)
    {
        //Reloading
        if (reloadbutton && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        if(readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            Reload();
        }
        
        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsLeft -= numberOfWaterUsed;
            Shoot();
        }

        //Stop Shooting
        if (!shooting || reloading || bulletsLeft <= 0)
        {
            waterParticles.Stop();
        }
    }

    private void Shoot()
    {
        waterParticles.Play();

        if (bulletsLeft <= 0)
        {
            bulletsLeft = 0;
        }

        readyToShoot = false;

        //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
    }

    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
