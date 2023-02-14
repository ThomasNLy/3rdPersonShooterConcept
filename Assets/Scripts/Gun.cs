using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.InputSystem;

public class Gun : ShootingRayCast2
{
    Mouse mouse;
    [Header("Bullet fx and shootingFX References")]
    public Transform bulletFX;

    public GameObject muzzleFlash;
    [Header("Fire Rate Variables")]
    [SerializeField]
    private float fireRate = 0.1f;
    [SerializeField]
    private float nextShot = 0f;

    [Header("Ammo Variables")]
    [SerializeField]
    private int totalAmmo;
    [SerializeField]
    private int magazineAmmo;
    [SerializeField]
    private int magazineSize;
    [SerializeField]
    private int reloadTime;
    [SerializeField]
    public bool reloading;

    private void Awake()
    {
        totalAmmo = 600;
        magazineAmmo = 30;
        magazineSize = 30;
        reloadTime = 2;
        reloading = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        mouse = Mouse.current;
        bulletFX.gameObject.GetComponent<ParticleSystem>().Stop();
        muzzleFlash.gameObject.GetComponent<ParticleSystem>().Stop();


   

    }

    // Update is called once per frame
    public override void Update()
    {
        Shoot();
    }


    public void Shoot()
    {
        if (mouse.leftButton.isPressed && nextShot >= fireRate && magazineAmmo > 0)
        {
            nextShot = 0f;
            base.ShootRay();
            muzzleFlash.gameObject.GetComponent<ParticleSystem>().Play();
            ShootBullet();
        }
        else if (magazineAmmo == 0 && mouse.leftButton.isPressed)
        {
            reloading = true;
            Invoke("Reload", reloadTime);
            

        }


        if (nextShot < fireRate)
        {
            nextShot += Time.deltaTime;
        }

        if (Keyboard.current.rKey.isPressed && magazineAmmo < magazineSize)
        {
            reloading = true;
            Invoke("Reload", reloadTime);
           


        }

    }

    protected override void DoSomethingWhenHit()
    {
        if (hitTarget.GetComponent<Health>() != null)
        {
            hitTarget.GetComponent<Health>().TakeDamage(1);
        }
        bulletFX.position = hit.point;
        bulletFX.gameObject.GetComponent<ParticleSystem>().Play();
    }


    private void Reload()
    {
        reloading = false;

        if (magazineAmmo < magazineSize && totalAmmo > magazineSize)
        {
            magazineAmmo = magazineSize;



        }
        else if (totalAmmo <= magazineSize)
        {

            magazineAmmo = totalAmmo;
        }
    }

    private void ShootBullet()
    {
        magazineAmmo -= 1;
        totalAmmo -= 1;
    }

    public int MagazineCount
    {
        get { return magazineAmmo; }
    }

    public int TotalAmmo
    {
        get { return totalAmmo; }
    }

    public int ReloadTime
    {
        get
        {
            return reloadTime;
        }
    }
}
