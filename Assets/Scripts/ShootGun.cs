using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootGun : ShootingRayCast2
{
    Mouse mouse;
    [Header("Bullet fx")]
    public Transform bulletFX;
    [Header("Fire Rate Variables")]
    [SerializeField]
    private float fireRate = 0.1f;
    [SerializeField]
    private float nextShot = 0f;
    // Start is called before the first frame update
    void Start()
    {
        mouse = Mouse.current;
        bulletFX.gameObject.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void ShootRay()
    {
        if (mouse.leftButton.isPressed && nextShot >= fireRate)
        {
            nextShot = 0f;
            base.ShootRay();
        }
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
}
