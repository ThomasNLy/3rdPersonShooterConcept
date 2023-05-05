using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class HomingMissile : MonoBehaviour
{


    [Header("Fx Refference")]
    public GameObject explosionFX;
    [Header("Weapon Variables")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 direction;
    private float timer;
    private float maxTimer;


    // Start is called before the first frame update
    void Start()
    {

        speed = 5f;
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

            Invoke("Explode", 5f);
            
           

        }
        //this.transform.position += direction * speed * Time.deltaTime;
        
        
    }
    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    public void Init(Transform target)
    {
        this.target = target;
        speed = 20;
        //timer = maxTimer + 1; // used so the missile will move in the direction right away/timer starts immediately
        maxTimer = 1.5f;


        // random x direction to calcualte the y axis rotation
        /**
         * x and z direction needed to calcualte angle using z and x axis for y axis rotation
         * a direction constant 1 as only left and right(x direction) is randomized
         */
        float randomXDir = Random.Range(-1f, 1f); 
        float yAxisRotation = Mathf.Atan2(randomXDir, 1f); // getting the angle in radians with adjacent being a constant

        /**
         * random y direction to calcualte x axis rotation
         * y and z axis used for x axis rotation 
         * z direction is a constant 1 as only up and down (y direction) is randomized
         */
        float randomYDir = Random.Range(-1f, 0f);
        float xAxisRotation = Mathf.Atan2(randomYDir, 1f);
        
        transform.Rotate(xAxisRotation * Mathf.Rad2Deg, yAxisRotation * Mathf.Rad2Deg, 0);
        direction = transform.forward; // set the direction to move in to be the  missiles forward vector
       

        setRotation();
    }

    private void SetDirection()
    {
        if (target != null)
        {
            direction = (target.position - this.transform.position).normalized;
        }
        else
        {
            this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
            direction = this.transform.forward;
        }
        setRotation();

    }
    private void setRotation()
    {
        this.transform.rotation = Quaternion.LookRotation(direction);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Explode();
            other.GetComponent<Health>().TakeDamage(2);
        }
        
        //else if (other.tag == "Floor" || other.tag == "Wall")
        //{
        //    Debug.Log(other.name);
        //    Explode();
        //}
        
        // explodes when it hits anything that isn't the player or the weapon the player is holding ie. walls, untagged items, the floor
        else if (other.tag != "Weapon" && other.tag !="Player" && other.tag !="EditorOnly")
        {
            Debug.Log(other.tag);
            Debug.Log(other.name);
            Explode();
        }






    }
    private void Explode()
    {
        AudioManager.Instance.PlayExplosionSoundEffect();
        explosionFX.GetComponent<ParticleSystem>().Play();
        Destroy(this.gameObject, explosionFX.GetComponent<ParticleSystem>().main.duration);
    }
}
