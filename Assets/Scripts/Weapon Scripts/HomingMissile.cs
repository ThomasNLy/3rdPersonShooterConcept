using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{


    [Header("Fx Refference")]
    public GameObject explosionFX;
    [Header("Weapon Variables")]
    [SerializeField]
    private int speed;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 direction;
    private float timer;
    private float maxTimer;


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {


        if (timer > maxTimer)
        {
            SetDirection();
            timer = 0f;

        }
        else
        {
            timer += Time.deltaTime;
        }

        if (target == null)
        {
            
            Destroy(gameObject, 5f);

        }
        this.transform.position += direction * speed * Time.deltaTime;
    }

    public void Init(Transform target)
    {
        this.target = target;
        speed = 20;
        timer = maxTimer + 1; // used so the missile will move in the direction right away/timer starts immediately
        maxTimer = 1.5f;

        //inital random direction before moving in proper direction
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f), Random.Range(0, this.transform.forward.z * -1));

    }

    private void SetDirection()
    {
        if (target != null)
        {
            direction = (target.position - this.transform.position).normalized;
        }
        else
        {
            direction = this.transform.forward;
        }
       

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            explosionFX.GetComponent<ParticleSystem>().Play();
            Destroy(this.gameObject, explosionFX.GetComponent<ParticleSystem>().main.duration);
            other.GetComponent<Health>().TakeDamage(2);
        }

    }
}
