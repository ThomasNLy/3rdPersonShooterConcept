using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrappleHook : ShootingRayCast2
{
    Keyboard keyboard;
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    private bool _usingGrapple;
    [SerializeField]
    private int speed = 12;

    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        keyboard = Keyboard.current;
        controller = GetComponent<CharacterController>();
        _usingGrapple = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        
    }

    public override void ShootRay()
    {
        if (keyboard.fKey.isPressed && !usingGrapple)
        {
            base.ShootRay();
           
        }
        MoveTowardsTarget();

    }

    protected override void DoSomethingWhenHit()
    {
        _usingGrapple = true;
        
        direction = hit.point - transform.position; // get direction vector from player to target
        direction = direction.normalized;
       


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
            controller.Move(direction.normalized * speed * Time.deltaTime);
        }
       
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        usingGrapple = false;
    }
}
