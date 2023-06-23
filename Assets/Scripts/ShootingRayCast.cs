using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class ShootingRayCast : MonoBehaviour
{
    Mouse mouse;
    

    [SerializeField]
    private LayerMask aimColliderMask = new LayerMask();
    [SerializeField]
    private Transform debugTransform;
    [SerializeField]
    private Transform hitTarget = null;

    [Header("Particle Explosion Variable")]
    [SerializeField]
    private Transform explosionFx;

    [Header("Fire rate variables")]
    [SerializeField]
    private float fireRate = 0.1f;
    [SerializeField]
    private float nextShot = 0f;
    [SerializeField]
    bool shoot;

    RaycastHit h;
    // Start is called before the first frame update
    void Start()
    {
        mouse = Mouse.current;
        shoot = true;
        explosionFx.gameObject.GetComponent<ParticleSystem>().Stop();
        fireRate = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

        hitTarget = null;

        // get the center of the screen to act as the reticle 
        Vector2 screenCenterPoint = new Vector3(Screen.width / 2, Screen.height / 2);

        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 999f, aimColliderMask))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
            debugTransform.position = hit.point;
          

        }
        //trying to limit fire rate
        if (mouse.leftButton.isPressed && nextShot >= fireRate)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, aimColliderMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.blue);
                debugTransform.position = hit.point;
               
                hitTarget = hit.transform; // get the object it hits transform component
               
                //shoot = false;
                nextShot = 0;

                if (hitTarget != null)
                {   //having the particle system play over and over instead of instantiating to save on frames
                    explosionFx.transform.position = hit.point;
                    explosionFx.gameObject.GetComponent<ParticleSystem>().Play();
                    //Instantiate(explosionFx, hit.point, Quaternion.identity);

                }



                
            }

        }

        Shoot2();
       

        //if (hitTarget != null)
        //{

        //    Instantiate(explosionFx, hit.point, Quaternion.identity);

        //}

        //if (hitTarget != null)
        //{
        //    Destroy(hitTarget.gameObject);
        //}

      
    }

    private void Shoot()
    {
        if (shoot == false)
        {


            if (nextShot >= fireRate)
            {
                nextShot = 0f;
                shoot = true;
            }

            nextShot += Time.deltaTime;

        }

    }

    private void Shoot2()
    {
        if (nextShot < fireRate)
        {

            nextShot += Time.deltaTime;
            
        }
      
       
        
    }



}



