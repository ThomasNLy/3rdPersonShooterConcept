using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootMissile : MonoBehaviour
{
    Mouse mouse;
    [Header("Prefab Reference")]
    public GameObject homingMissile;
    List<GameObject> targets;
    float timer;
    float maxTime;
    [Header("Weapon Variables")]
    [SerializeField]
    int weaponRange;
    [SerializeField]
    float fireRate;
    [SerializeField]
    float nextShot;
    // Start is called before the first frame update
    void Start()
    {
        mouse = Mouse.current;
        targets = new List<GameObject>();
        timer = 0f;
        maxTime = 2f;
        weaponRange = 30;
        fireRate = 2f;
        nextShot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse.rightButton.isPressed && nextShot <= 0)
        {
            nextShot = fireRate;
            for (int i = 0; i < 5; i++)
            {
                GameObject missile = Instantiate(homingMissile, this.gameObject.transform.position, transform.rotation);
                if (targets.Count > 0)
                {
                    missile.GetComponent<HomingMissile>().Init(targets[Random.Range(0, targets.Count)].transform);
                }
                else
                {
                    missile.GetComponent<HomingMissile>().Init(null);
                }
               
            }
            
        }
        

        if (timer < maxTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
            FindTargets();
            Debug.Log(targets.Count);
        }

        if (nextShot > 0)
        {
            nextShot -= Time.deltaTime;
        }


       
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
