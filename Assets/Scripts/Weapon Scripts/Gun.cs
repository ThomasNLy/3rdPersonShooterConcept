using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : ShootingRayCast2
{
    
    [Header("Bullet fx and shootingFX References")]
    public Transform bulletFX;

    public GameObject muzzleFlash;
    [Header("Fire Rate Variables")]
    [SerializeField]
    public float fireRate = 0.1f;
    [SerializeField]
    public float nextShot = 0f;

    [Header("Ammo Variables")]
    [SerializeField]
    protected int totalAmmo;
    [SerializeField]
    protected int magazineAmmo;
    [SerializeField]
    protected int magazineSize;
    [SerializeField]
    protected int reloadTime;
    [SerializeField]
    public bool reloading;

    private void Awake()
    {
        totalAmmo = 600;
        magazineAmmo = 30;
        magazineSize = 30;
        reloadTime = 2;
        reloading = false;
        nextShot = fireRate;
    }
    // Start is called before the first frame update
    void Start()
    {
      
        Init();
    }

    protected virtual void Init()
    {
        bulletFX.gameObject.GetComponent<ParticleSystem>().Stop();
        muzzleFlash.gameObject.GetComponent<ParticleSystem>().Stop();
    }

    public virtual void Shoot()
    {
        if (nextShot >= fireRate)
        {
            nextShot = 0;
            base.ShootRay();
            muzzleFlash.gameObject.GetComponent<ParticleSystem>().Play();
            AudioManager.Instance.PlaySMGSoundEffect();
            ShootBullet();
        }
        //if (mouse.leftButton.isPressed && nextShot >= fireRate && magazineAmmo > 0)
        //{
        //    nextShot = 0f;
        //    base.ShootRay();
        //    muzzleFlash.gameObject.GetComponent<ParticleSystem>().Play();
        //    ShootBullet();
        //}
        //else if (magazineAmmo == 0 && mouse.leftButton.isPressed)
        //{
        //    reloading = true;
        //    Invoke("Reload", reloadTime);


        //}


        //if (nextShot < fireRate)
        //{
        //    nextShot += Time.deltaTime;
        //}

        //if (Keyboard.current.rKey.isPressed && magazineAmmo < magazineSize)
        //{
        //    reloading = true;
        //    Invoke("Reload", reloadTime);



        //}

    }

    public virtual void FireRateCoolDown()
    {
        if (nextShot < fireRate)
        {
            nextShot += Time.deltaTime;
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


    public void Reload()
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

    protected void ShootBullet()
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
    public int MagazineSize
    {
        get { return magazineSize; }
    }
        
}
