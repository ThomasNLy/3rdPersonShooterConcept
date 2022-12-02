using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy") 
        {
            Health health = this.GetComponent<Health>();
            health.TakeDamage(1);
        }
        
    }
}
