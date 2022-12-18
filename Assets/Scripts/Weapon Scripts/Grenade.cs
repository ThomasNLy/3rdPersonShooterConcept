using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    [Header("Fx Refference")]
    public GameObject explosionFX;

    [Header("Grenade Variables")]
    [SerializeField]
    private bool exploded;
    private bool startFuse;
    [SerializeField]
    float durationTimer;
    [SerializeField]
    float explosionTimer;
    // Start is called before the first frame update
    void Start()
    {
        explosionFX.GetComponent<ParticleSystem>().Stop();
        exploded = false;
        startFuse = false;
        durationTimer = explosionFX.GetComponent<ParticleSystem>().main.duration;
        explosionTimer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!startFuse) 
        {
            startFuse = true;
            Invoke("Explode", explosionTimer);
        }




    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && exploded)
        {
            other.GetComponent<Health>().TakeDamage(10);
        }
    }


    void Explode()
    {
        exploded = true;
        explosionFX.GetComponent<ParticleSystem>().Play();
        Debug.Log("Explosion");
        Destroy(this.gameObject, durationTimer);
    }
}
