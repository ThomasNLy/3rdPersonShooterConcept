using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [Header("Health Variables")]
    [SerializeField]
    Health health;
    [SerializeField]
    bool takingDamage;
    float hurtTimer;

    private void Awake()
    {
        health = this.GetComponent<Health>();
        takingDamage = false;
        hurtTimer = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (takingDamage)
        {
            hurtTimer += Time.deltaTime;
            if (hurtTimer > 3f)
            {
                hurtTimer = 0;
                takingDamage = false;
            }
        }
    }

 
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" && !takingDamage) 
        {
            Health health = this.GetComponent<Health>();
            health.TakeDamage(1);
            takingDamage = true;
        }
        
    }

   

}
