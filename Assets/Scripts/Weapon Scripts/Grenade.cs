using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    [Header("Fx Refference")]
    public GameObject explosionFX;
    [SerializeField]
    private bool startFuse;
    [Header("Grenade Variables")]
    [SerializeField]
    float durationTimer;
    [SerializeField]
    float explosionTimer;
    // Start is called before the first frame update
    void Start()
    {
        explosionFX.GetComponent<ParticleSystem>().Stop();
        durationTimer = explosionFX.GetComponent<ParticleSystem>().main.duration;
        explosionTimer = 5f;
        startFuse = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!startFuse)
        {
            startFuse = true;
            // will trigger explosion after set timer 
            Invoke("Explode", explosionTimer);
        }
        

    }



    void Explode()
    {
        AudioManager.Instance.PlayGrenadeSoundEffect();
        //this.transform.rotation = Quaternion.Euler(0, 0, 0);
        explosionFX.GetComponent<ParticleSystem>().Play();
        
        Debug.Log("Explosion");
        DealDamage();
        Destroy(this.gameObject, durationTimer);
    }


    void DealDamage()
    {
        Collider[] itemsInArea = Physics.OverlapSphere(this.transform.position, 7);
        foreach (Collider item in itemsInArea)
        {

            Vector3 direction = (item.transform.position - this.transform.position).normalized;
            if (Physics.Raycast(this.transform.position, direction, out RaycastHit itemHit, 7))
            {
                if (itemHit.transform.tag == "Enemy")
                {
                    itemHit.transform.GetComponent<Health>().TakeDamage(10);
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawSphere(this.transform.position, 7);

    }
}
