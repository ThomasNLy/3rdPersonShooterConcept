using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SeekingMissleLauncher : Gun
{

    [Header("Prefab Reference")]
    public GameObject homingMissile;
    List<GameObject> targets;
    float timer; // used to check if viable targets in range every couple of seconds
    float maxTime; // delay between checking viable targets in range
    [Header("Weapon Variables")]
    [SerializeField]
    public int weaponRange;

   
     void Awake()
    {

        targets = new List<GameObject>();
        timer = 0f;
        maxTime = 2f;
        weaponRange = 30;
        fireRate = 2f;
        nextShot = fireRate;
    
        magazineAmmo = 5;
        magazineSize = 5;
        totalAmmo = 10;
        reloadTime = 5;
        bulletFX = null;
        
    }
    protected override void Init()
    {
        
    }

    public override void Shoot()
    {
       
        if (nextShot >= fireRate)
        {
            ShootBullet();
            FindTargets();
            nextShot = 0;
            for (int i = 0; i < 5; i++)
            {
                GameObject missile = Instantiate(homingMissile, this.gameObject.transform.position, this.gameObject.transform.rotation);
                
                if (targets.Count > 0)
                {
                    try
                    {
                        Transform randomTarget = targets[Random.Range(0, targets.Count)].transform;

                        missile.GetComponent<HomingMissile>().Init(randomTarget);
                    }
                    catch (MissingReferenceException e)
                    {
                        missile.GetComponent<HomingMissile>().Init(null);
                        //Debug.Log("target destroyed already");
                    }
                    
                }
                else
                {
                    missile.GetComponent<HomingMissile>().Init(null);
                }

            }

        }


  

    }

    public override void FireRateCoolDown()
    {
        base.FireRateCoolDown();
        
        /**
         * timer used for finding new targets/ if they are still in range/valid
        // */
        //if (timer < maxTime)
        //{
        //    timer += Time.deltaTime;
        //}
        //else
        //{
        //    timer = 0f;
        //    //FindTargets();
            
        //    //Debug.Log(targets.Count);
        //}

    }
    void FindTargets()
    {
        Collider[] targetsInRange = Physics.OverlapSphere(this.transform.position, weaponRange);

        foreach (Collider target in targetsInRange)
        {
            if (target.tag == "Enemy" && !targets.Contains(target.gameObject))
            {
                targets.Add(target.gameObject);
            }


        }
        if (targets.Count > 0)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] != null)
                {
                    TargetInRange(targets[i]);
                }
                else
                {
                    targets.Remove(targets[i]);
                }

            }
        }



    }

    private void TargetInRange(GameObject t)
    {

        float xDist = t.transform.position.x - this.transform.position.x;
        float yDist = t.transform.position.y - this.transform.position.y;
        float zDist = t.transform.position.z - this.transform.position.z;


        float dist = xDist * xDist + yDist * yDist + zDist * zDist;
        // Debug.Log("distance" + dist);
        if (dist > (weaponRange * weaponRange))
        {
            targets.Remove(t);

        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, 0.5f);
        Gizmos.DrawSphere(this.transform.position, 30);
    }

}
