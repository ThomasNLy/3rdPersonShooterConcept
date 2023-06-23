using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowItem : MonoBehaviour
{
    Vector3 screenCenter;
    Ray ray;


    [Header("Item to throw")]
    public GameObject objectToThrow;

    [Header("Throwing Variables")]
    public float upForce;
    public float forwardForce;
    [SerializeField]
    bool readyToThrow;
    public Transform launchPoint;
    [SerializeField]
    private int throwDelay;
    [SerializeField]
    public LayerMask aimColliderMask = new LayerMask();

    // Start is called before the first frame update
    void Start()
    {
        throwDelay = 3;
        readyToThrow = true;
        upForce = 10f;
        forwardForce = 7f;

    }

    // Update is called once per frame
    void Update()
    {
        screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        ray = Camera.main.ScreenPointToRay(screenCenter);
        //Debug.Log(ray.origin);

        if (Keyboard.current.gKey.isPressed && readyToThrow)
        {
            Throw();
        }

    }


    private void Throw()
    {
        readyToThrow = false;
        Vector3 direction = ray.direction;

        if (Physics.Raycast(ray, out RaycastHit hit, 500f))
        {
            direction = (hit.point - launchPoint.position).normalized;
            //Debug.Log(direction);
            //Debug.DrawRay(ray.origin, ray.direction * 500f, Color.red);

        }




        //instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, launchPoint.position, launchPoint.rotation);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 force = direction * forwardForce + transform.up * upForce;

        projectileRb.AddForce(force, ForceMode.Impulse);

        Invoke(nameof(ResetThrow), throwDelay); // used to call a function after a delay in seconds 
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
