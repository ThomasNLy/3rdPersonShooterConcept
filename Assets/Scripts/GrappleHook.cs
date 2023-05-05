using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrappleHook : ShootingRayCast2
{
    Keyboard keyboard;
    [Header("Character controller component")]
    [SerializeField]
    CharacterController controller;
    [Header("Grapple Hook Variables")]
    [SerializeField]
    private bool _usingGrapple;
    [SerializeField]
    private int speed = 12;

    [SerializeField]
    private bool hitObject;

    [SerializeField]
    private LineRenderer lineRender;
   
    public Transform startPoint;

    Vector3 direction;

    private void Awake()
    {
        lineRender = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        keyboard = Keyboard.current;
        controller = GetComponent<CharacterController>();
        _usingGrapple = false;
        maxHitDistance = 35f;
    }

    // Update is called once per frame
    public void Update()
    {
        ShootRay();
        
    }

    public override void ShootRay()
    {
       
        if (keyboard.fKey.IsPressed() && !usingGrapple)
        {
            base.ShootRay();

        }

        

        MoveTowardsTarget();


        // checks if the player lands back on the ground after jumping up a bit after hitting an object i.e. wall and stops them from continually launching up.
        if (controller.isGrounded)

        {
            hitObject = false;
        }


        //have the player jump up a bit after hitting an object ie climb over a small ledge. 
        // can't be using the grapple hook for this to work: allows the player to chain grappling 
        if (hitObject && !usingGrapple)
        {
            controller.Move(new Vector3(0, 10, 0) * Time.deltaTime);
        }
       

    }

    protected override void DoSomethingWhenHit()
    {
        _usingGrapple = true;
        
        direction = hit.point - transform.position; // get direction vector from player to target
        direction = direction.normalized;
        Debug.Log(hit.transform.gameObject.name);
       
       


    }

    public bool usingGrapple
    {
        get { return _usingGrapple;}
        private set { _usingGrapple = value; }
    }

    protected void MoveTowardsTarget()
    {
        if (usingGrapple)
        {
            controller.Move(direction * speed * Time.deltaTime);
            lineRender.SetPosition(0, startPoint.position);
            lineRender.SetPosition(1, hit.point);
        }
        else
        {
            lineRender.SetPositions(new Vector3[] { this.transform.position, this.transform.position});
        }
       
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //checks to see is using the grapple hook and the player isn't on the ground to have them jump up after colliding with an object
        // prevents player form jumping by accident if they are grappling towards the floor 
        if (usingGrapple && controller.isGrounded == false)
        {
           Debug.Log("hit the wall");
           hitObject = true;
        }

        usingGrapple = false;
        
    }
}
